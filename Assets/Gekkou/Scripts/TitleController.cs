using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gekkou;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;
    public AudioClip sound1;
    AudioSource audioSource;

    private void Start()
    {
        _startButton.onClick.AddListener(OnClickStart);
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickStart()
    {
        // game scene に移動
        SceneSystemManager.Instance.SceneLoading(Scene.PrototypeScene);

        audioSource.PlayOneShot(sound1);
    }
}
