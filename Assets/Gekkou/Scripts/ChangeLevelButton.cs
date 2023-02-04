using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gekkou;

public class ChangeLevelButton : MonoBehaviour
{
    [SerializeField]
    private GameSystemController.GameLevel _targetLevel;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private bool _isFailButton = false;

    private void Start()
    {
        _button.onClick.AddListener(OnButton);
    }

    public void OnButton()
    {
        GameSystemController.Instance.ChangeGameLevel(_targetLevel);
        if (_isFailButton)
            UISoundManager.Instance.PlayFailure();
        else
            UISoundManager.Instance.PlaySuccess();
    }
}
