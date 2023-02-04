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

    [SerializeField]
    private Button _startButton;

    private void Start()
    {
        SetViewLabels(GrowthParameterManager.Instance.GrowthParameters);
        _startButton.onClick.AddListener(OnStartButton);
    }

    public void SetViewLabels(int[] values)
    {
        for (int i = 0; i < _viewLabels.Length; i++)
        {
            _viewLabels[i].SetText(values[i] + "/1000000");
        }
    }

    public void OnStartButton()
    {

    }
}
