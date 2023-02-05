using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gekkou;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;


public enum TermType
{
    Penetration,
    Speed,
    Beauty,
    Absorption,
    Play,
    Setting,
    Exit,
    Retry
}

public class TermLoader : SingletonMonobehavior<TermLoader>
{
    private readonly string tableName = "GameTerms";

    [SerializeField] private TableReference reference;

    private StringTable termTable;

    // Start is called before the first frame update
    async void Awake()
    {
        await PreloadLocalizationTablesAsync();

    }

    private async UniTask PreloadLocalizationTablesAsync()
    {
        await LocalizationSettings.InitializationOperation.ToUniTask();
        await LocalizationSettings.StringDatabase.PreloadOperation.ToUniTask();
        await LocalizationSettings.StringDatabase.PreloadTables(reference).ToUniTask();
        termTable = await LocalizationSettings.StringDatabase.GetTableAsync(reference).ToUniTask();
    }

    public string GetTerm(TermType type)
    {
#if UNITY_EDITOR
        Debug.LogWarning("初期化が完了していない場合、正常に取得できない場合がある");
#endif
        var termEntry = termTable.GetEntry(type.ToString());
        return termEntry.LocalizedValue;
    }
    
    public async UniTask<string> GetTermAsync(TermType type)
    {
        var termEntry = await LocalizationSettings.StringDatabase.GetTableEntryAsync(tableName, type.ToString()).Task.AsUniTask();
        return termEntry.Entry.LocalizedValue;
    }

}
