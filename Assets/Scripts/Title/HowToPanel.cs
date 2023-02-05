using System.Collections;
using System.Collections.Generic;
using Gekkou;
using UnityEngine;

public class HowToPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public void OnClose()
    {
        UISoundManager.Instance.PlayFailure();
        this.panel.SetActive(false);
    }
    public void OnOpen()
    {
        UISoundManager.Instance.PlaySuccess();
        this.panel.SetActive(true);
    }
}
