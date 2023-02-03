using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Gekkou
{

    /// <summary>
    /// タグ名を定数で管理するクラスを作成する
    /// </summary>
    public class SceneNameEditor
    {
        // 無効な文字を管理する配列
        private static readonly string[] INVALUD_CHARS =
        {
        " ", "!", "\"", "#", "$",
        "%", "&", "\'", "(", ")",
        "-", "=", "^",  "~", "\\",
        "|", "[", "{",  "@", "`",
        "]", "}", ":",  "*", ";",
        "+", "/", "?",  ".", ">",
        ",", "<"
    };

        private const string FILEITEM_NAME = "Scene";
        /// <summary> コマンド実行のパス </summary>
        private const string MENUITEM_NAME = "Tools/Gekkou/Create/" + FILEITEM_NAME;
        /// <summary> ファイル作成場所のパス </summary>
        private const string PATH = "Assets/Scripts/" + FILEITEM_NAME + ".cs";

        /// <summary> ファイル名.拡張子 </summary>
        private static readonly string FILENAME = Path.GetFileName(PATH);
        /// <summary> ファイル名のみ </summary>
        private static readonly string FILENAME_WITHOUT_EXTENSION = Path.GetFileNameWithoutExtension(PATH);

        /// <summary>
        /// タグ名を定数で管理するクラスの作成
        /// </summary>
        [MenuItem(MENUITEM_NAME)]
        public static void Create()
        {
            if (!CanCreate()) return;
            CreateScript();
            EditorUtility.DisplayDialog(FILENAME, "Create Compleate.", "OK");
        }

        /// <summary>
        /// スクリプトの作成
        /// </summary>
        public static void CreateScript()
        {
            var builder = new StringBuilder();

            builder.AppendLine("/// <summary>\r" +
                               "/// シーン名を定数で管理するクラス\r" +
                               "/// </summary>");
            builder.AppendFormat($"public static class {FILENAME_WITHOUT_EXTENSION}").AppendLine();
            builder.AppendLine("{");

            foreach (var t in EditorBuildSettings.scenes
                .Select(c => Path.GetFileNameWithoutExtension(c.path))
                .Distinct()
                .Select(c => new { var = RemoveInvalidChars(c), val = c }))
            {
                builder.Append("\t").AppendFormat($"public const string {t.var} = \"{t.val}\";").AppendLine();
            }

            builder.AppendLine("}");

            var directoryName = Path.GetDirectoryName(PATH);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            File.WriteAllText(PATH, builder.ToString(), Encoding.UTF8);
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
        }

        /// <summary>
        /// 作成可能か取得
        /// </summary>
        /// <returns></returns>
        [MenuItem(MENUITEM_NAME, true)]
        public static bool CanCreate()
        {
            return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
        }

        /// <summary>
        /// 無効な文字の削除
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveInvalidChars(string str)
        {
            Array.ForEach(INVALUD_CHARS, c => str = str.Replace(c, string.Empty));
            return str;
        }
    }

}
