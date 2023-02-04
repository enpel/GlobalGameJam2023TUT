using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.AddressableAssets;
using Cinemachine;

public class FibrousRootManager : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] AssetReference m_rootRef;
    [SerializeField] SplineContainer m_splineContainer;
    [SerializeField] Transform m_parentTransform;
    [SerializeField] CinemachineVirtualCamera m_overLookingCam;
    [SerializeField] int m_fibrousRootInterval;
    [SerializeField] float m_rootPosDiff;
    [SerializeField] Vector3 m_vCamDelta;
    [SerializeField] float m_drawIntervalTime;

    Spline m_rootSpline;

    GameObject m_rootObj;

    void Start()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(m_rootRef);
        m_rootObj = handle.WaitForCompletion();
    }

    public void DrawFabirousRoots()
    {
        Time.timeScale = 0.0f;
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
