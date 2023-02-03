using UnityEngine;
using UnityEngine.Splines;

public class RootDrawintgController : MonoBehaviour
{
    [SerializeField] Transform m_playerTransform;
    [SerializeField] SplineContainer m_splineContainer;
    [SerializeField] float m_knotInterval;
    [SerializeField] Vector3 m_tangentIn;
    [SerializeField] Vector3 m_tangentOut;

    Vector3 m_prevPos;
    Spline m_spline;

    void Start()
    {
        m_spline = m_splineContainer.Spline;

        // 全削除
        m_spline.Clear();

        // 初回knot
        m_spline.Add(new BezierKnot(transform.position, m_tangentIn, m_tangentOut));

        m_prevPos = transform.position;
    }

    void Update()
    {
        // 過去位置と現在地の差がknotIntervalより大きい時
        if ((m_playerTransform.position - m_prevPos).magnitude > m_knotInterval)
        {
            // Knot追加
            m_spline.Add(new BezierKnot(m_playerTransform.position, m_tangentIn, m_tangentOut));

            // 過去位置更新
            m_prevPos = m_playerTransform.position;
        }
    }
}
