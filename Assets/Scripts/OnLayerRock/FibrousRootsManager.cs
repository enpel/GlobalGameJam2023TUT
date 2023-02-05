using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;
using UnityEngine.Splines;

public class FibrousRootsManager : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_rootPrefab;
    [SerializeField] SplineContainer m_splineContainer;
    [SerializeField] CinemachineBrain m_cinemachineBrain;
    [SerializeField] CinemachineVirtualCamera m_overLookingCam;
    [SerializeField] int m_fibrousRootInterval;
    [SerializeField] float m_rootPosDiff;
    [SerializeField] Vector3 m_vCamDiff;
    [SerializeField] float m_drawInterval;
    [SerializeField] float m_fibCountMul;

    Spline m_rootSpline;
    float m_timeCount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        m_cinemachineBrain.m_CameraActivatedEvent.AddListener(OnChangeCamera);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateFabirousRoots()
    {
        Time.timeScale = 0.0f;
        m_overLookingCam.transform.position -= m_vCamDiff;
        m_overLookingCam.Priority = 15;

        m_timeCount += Time.unscaledDeltaTime;
        m_rootSpline = m_splineContainer.Spline;
        int count = 1;
        foreach (var i in m_rootSpline)
        {
            if (count % m_fibrousRootInterval == 0)
            {
                for (int j = 1; j <= (int)(m_player.GetComponent<PlayerGrowthParameters>().AbsorptionPower * m_fibCountMul); ++j)
                {
                    if (i.Position.y - m_rootPosDiff * j < m_player.transform.position.y)
                    {
                        break;
                    }
                    Task.Run(() =>
                    {
                        while (m_timeCount < m_drawInterval)
                        {
                            continue;
                        }
                    });
                    Instantiate(
                        m_rootPrefab,
                        new Vector3(i.Position.x, i.Position.y, m_player.transform.position.z) + m_rootPosDiff * j * new Vector3(1.0f, -1.0f, 0.0f),
                        Quaternion.Euler(0.0f, 0.0f, -45.0f),
                        transform);
                    Instantiate(
                        m_rootPrefab,
                        new Vector3(i.Position.x, i.Position.y, m_player.transform.position.z) - m_rootPosDiff * j * new Vector3(1.0f, 1.0f, 0.0f),
                        Quaternion.Euler(0.0f, 0.0f, 45.0f),
                        transform);
                }
            }
            ++count;
        }
    }

    void OnChangeCamera(ICinemachineCamera incomingVcam, ICinemachineCamera outgoingVcam)
    {
        if (incomingVcam.Name != m_overLookingCam.Name)
        {
            Task.Run(() =>
            {
                while (m_cinemachineBrain.ActiveBlend.BlendWeight < 1.0f)
                {
                    continue;
                }
            });
            m_overLookingCam.Priority = 15;
            Time.timeScale = 1.0f;
        }
    }
}
