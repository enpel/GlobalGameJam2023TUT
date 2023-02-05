using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gekkou;

public class SelectController : MonoBehaviour
{
    [SerializeField, EnumIndex(typeof(PlayerGrowthParameters.GrowthType))]
    private TextMeshProUGUI[] _viewLabels = new TextMeshProUGUI[4];
    [SerializeField, EnumIndex(typeof(PlayerGrowthParameters.GrowthType))]
    private Image[] _gageImage = new Image[4];

    [SerializeField]
    private float _gameMaxRate = 1000;

    [SerializeField]
    private Button _leftSelectButton;
    [SerializeField]
    private Button _rightSelectButton;
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private GameObject _saveSeedLabel;

    [SerializeField]
    private TextMeshProUGUI _nameLabel;
    [SerializeField]
    private SpeciesDataBase _speciesDataBase;

    [SerializeField]
    private SeedViewer _seedViewer;
    private bool isNoSave = false;

    private enum SelectSeedType
    {
        None,
        Penetration,
        Growth,
        Beauty,
        Absorption,
        Save,
    }

    [System.Serializable]
    private class SeedParam
    {
        [EnumIndex(typeof(PlayerGrowthParameters.GrowthType))]
        public int[] Parameter = new int[4];
    }


    [SerializeField, EnumIndex(typeof(SelectSeedType))]
    private SeedParam[] _selectSeeds = new SeedParam[4];
    [SerializeField]
    private SelectSeedType _currentSelectType = SelectSeedType.Save;

    private void Start()
    {
        _leftSelectButton.onClick.AddListener(LeftButton);
        _rightSelectButton.onClick.AddListener(RightButton);
        _startButton.onClick.AddListener(SelectCorrect);

        var param = SaveSystemManager.Instance.SaveData.seedParameters;
        for (int i = 0; i < param.Length; i++)
        {
            if (param[i] > 0)
            {
                ChangeSelect(SelectSeedType.Save);
                return;
            }
        }
        isNoSave = true;
        ChangeSelect(SelectSeedType.None);
    }

    public void SetViewLabels(int[] values)
    {
        for (int i = 0; i < _viewLabels.Length; i++)
        {
            _viewLabels[i].SetText(values[i].ToString());
        }

        _nameLabel.SetText(_speciesDataBase.GetSpeciesData(values).SpeciesName_jp);

        for (int i = 0; i < _gageImage.Length; i++)
        {
            _gageImage[i].fillAmount = Mathf.Clamp01(values[i] / _gameMaxRate);
        }
    }

    public void LeftButton()
    {
        ChangeSelect(-1);
    }

    public void RightButton()
    {
        ChangeSelect(1);
    }

    private void ChangeSelect(int add)
    {
        var select = (int)_currentSelectType + add;
        if (isNoSave)
        {
            if (select < 0)
                _currentSelectType = SelectSeedType.Absorption;
            else if (select > (int)SelectSeedType.Absorption)
                _currentSelectType = 0;
            else
                _currentSelectType = (SelectSeedType)select;
        }
        else
        {
            if (select < 0)
                _currentSelectType = SelectSeedType.Save;
            else if (select > (int)SelectSeedType.Save)
                _currentSelectType = 0;
            else
                _currentSelectType = (SelectSeedType)select;
        }

        ChangeViewer();
    }

    private void ChangeSelect(SelectSeedType type)
    {
        _currentSelectType = type;
        ChangeViewer();
    }

    private void ChangeViewer()
    {
        _saveSeedLabel.SetActive(_currentSelectType == SelectSeedType.Save);
        if (_currentSelectType == SelectSeedType.Save)
        {
            _seedViewer.ChangeView(SaveSystemManager.Instance.SaveData.seedParameters);
            SetViewLabels(SaveSystemManager.Instance.SaveData.seedParameters);
        }
        else
        {
            _seedViewer.ChangeView(_selectSeeds[(int)_currentSelectType].Parameter);
            SetViewLabels(_selectSeeds[(int)_currentSelectType].Parameter);
        }
    }

    public void SelectCorrect()
    {
        if (_currentSelectType == SelectSeedType.Save)
            PlayerGrowthParameters.Instance.SettingParameter(SaveSystemManager.Instance.SaveData.seedParameters);
        else
            PlayerGrowthParameters.Instance.SettingParameter(_selectSeeds[(int)_currentSelectType].Parameter);
    }
}
