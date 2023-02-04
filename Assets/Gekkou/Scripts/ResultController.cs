using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gekkou;

public class ResultController : MonoBehaviour
{
    [SerializeField]
    private Button _retryButton;
    [SerializeField]
    private Button _titleButton;

    private void Start()
    {
        _retryButton.onClick.AddListener(OnClickRetry);
        _titleButton.onClick.AddListener(OnClickTitle);
    }

    public void OnClickRetry()
    {
        // game scene に移動
        SceneSystemManager.Instance.SceneLoading(Scene.GekkouScene);
    }

    public void OnClickTitle()
    {
        // title scene に移動
        SceneSystemManager.Instance.SceneLoading(Scene.TitleScene);
    }
}
