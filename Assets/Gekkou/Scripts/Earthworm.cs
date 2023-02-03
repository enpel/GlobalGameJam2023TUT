using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthworm : Nutrition
{
    [SerializeField]
    private float _moveSpeed = 1.0f;
    [SerializeField]
    private float _moveRange = 5.0f;
    [SerializeField, Gekkou.RangeVector]
    private Vector3 _moveDirection;

    private Vector3 startPos;
    [SerializeField]
    private Gekkou.Timer timer;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        timer.UpdateTimer();
        transform.position = startPos + _moveDirection * _moveRange * Mathf.Sin(timer.ElaspedTime * _moveSpeed);
    }
}
