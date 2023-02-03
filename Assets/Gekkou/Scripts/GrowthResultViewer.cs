using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrowthResultViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _resultLabel;

    private void Start()
    {
        ResultView();
    }

    private void ResultView()
    {
        var param = GrowthParameterManager.Instance.GrowthParameters;
        
        // 仮作成
        var text = "";
        text += "貫通力 : " + param[(int)PlayerGrowthParameters.GrowthType.Penetration] + "\n";
        text += "成長速度 : " + param[(int)PlayerGrowthParameters.GrowthType.Growth] + "\n";
        text += "美力 : " + param[(int)PlayerGrowthParameters.GrowthType.Beauty] + "\n";
        text += "吸収力 : " + param[(int)PlayerGrowthParameters.GrowthType.Absorption] + "\n";

        _resultLabel.SetText(text);
    }
}
