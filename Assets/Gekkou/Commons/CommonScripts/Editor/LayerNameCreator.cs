using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Gekkou
{

    /// <summary>
    /// レイヤー名を定数で管理するクラスを作成するスクリプト
    /// </summary>
    public static class LayerNameCreator
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

        /// <summary> コマンド実行のパス </summary>
        private const string ITEM_NAME = "Tools/Gekkou/Create/Layer";

        /// <summary> ファイル名.拡張子 </summary>
        private static readonly string FILENAME = "Layer";

        /// <summary> ファイル作成場所のパス </summary>
        private static readonly string PATH = FileExporter.CreateFilePath(FILENAME);

        /// <summary>
        /// レイヤー名を定数で管理するクラスを作成します
        /// </summary>
        [MenuItem(ITEM_NAME)]
        public static void Create()
        {
            if (!FileExporter.CanCreate()) return;
            CreateScript();
        }

        /// <summary>
        /// スクリプトを作成します
        /// </summary>
        public static void CreateScript()
        {
            var builder = new StringBuilder();

            builder.AppendLine("/// <summary>");
            builder.AppendLine("/// レイヤー名を定数で管理するクラス");
            builder.AppendLine("/// </summary>");
            builder.AppendFormat($"public static class {FILENAME}").AppendLine();
            builder.AppendLine("{");

            foreach (var n in InternalEditorUtility.layers.
                Select(c => new { var = RemoveInvalidChars(c), val = LayerMask.NameToLayer(c) }))
            {
                builder.Append("\t").AppendFormat(@"public const int {0} = {1};", n.var, n.val).AppendLine();
            }
            foreach (var n in InternalEditorUtility.layers.
                Select(c => new { var = RemoveInvalidChars(c), val = 1 << LayerMask.NameToLayer(c) }))
            {
                builder.Append("\t").AppendFormat(@"public const int {0}Mask = {1};", n.var, n.val).AppendLine();
            }

            builder.AppendLine("}");

            FileExporter.FileExport(builder.ToString(), PATH);
        }

        /// <summary>
        /// 無効な文字を削除します
        /// </summary>
        public static string RemoveInvalidChars(string str)
        {
            Array.ForEach(INVALUD_CHARS, c => str = str.Replace(c, string.Empty));
            return str;
        }
    }
}