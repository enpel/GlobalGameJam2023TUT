using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class LevelLoaderWindow : EditorWindow
{
    string myString = "Hello World";
    private string sheetId = "";
    private string sheetName = "";
    private bool isLoading = false;

    private string outputPath = "Assets/Prefab/Levels/";
    
    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Tools/LevelLoader")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        LevelLoaderWindow window = (LevelLoaderWindow)EditorWindow.GetWindow(typeof(LevelLoaderWindow));
        window.Show();
        window.LoadParameter();
    }

    public void LoadParameter()
    {
        sheetId = LevelLoaderEditorData.instance.SheetId;
        sheetName = LevelLoaderEditorData.instance.SheetName;
    }

    async void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        
        sheetId = EditorGUILayout.TextField("SheetId", sheetId);
        if (LevelLoaderEditorData.instance.SheetId != null && LevelLoaderEditorData.instance.SheetId != sheetId)
        {
            LevelLoaderEditorData.instance.SheetId = sheetId;
        }
        
        sheetName = EditorGUILayout.TextField("SheetName", sheetName);
        if (LevelLoaderEditorData.instance.SheetName != null && LevelLoaderEditorData.instance.SheetName != sheetName)
        {
            LevelLoaderEditorData.instance.SheetName = sheetName;
        }

        using (new EditorGUI.DisabledScope(isLoading))
        {
            if (GUILayout.Button("Load LevelPattern from GSS"))
            {
                isLoading = true;
                try
                {
                    await LoadGSS(sheetId, sheetName);
                    isLoading = false;
                    EditorUtility.DisplayDialog("","Load Complete!" ,  "OK");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    isLoading = false;
                    throw;
                }
            }
        }
        
    }
    
     async UniTask LoadGSS(string sheetId, string sheetName){
        var request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/"+sheetId+"/gviz/tq?tqx=out:csv&sheet="+sheetName);

        await request.SendWebRequest();

        if(request.isHttpError || request.isNetworkError) {
            Debug.Log(request.error);
        }
        else{
            Debug.Log(request.downloadHandler.text);
            ConvertPrefabPlaceDataFromCSV(request.downloadHandler.text);
            await GeneratePrefab(sheetName);
        }
    }

     void ConvertPrefabPlaceDataFromCSV(string csv)
     {
         
     }

     async UniTask GeneratePrefab(string rootName)
     {
         GameObject gameObject = EditorUtility.CreateGameObjectWithHideFlags(rootName, HideFlags.HideInHierarchy);
         
         
         
         PrefabUtility.CreatePrefab (outputPath + rootName+".prefab", gameObject);

         Editor.DestroyImmediate (gameObject);

     }
     
    
}

[Serializable]
public class PrefabPlaceData
{
    private Vector3 position;
    private string prefabTypeKey;
    public Vector3 Position => position;
    public string PrefabTypeKey => prefabTypeKey;

    public PrefabPlaceData(Vector3 position, string prefabTypeKey)
    {
        this.position = position;
        this.prefabTypeKey = prefabTypeKey;
        
    }

}

public class LevelLoaderEditorData : ScriptableSingleton<LevelLoaderEditorData>
{
    public string SheetId;
    public string SheetName;
}