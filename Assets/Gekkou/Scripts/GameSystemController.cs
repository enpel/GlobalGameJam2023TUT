using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gekkou;
using DG.Tweening;

public class GameSystemController : MonoBehaviour
{
    [SerializeField]
    private GameObject _titleLevelObj;
    [SerializeField]
    private GameObject _titleCanvas;

    [SerializeField]
    private GameObject _selectLevelObj;

    [SerializeField]
    private GameObject _gameLevelObj;

    [SerializeField]
    private GameObject _resultLevelObj;

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
        _titleLevelObj.SetActive(nextLevel == GameLevel.Title || nextLevel == GameLevel.Select);
        _titleCanvas.SetActive(nextLevel == GameLevel.Title);
        _selectLevelObj.SetActive(nextLevel == GameLevel.Select);
        _gameLevelObj.SetActive(nextLevel == GameLevel.Game);
        _resultLevelObj.SetActive(nextLevel == GameLevel.Result);

        switch (nextLevel)
        {
            case GameLevel.Title:
                handTween = _handObj.transform.DOMove(_handPoses[(int)HandPos.Wait].position, _handMoveTime);
                handTween2 = _handObj.transform.DORotateQuaternion(_handPoses[(int)HandPos.Wait].rotation, _handMoveTime);
                break;
            case GameLevel.Select:
                handTween = _handObj.transform.DOMove(_handPoses[(int)HandPos.Select].position, _handMoveTime);
                handTween2 = _handObj.transform.DORotateQuaternion(_handPoses[(int)HandPos.Select].rotation, _handMoveTime);
                break;
            case GameLevel.Game:
                handTween = _handObj.transform.DOMove(_handPoses[(int)HandPos.Start].position, _handMoveTime);
                handTween2 = _handObj.transform.DORotateQuaternion(_handPoses[(int)HandPos.Start].rotation, _handMoveTime);
                break;
            case GameLevel.Result:
                break;
            default:
                break;
        }
    }
}
