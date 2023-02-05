using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.AddressableAssets;
using Cinemachine;

public class FibrousRootManager : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_rootPrefab;
    [SerializeField] SplineContainer m_splineContainer;
    [SerializeField] Transform m_parentTransform;
    [SerializeField] CinemachineVirtualCamera m_overLookingCam;
    [SerializeField] int m_fibrousRootInterval;
    [SerializeField] float m_rootPosDiff;
    [SerializeField] Vector3 m_vCamDelta;
    [SerializeField] int m_drawPerFrame;
    [SerializeField] float m_cutTime;

    Spline m_rootSpline;
    List<GameObject> m_rightFibrousRoots = new();
    List<GameObject> m_leftFibrousRoots = new();
    int m_idxOffset = 0;
    float m_timeCount = 0;

    void Update()
    {
        if (m_leftFibrousRoots.Count > 0 && m_rightFibrousRoots.Count > 0)
        {
            for (int i = 0; i < m_drawPerFrame; ++i)
            {
                m_leftFibrousRoots[i + m_idxOffset].SetActive(true);
                m_rightFibrousRoots[i + m_idxOffset].SetActive(true);
                ++m_idxOffset;
                if (m_leftFibrousRoots.Count >= i + m_idxOffset && m_rightFibrousRoots.Count >= i + m_idxOffset)
                {
                    m_leftFibrousRoots.Clear();
                    m_rightFibrousRoots.Clear();
                    break;
                }
            }
        }

        m_timeCount += Time.unscaledDeltaTime;
        if (m_timeCount > m_cutTime)
        {
            m_overLookingCam.Priority = 0;
            Time.timeScale = 1.0f;
        }
    }

    public void InstantiateFabirousRoots()
    {
        Time.timeScale = 0.0f;
        m_timeCount = 0;
        m_overLookingCam.transform.position -= m_vCamDelta;
        m_overLookingCam.Priority = 15;

        m_rootSpline = m_splineContainer.Spline;
        int count = 1;
        foreach (var i in m_rootSpline)
        {
            if (count % m_fibrousRootInterval == 0)
            {
                for (var j = 1; j <= m_player.GetComponent<PlayerGrowthParameters>().AbsorptionPower; ++j)
                {
                    if (i.Position.y - m_rootPosDiff * j < m_player.transform.position.z)
                    {
                        break;
                    }
                    m_rightFibrousRoots.Add(Instantiate(
                        m_rootPrefab, 
                        new Vector3(i.Position.x, i.Position.y, m_player.transform.position.z) + m_rootPosDiff * j * new Vector3(1.0f, -1.0f, 0.0f), 
                        Quaternion.Euler(0.0f, 0.0f, -45.0f), 
                        m_parentTransform));
                    m_leftFibrousRoots.Add(Instantiate(
                        m_rootPrefab, 
                        new Vector3(i.Position.x, i.Position.y, m_player.transform.position.z) - m_rootPosDiff * j * new Vector3(1.0f, 1.0f, 0.0f), 
                        Quaternion.Euler(0.0f, 0.0f, 45.0f), 
                        m_parentTransform));
                }
            }
            ++count;
        }
    }
}
