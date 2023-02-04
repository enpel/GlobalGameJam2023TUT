<<<<<<< HEAD
﻿using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.AddressableAssets;

public class FibrousRootManager : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] AssetReference m_rootRef;
    [SerializeField] SplineContainer m_splineContainer;
    [SerializeField] Transform m_parentTransform;
    [SerializeField] int m_fibrousRootInterval;
    [SerializeField] float m_rootPosDiff;
    [SerializeField] float m_dammyGoalYPos;

    Spline m_rootSpline;

    GameObject m_rootObj;

    void Start()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(m_rootRef);
        m_rootObj = handle.WaitForCompletion();
    }

    void Update()
    {
        // ダミー
        if (m_player.transform.position.y < m_dammyGoalYPos)
        {
            DrawFabirousRoots();
        }
    }

    public void DrawFabirousRoots()
    {
        Debug.Log("a");
        m_rootSpline = m_splineContainer.Spline;

        int count = 1;
        foreach (var i in m_rootSpline)
        {
            if (count % m_fibrousRootInterval == 0)
            {
                for (var j = 1; j <= m_player.GetComponent<PlayerGrowthParameters>().AbsorptionPower; ++j)
                {
                    Instantiate(
                        m_rootObj, 
                        new Vector3(i.Position.x, i.Position.y, m_player.transform.position.z) + m_rootPosDiff * j * new Vector3(1.0f, -1.0f, 0.0f), 
                        Quaternion.Euler(0.0f, 0.0f, -45.0f), 
                        m_parentTransform);
                    Instantiate(
                        m_rootObj, 
                        new Vector3(i.Position.x, i.Position.y, m_player.transform.position.z) - m_rootPosDiff * j * new Vector3(1.0f, 1.0f, 0.0f), 
                        Quaternion.Euler(0.0f, 0.0f, 45.0f), 
                        m_parentTransform);
                }
            }
            ++count;
        }
    }
}
