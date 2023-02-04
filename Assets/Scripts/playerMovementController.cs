using UnityEngine;
using UnityEngine.InputSystem;
using Gekkou;

public class PlayerMovementController : SingletonMonobehavior<PlayerMovementController>
{
    [SerializeField] float m_speed;

    protected override void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_IOS || UNITY_ANDROID
        var screenPos = Touchscreen.current.position.ReadValue();
#else
        var screenPos = Mouse.current.position.ReadValue();
#endif

        // 画面サイズ
        Vector2 viewSize = new(Camera.main.pixelWidth, Camera.main.pixelHeight);

        // 中央を0としてマウスの値を補正
        // Clampで上に戻れない
        Vector3 direction = new(screenPos.x - viewSize.x / 2, Mathf.Clamp((screenPos.y - viewSize.y / 2), (screenPos.y - viewSize.y / 2), 0.0f), 0.0f);
        direction = direction.normalized;

        // 移動
        transform.position += m_speed * Time.deltaTime * direction;
    }
}
