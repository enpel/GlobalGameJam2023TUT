using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 10.0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.up * _moveSpeed;

        if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.down * _moveSpeed;

        if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left * _moveSpeed;

        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * _moveSpeed;
    }
}
