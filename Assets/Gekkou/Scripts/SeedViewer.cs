using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedViewer : MonoBehaviour
{
    [SerializeField]
    private SpeciesDataBase _speciesDataBase;
    [SerializeField]
    private SpriteRenderer _seedSprite;

    public void ChangeView(int[] param)
    {
        var data = _speciesDataBase.GetSpeciesData(param);
        _seedSprite.color = data.SpeciesColor;
    }
}
