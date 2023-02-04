using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Gekkou;

public class Mole : Nutrition
{
    [SerializeField]
    private float _moveSpeed = 2.0f;
    [SerializeField]
    private float _rotateSpeed = 1.0f;
    [SerializeField]
    private float _searchRange = 10.0f;

    [SerializeField]
    private Timer _searchTimer; // 一定時間ごとに周囲にいるか探す
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Animator _animator;

    private Transform playerTrans;
    private Tweener moveTweener;

    private bool isSearch = false;
    private bool isDeath = false;

    private void Start()
    {
        playerTrans = PlayerMovementController.Instance.transform;
        SetRotateTween();
        _searchRange *= _searchRange;
    }

    private void Update()
    {
        if (isDeath)
            return;
        if (GameSystemController.Instance.IsGameLevelStop)
            return;

        if (_searchTimer.LoopUpdateTimer())
        {
            ChangeSearch(((playerTrans.position - transform.position).sqrMagnitude <= _searchRange));
        }
    }

    private void FixedUpdate()
    {
        if (isDeath)
            return;
        if (GameSystemController.Instance.IsGameLevelStop)
            return;

        if (isSearch)
            _rigidbody.velocity = transform.forward * Time.fixedDeltaTime * _moveSpeed;
    }

    private void OnDisable()
    {
        if (moveTweener != null)
            moveTweener.Kill();
    }

    private void ChangeSearch(bool arg)
    {
        isSearch = arg;

        _animator.SetBool("Move", isSearch);
    }

    private void SetRotateTween()
    {
        moveTweener = transform.DOLookAt(playerTrans.position, _rotateSpeed)
            .OnComplete(() => SetRotateTween());
    }

    public override bool HitNutritionObject(int penetrationPower)
    {
        // 種類が吸収なので吸収
        if (_nutritionType == NutritionType.Absorption)
            return true;

        // 抵抗値が上回ったので抵抗
        if (_resistPenetrationPower > penetrationPower)
        {
            _animator.SetTrigger("Attack");
            return false;
        }

        // 下回ったので吸収
        return true;
    }

    public override void AbsorbedObject()
    {
        _animator.SetTrigger("Death");
        isDeath = true;

        Invoke("EnableObj", _disableTime);
    }
}
