using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Linq;

namespace Gekkou
{
    /// <summary>
    /// NameSpaceEditor表示用
    /// </summary>
    public class NameSpaceEditorWindow : EditorWindow
    {
        private Object _selectFile;
        private string _spaceName;

        [MenuItem("Tools/Gekkou/Window/NamespaceEditor")]
        private static void Open()
        {
            var window = GetWindow<NameSpaceEditorWindow>();
            window.titleContent = new GUIContent("Namespace Editor");
        }

        private string GetPath()
        {
            return GetPath(AssetDatabase.GetAssetPath(_selectFile));
        }

        private string GetPath(string adPath)
        {
            var path = Application.dataPath.Replace("Assets", adPath);
            return path.Replace('/', Path.DirectorySeparatorChar);
        }

        private void RemoveNamespace()
        {
            var path = GetPath();

            RemoveNamespace(path);
        }

        private void RemoveNamespace(string path)
        {
            var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var data = new StreamReader(file, Encoding.UTF8);
            var newData = NameSpaceEditor.RemoveNameSpace(data.ReadToEnd());

            FileExporter.FileExport(newData, path, "namespace remove is complete.");
        }

        private void AddNamespace()
        {
            var path = GetPath();

            AddNamespace(path);
        }

        private void AddNamespace(string path)
        {
            var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var data = new StreamReader(file, Encoding.UTF8);
            var newData = NameSpaceEditor.AddNameSpace(data.ReadToEnd(), _spaceName);

            FileExporter.FileExport(newData, path, "namespace add is complete.");
        }

        private void SelectOfRemoveNamespace()
        {
            if (Selection.assetGUIDs != null && Selection.assetGUIDs.Length > 0)
            {
                foreach (var adPath in Selection.assetGUIDs.Select(_ => AssetDatabase.GUIDToAssetPath(_)))
                {
                    if (Path.GetExtension(adPath) == ".cs")
                    {
                        var path = GetPath(adPath);

                        RemoveNamespace(path);
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Error", "Please select \".cs\" files.", "OK");
                    }
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please select files.", "OK");
            }
        }

        private void SelectOfAddNamespace()
        {
            if (Selection.assetGUIDs != null && Selection.assetGUIDs.Length > 0)
            {
                foreach (var adPath in Selection.assetGUIDs.Select(_ => AssetDatabase.GUIDToAssetPath(_)))
                {
                    if (Path.GetExtension(adPath) == ".cs")
                    {
                        var path = GetPath(adPath);

                        AddNamespace(path);
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Error", "Please select \".cs\" files.", "OK");
                    }
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please select files.", "OK");
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical("Box");
            {
                EditorGUILayout.LabelField("Select Data");
                _selectFile = EditorGUILayout.ObjectField("Target File", _selectFile, typeof(Object), false);
                _spaceName = EditorGUILayout.TextField("Add Name", _spaceName);
            }
            EditorGUILayout.EndVertical();

            if (FileExporter.CanCreate())
            {
                EditorGUILayout.BeginVertical("Box");
                {
                    EditorGUILayout.LabelField("Processing for set data.");
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Remove Namespace"))
                        {
                            if (_selectFile != null)
                            {
                                RemoveNamespace();
                            }
                        }

                        if (GUILayout.Button("Add Namespace"))
                        {
                            if (_selectFile != null)
                            {
                                AddNamespace();
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.Space(20);

                EditorGUILayout.BeginVertical("Box");
                {
                    EditorGUILayout.LabelField("Processing for selected data.");
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Select Remove Namespace"))
                        {
                            SelectOfRemoveNamespace();
                        }

                        if (GUILayout.Button("Add Namespace"))
                        {
                            SelectOfAddNamespace();
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.HelpBox("Only Edit Mode.", MessageType.Info);
            }

        }
    }

}
