using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System;
using System.Collections.Generic;

namespace Gekkou
{

    public class OpenSceneEditor : AssetPostprocessor
    {
        #region 定数
        private static string ASSETS_PATH = Application.dataPath + "/";

        private static string SOURCE_TEMPLATE =
    @"#if UNITY_EDITOR
using UnityEditor;

namespace Gekkou
{
    public class #SCRIPTNAME#
    { 
        #WRITE#
    }
}
#endif
";

        private static string OPENSCENE_TEMPLATE =
    @"
        [MenuItem(#TOOL_PATH#)]
        private static void Open#NAME#Scene()
        {
            // シーンの変更があった場合にどうするか聞く
            if (UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(#PATH#);
            }
        }
";
        #endregion

        private static void OnPostprocessAllAssets(string[] importedAssetPaths, string[] deletedAssetPaths, string[] movedAssetPaths, string[] movedFromAssetPaths)
        {
            var contain = false;
            var paths = new List<string>();
            paths.AddRange(importedAssetPaths);
            paths.AddRange(deletedAssetPaths);
            paths.AddRange(movedAssetPaths);
            foreach (var path in paths)
            {
                if (path.EndsWith(".unity"))
                {
                    contain = true;
                    break;
                }
            }

            if (contain)
            {
                EditorApplication.delayCall = () => OpenSceneCreater(false);
            }
        }

        [MenuItem("Tools/Gekkou/Create/OpenScene")]
        private static void OpenSceneCreater()
        {
            OpenSceneCreater(true);
        }

        private static void OpenSceneCreater(bool isDialog)
        {
            if (!FileExporter.CanCreate())
                return;

            var margeText = new StringBuilder();
            var filePaths = GetAllScenePath();
            foreach (var file in filePaths)
            {
                var text = new StringBuilder(OPENSCENE_TEMPLATE);

                var toolPath = EncloseDoubleQuotation("Tools/Gekkou/OpenScene/"
                    + GetSceneName(file));
                var name = RemoveInvalidChars(Path.GetFileNameWithoutExtension(file));
                var findex = file.IndexOf("Assets/");
                var path = EncloseDoubleQuotation(file.Substring(findex));

                text.Replace("#TOOL_PATH#", toolPath)
                    .Replace("#NAME#", name)
                    .Replace("#PATH#", path);

                margeText.Append(text);
                margeText.AppendLine();
            }

            var fileName = "OpenScene";
            var fileData = new StringBuilder(SOURCE_TEMPLATE);
            fileData.Replace("#SCRIPTNAME#", fileName)
                .Replace("#WRITE#", margeText.ToString());

            FileExporter.EasyFileExport(fileData.ToString(), fileName, isDialog: isDialog);
        }

        private static string[] GetAllScenePath()
        {
            var files = Directory.GetFiles(Application.dataPath, "*.unity", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = files[i].Replace("\\", "/");
            }

            return files;
        }

        private static string EncloseDoubleQuotation(string text)
        {
            var export = new StringBuilder();
            export.Append("\"").Append(text).Append("\"");
            return export.ToString();
        }

        private static string GetSceneName(string path)
        {
            var str = path.Substring(path.LastIndexOf("/") + 1);
            return str.Remove(str.IndexOf("."));
        }

        private static string RemoveInvalidChars(string path)
        {
            var invalidChar = new string[]
            {
            " ", "-", "&", "(", ")"
            };

            Array.ForEach(invalidChar, c => path = path.Replace(c, string.Empty));
            return path;
        }
    }

}
