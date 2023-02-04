using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCallButton : MonoBehaviour
{
    [SerializeField]
    private Button _callButton;

    private void Start()
    {
        if (_callButton == null)
            _callButton = GetComponent<Button>();

        _callButton.onClick.AddListener(OnOptionCall);
    }

    public void OnOptionCall()
    {
        OptionManager.Instance.EnableOption();
    }
}
