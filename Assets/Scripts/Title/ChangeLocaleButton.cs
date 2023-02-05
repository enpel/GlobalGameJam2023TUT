using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class ChangeLocaleButton : MonoBehaviour
{

    public void OnChangeLocale()
    {
        Debug.Log(LocalizationSettings.SelectedLocale.Identifier.Code);
        if (LocalizationSettings.SelectedLocale.Identifier.Code == "en")
        {
            ChangeLocale("ja-JP").Forget();
        } else if (LocalizationSettings.SelectedLocale.Identifier.Code == "ja-JP")
        {
            ChangeLocale("en").Forget();
        }
    }

    async UniTask ChangeLocale(string localeName)
    {
        
        Debug.Log("ChangeLocale " +LocalizationSettings.SelectedLocale.LocaleName);
        // Set locale with locale code.
        LocalizationSettings.SelectedLocale = Locale.CreateLocale(localeName);

        // Wait initialization.
        await LocalizationSettings.InitializationOperation.Task;
    }
}
