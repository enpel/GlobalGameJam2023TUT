using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedViewer : MonoBehaviour
{
    [SerializeField]
    private SpeciesDataBase _speciesDataBase;
    [SerializeField]
    private SpriteRenderer[] _seedSprites;

    public void ChangeView(int[] param)
    {
        var data = _speciesDataBase.GetSpeciesData(param);
        for (int i = 0; i < _seedSprites.Length; i++)
        {
            _seedSprites[i].color = data.SpeciesColor;
        }
    }
}
