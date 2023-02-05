using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gekkou;
using DG.Tweening;

public class GameSystemController : SingletonMonobehavior<GameSystemController>
{
    public bool IsGameLevelStop = true;

    [SerializeField]
    private GameObject _titleLevelObj;
    [SerializeField]
    private GameObject _titleCanvas;
    [SerializeField]
    private AudioClip _titleBGM;

    [SerializeField]
    private GameObject _selectLevelObj;

    [SerializeField]
    private GameObject _gameLevelObj;
    [SerializeField]
    private AudioClip _gameBGM;

    [SerializeField]
    private GameObject _resultLevelObj;
    [SerializeField]
    private AudioClip _resultBGM;

    [SerializeField]
    private GameObject _handObj;
    private enum HandPos { Wait, Select, Start}
    [SerializeField, EnumIndex(typeof(HandPos))]
    private Transform[] _handPoses = new Transform[3];
    [SerializeField]
    private float _handMoveTime = 2.0f;

    [SerializeField]
    private GameObject _seedObj;

    public enum GameLevel
    {
        Title,
        Select,
        Game,
        Result,
    }

    [SerializeField, ReadOnly]
    private GameLevel _currentLevel = GameLevel.Title;

    private Tween handTween;
    private Tween handTween2;

    protected override void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeGameLevel(GameLevel.Title);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.T))
            ChangeGameLevel(GameLevel.Title);
        if (Input.GetKey(KeyCode.S))
            ChangeGameLevel(GameLevel.Select);
        if (Input.GetKey(KeyCode.G))
            ChangeGameLevel(GameLevel.Game);
        if (Input.GetKey(KeyCode.R))
            ChangeGameLevel(GameLevel.Result);
    }

    public void ChangeGameLevel(GameLevel nextLevel)
    {
        switch (nextLevel)
        {
            case GameLevel.Title:
                BGMManager.Instance.FadeAudio(_titleBGM);
                if (_currentLevel == GameLevel.Result)
                {
                    _currentLevel = nextLevel;
                    GrowthParameterManager.Instance.UpdateGrowthParameter();
                    SceneSystemManager.Instance.SceneReloading();
                    return;
                }

                handTween = _handObj.transform.DOMove(_handPoses[(int)HandPos.Wait].position, _handMoveTime);
                handTween2 = _handObj.transform.DORotateQuaternion(_handPoses[(int)HandPos.Wait].rotation, _handMoveTime);
                break;
            case GameLevel.Select:
                handTween = _handObj.transform.DOMove(_handPoses[(int)HandPos.Select].position, _handMoveTime);
                handTween2 = _handObj.transform.DORotateQuaternion(_handPoses[(int)HandPos.Select].rotation, _handMoveTime);
                break;
            case GameLevel.Game:
                BGMManager.Instance.FadeAudio(_gameBGM);
                handTween = _handObj.transform.DOMove(_handPoses[(int)HandPos.Start].position, _handMoveTime);
                handTween2 = _handObj.transform.DORotateQuaternion(_handPoses[(int)HandPos.Start].rotation, _handMoveTime);
                break;
            case GameLevel.Result:
                BGMManager.Instance.FadeAudio(_resultBGM);
                handTween = _handObj.transform.DOMove(_handPoses[(int)HandPos.Wait].position, _handMoveTime);
                handTween2 = _handObj.transform.DORotateQuaternion(_handPoses[(int)HandPos.Wait].rotation, _handMoveTime);
                IsGameLevelStop = true;
                break;
            default:
                break;
        }

        _titleLevelObj.SetActive(nextLevel == GameLevel.Title || nextLevel == GameLevel.Select);
        _titleCanvas.SetActive(nextLevel == GameLevel.Title);
        _selectLevelObj.SetActive(nextLevel == GameLevel.Select);
        _gameLevelObj.SetActive(nextLevel == GameLevel.Game || nextLevel == GameLevel.Result);
        _resultLevelObj.SetActive(nextLevel == GameLevel.Result);

        _currentLevel = nextLevel;
    }

}
