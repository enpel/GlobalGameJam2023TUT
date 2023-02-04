using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gekkou;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;

    private void Start()
    {
        _startButton.onClick.AddListener(OnClickStart);
    }

    public void OnClickStart()
    {
        // game scene に移動
        SceneSystemManager.Instance.SceneLoading(Scene.GekkouScene);
    }
}
