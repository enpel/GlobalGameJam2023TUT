using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gekkou;

public class OptionManager : SingletonMonobehavior<OptionManager>
{
    [SerializeField]
    private GameObject _optionObject;

    public void EnableOption()
    {
        _optionObject.SetActive(true);
    }

    public void DisableOption()
    {
        _optionObject.SetActive(false);
    }


}
