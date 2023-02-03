using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float m_speed;

    // Update is called once per frame
    void Update()
    {
        // マウスの位置
        var mousePos = Mouse.current.position.ReadValue();

        // 画面サイズ
        Vector2 viewSize = new(Camera.main.pixelWidth, Camera.main.pixelHeight);

        // 中央を0としてマウスの値を補正
        // Clampで上に戻れない
        Vector3 direction = new(mousePos.x - viewSize.x / 2, Mathf.Clamp((mousePos.y - viewSize.y / 2), (mousePos.y - viewSize.y / 2), 0.0f), 0.0f);
        direction = direction.normalized;

        // 移動
        transform.position += m_speed * Time.deltaTime * direction;
    }
}
