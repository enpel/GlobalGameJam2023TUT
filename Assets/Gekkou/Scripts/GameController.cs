using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float _startWaitTime = 1.0f;

    [SerializeField]
    private GameObject _playerObj;

    private void Start()
    {
        StartCoroutine(StartMove());
    }

    private IEnumerator StartMove()
    {
        yield return new WaitForSeconds(_startWaitTime);

        GameSystemController.Instance.IsGameLevelStop = false;

        // ここで栄養の自動生成とかを行う
    }
}
