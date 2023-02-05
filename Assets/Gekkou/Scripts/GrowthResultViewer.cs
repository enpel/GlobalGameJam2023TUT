using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gekkou;

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
        var param = SaveSystemManager.Instance.SaveData.seedParameters;

        // 仮作成
        var text = "";
        text += TermLoader.Instance.GetTerm(TermType.Penetration)+":" +param[(int)PlayerGrowthParameters.GrowthType.Penetration] + "\n";
        text += TermLoader.Instance.GetTerm(TermType.Speed) + ":" +param[(int)PlayerGrowthParameters.GrowthType.Growth] + "\n";
        text += TermLoader.Instance.GetTerm(TermType.Beauty) + ":" +param[(int)PlayerGrowthParameters.GrowthType.Beauty] + "\n";
        text += TermLoader.Instance.GetTerm(TermType.Absorption) + ":" +param[(int)PlayerGrowthParameters.GrowthType.Absorption] + "\n";

        _resultLabel.SetText(text);
    }
}
