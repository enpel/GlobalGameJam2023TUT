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
    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;


    private void Start()
    {
        _retryButton.onClick.AddListener(OnClickRetry);
        _titleButton.onClick.AddListener(OnClickTitle);
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickRetry()
    {
        // game scene に移動
        SceneSystemManager.Instance.SceneLoading(Scene.PrototypeScene);
        audioSource.PlayOneShot(sound1);
    }

    public void OnClickTitle()
    {
        // title scene に移動
        SceneSystemManager.Instance.SceneLoading(Scene.TitleScene);
        audioSource.PlayOneShot(sound2);
    }
}
