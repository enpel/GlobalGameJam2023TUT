using UnityEngine;
using UnityEngine.InputSystem;
using Gekkou;

public class PlayerMovementController : SingletonMonobehavior<PlayerMovementController>
{
    [SerializeField] float m_speed;

    [SerializeField]
    private float _growthSpeed = 1.0f;
    public float GrowthRate // 成長速度の数値をもとに加算量を計算・保存
    {
        set
        {
            _growthSpeed = _growthSpeedCurve.Evaluate(value);
        }
    }

    [SerializeField]
    private AnimationCurve _growthSpeedCurve;

    // x軸方向の移動範囲の最小値
    [SerializeField] private float _minX = -50;
    // x軸方向の移動範囲の最大値
    [SerializeField] private float _maxX = 50;

    protected override void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystemController.Instance.IsGameLevelStop)
            return;

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

        var pos = transform.position;
        // x軸方向の移動範囲制限
        pos.x = Mathf.Clamp(pos.x, _minX, _maxX);
        transform.position = pos;
        // 移動 成長速度は、通常速度に加算していく
        transform.position += (m_speed + _growthSpeed) * Time.deltaTime * direction;
    }
}
