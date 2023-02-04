using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

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
            var prefabPlaceDatas = ConvertPrefabPlaceDataFromCSV(request.downloadHandler.text);
            GeneratePrefab(sheetName, prefabPlaceDatas);
        }
    }

     List<PrefabPlaceData> ConvertPrefabPlaceDataFromCSV(string csv)
     {
         StringReader reader = new StringReader(csv);

         float xOffset = -50.0f;
         List<PrefabPlaceData> prefabPlaceData = new List<PrefabPlaceData>();

         string line = "";
         float placePositionY = 0;
         while (reader.Peek() > -1) {
             line = reader.ReadLine();
             string[] Splits = line.Split(',');

             for (int i = 0; i < Splits.Length; i++)
             {
                 var key = Splits[i].Replace("\"", "");
                 if (string.IsNullOrWhiteSpace(Splits[i]))
                 {
                     continue;
                 }

                 var position = new Vector3(xOffset + i, placePositionY, 0);
                 prefabPlaceData.Add(new PrefabPlaceData(position, key));
             }

             placePositionY-= 1;
         }

         return prefabPlaceData;
     }

     void GeneratePrefab(string rootName, List<PrefabPlaceData> prefabPlaceDatas)
     {
         LevelLoaderConvertSettings settings = Resources.Load<LevelLoaderConvertSettings>("LevelLoaderConvertSettings");
         Dictionary<string, GameObject> prefabDictionary = settings.GetPairDataDictionary();
         GameObject rootObject = EditorUtility.CreateGameObjectWithHideFlags(rootName, HideFlags.HideInHierarchy);
         
         foreach (var keyValuePair in prefabDictionary)
         {
             Debug.Log(keyValuePair);
         }
         
         prefabPlaceDatas.ForEach(x =>
         {
             Debug.Log("containts{"+x.PrefabTypeKey+"}: " + prefabDictionary.ContainsKey(x.PrefabTypeKey));
             if (!prefabDictionary.ContainsKey(x.PrefabTypeKey))
             {
                 return;
             }

             var originPrefab = prefabDictionary[x.PrefabTypeKey];
             Object prefab = PrefabUtility.GetPrefabParent (originPrefab);//Prefabを取得
             string path   = AssetDatabase.GetAssetPath (prefab);//Prefabのパスを取得
             Debug.Log(path);
             
             var clone= Instantiate(originPrefab, x.Position, originPrefab.transform.rotation);

             clone.transform.parent = rootObject.transform;
         });
         
         PrefabUtility.CreatePrefab (outputPath + rootName+".prefab", rootObject);

         Editor.DestroyImmediate (rootObject);

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