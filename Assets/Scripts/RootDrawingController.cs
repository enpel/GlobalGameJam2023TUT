using UnityEngine;
using UnityEngine.Splines;

public class RootDrawingController : MonoBehaviour
{
    // スプライン
    [SerializeField] Transform m_playerTransform;
    [SerializeField] SplineContainer m_splineContainer;
    [SerializeField] float m_knotInterval;
    [SerializeField] Vector3 m_tangentIn;
    [SerializeField] Vector3 m_tangentOut;

    Vector3 m_prevPos = Vector3.zero;
    Spline m_spline;

    void Start()
    {
        m_spline = m_splineContainer.Spline;

        // 全削除
        m_spline.Clear();

        m_spline.Add(new BezierKnot(Vector3.zero, m_tangentIn, m_tangentOut));
    }

    void Update()
    {
        if (m_prevPos == Vector3.zero)
        {
            m_prevPos = m_playerTransform.position;
        }
        else if ((m_playerTransform.position - m_prevPos).magnitude > m_knotInterval)
        {
            m_spline.Add(new BezierKnot(m_playerTransform.position, m_tangentIn, m_tangentOut));
            m_prevPos = m_playerTransform.position;
        }
    }
}
