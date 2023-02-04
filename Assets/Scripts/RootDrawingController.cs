using UnityEngine;
using UnityEngine.Splines;

public class RootDrawintgController : MonoBehaviour
{
    [SerializeField] GameObject m_playerObj;
    [SerializeField] SplineContainer m_splineContainer;
    [SerializeField] float m_knotInterval;

    Vector3 m_prevPos;
    Spline m_rootSpline;

    void Start()
    {
        m_rootSpline = m_splineContainer.Spline;

        // 全削除
        m_rootSpline.Clear();

        // 初回knot
        m_rootSpline.Add(new BezierKnot(transform.position));

        m_prevPos = transform.position;
    }

    void Update()
    {
            // 過去位置と現在地の差がknotIntervalより大きい時
        if ((m_playerObj.transform.position - m_prevPos).magnitude > m_knotInterval)
        {
            // Knot追加
            m_rootSpline.Add(new BezierKnot(m_playerObj.transform.position));

            // 過去位置更新
            m_prevPos = m_playerObj.transform.position;
        }
    }
}
