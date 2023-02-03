using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Gekkou
{
    /// <summary>
    /// EnumEdior表示用
    /// </summary>
    public class EnumEditorWindow : EditorWindow
    {
        private string _enumName = "EnumName";
        [SerializeField]
        private string[] _itemNameList;
        private bool _isNumber = false;
        private int _itemStartNumber = 0;
        private string _filePath = "";
        private string _fileSummary = "";

        [MenuItem("Tools/Gekkou/Window/EnumEditor")]
        private static void Open()
        {
            var window = GetWindow<EnumEditorWindow>("Enum Editor");
        }

        private void FindFilePath()
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                _filePath = FileExporter.DEFAULT_PATH;
            }
            _filePath = EnumEditor.FindPath(_enumName, _filePath);
        }

        private void CreateEnumFile()
        {
            EnumEditor.Create(_enumName, _itemNameList, _filePath, _fileSummary, _isNumber, _itemStartNumber);
        }

        private SerializedProperty _itemNameListProp;
        [Min(0)]
        private int _itemListSize = 0;

        private void OnGUI()
        {
            var win = new SerializedObject(this);
            _itemNameListProp = win.FindProperty("_itemNameList");
            win.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.LabelField("Enum Data");
                _enumName = EditorGUILayout.TextField("Enum Name", _enumName);

                EditorGUILayout.BeginVertical(GUI.skin.box);
                {
                    EditorGUILayout.LabelField("Enum Item Name");
                    GUI.color = Color.magenta;
                    _itemListSize = EditorGUILayout.IntField("Enum Item Size", _itemListSize);
                    if (_itemListSize < 0)
                        _itemListSize = 0;

                    if (_itemNameList.Length != _itemListSize)
                        Array.Resize(ref _itemNameList, _itemListSize);

                    GUI.color = Color.yellow;
                    EditorGUI.indentLevel++;
                    for (int i = 0; i < _itemNameListProp.arraySize; i++)
                    {
                        var item = _itemNameListProp.GetArrayElementAtIndex(i);
                        EditorGUILayout.PropertyField(item, new GUIContent("Item" + i));
                    }
                    EditorGUI.indentLevel--;
                    GUI.color = Color.white;
                }
                EditorGUILayout.EndVertical();
                //EditorGUILayout.PropertyField(win.FindProperty("_itemNameList"), true);

                _isNumber = EditorGUILayout.Toggle("Is Number", _isNumber);
                if (_isNumber)
                {
                    _itemStartNumber = EditorGUILayout.IntField("Item Start Number", _itemStartNumber);
                }
                EditorGUILayout.LabelField("File Summary");
                _fileSummary = EditorGUILayout.TextArea(_fileSummary, GUILayout.Height(90.0f));
            }
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Create Enum File"))
            {
                FindFilePath();
                if (!string.IsNullOrEmpty(_filePath))
                {
                    CreateEnumFile();
                }
            }

            win.ApplyModifiedProperties();
        }

    }

}
