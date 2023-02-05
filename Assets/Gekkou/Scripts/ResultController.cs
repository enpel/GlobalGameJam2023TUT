using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gekkou;
using DG.Tweening;

public class ResultController : MonoBehaviour
{
    [SerializeField, EnumIndex(typeof(PlayerGrowthParameters.GrowthType))]
    private TextMeshProUGUI[] _viewLabels = new TextMeshProUGUI[4];
    [SerializeField, EnumIndex(typeof(PlayerGrowthParameters.GrowthType))]
    private Image[] _gageImage = new Image[4];
    [SerializeField]
    private TextMeshProUGUI _nameLabel;

    [SerializeField]
    private float _gameMaxRate = 10000;
    [SerializeField]
    private SpeciesDataBase _speciesDataBase;
    [SerializeField]
    private Timer _countTimer;

    private void Start()
    {
        SetViewLabels();
    }

    public void SetViewLabels()
    {
        StartCoroutine(ViewUpdate());
    }

    private IEnumerator ViewUpdate()
    {
        var old = PlayerGrowthParameters.Instance.StartParameter;
        var current = PlayerGrowthParameters.Instance.CurrentGrowthParameter;
        _nameLabel.SetText(_speciesDataBase.GetSpeciesData(current).SpeciesName_jp);

        for (int i = 0; i < 4; i++)
        {
            var num = old[i];

            _viewLabels[i].SetText(num.ToString());
            _gageImage[i].fillAmount = Mathf.Clamp01(num / _gameMaxRate);
        }

        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            if (_countTimer.UpdateTimer())
            {
                break;
            }

            for (int i = 0; i < 4; i++)
            {
                var num = Mathf.Lerp(old[i], current[i], _countTimer.TimeRate);

                _viewLabels[i].SetText(((int)num).ToString("N0"));
                _gageImage[i].fillAmount = Mathf.Clamp01(num / _gameMaxRate);
            }

            yield return null;
        }

        for (int i = 0; i < 4; i++)
        {
            var num = current[i];

            _viewLabels[i].SetText(num.ToString());
            _gageImage[i].fillAmount = Mathf.Clamp01(num / _gameMaxRate);
        }
    }
}
