using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class AnimatableText : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    // Start is called before the first frame update

    public void PlayJump(string popText)
    {
        this.text.text = popText;

        Sequence sequence = DOTween.Sequence()
            .OnStart(() =>
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            })
            .Join(text.transform.DOPunchPosition(new Vector3(0,0.2f,-1), 0.3f))
            .Join(text.DOFade(0, 0.5f));

        sequence.Play();
    }

}
