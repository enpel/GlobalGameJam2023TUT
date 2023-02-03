using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovementController : MonoBehaviour
{
    [SerializeField] float m_speed;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Mouse.current.position.ReadValue());
        transform.position += m_speed * Time.deltaTime * Vector3.down;
    }
}
