using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;

namespace Gekkou
{

    /// <summary>
    /// タグ名を定数で管理するクラスを作成する
    /// </summary>
    public class TagEditor
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
        private const string ITEM_NAME = "Tools/Gekkou/Create/Tag";

        /// <summary> ファイル名.拡張子 </summary>
        private static readonly string FILENAME = "Tag";

        /// <summary> ファイル作成場所のパス </summary>
        private static readonly string PATH = FileExporter.CreateFilePath(FILENAME);

        /// <summary>
        /// タグ名を定数で管理するクラスの作成
        /// </summary>
        [MenuItem(ITEM_NAME)]
        public static void Create()
        {
            if (!FileExporter.CanCreate()) return;
            CreateScript();
        }

        /// <summary>
        /// スクリプトの作成
        /// </summary>
        public static void CreateScript()
        {
            var builder = new StringBuilder();

            builder.AppendLine("/// <summary>\r" +
                               "/// タグ名を定数で管理するクラス\r" +
                               "/// </summary>");
            builder.AppendFormat($"public static class {FILENAME}").AppendLine();
            builder.AppendLine("{");

            foreach (var t in InternalEditorUtility.tags.Select(c => new { var = RemoveInvalidChars(c), val = c }))
            {
                builder.Append("\t").AppendFormat($"public const string {t.var} = \"{t.val}\";").AppendLine();
            }

            builder.AppendLine("}");

            FileExporter.FileExport(builder.ToString(), PATH);
        }

        /// <summary>
        /// 無効な文字の削除
        /// </summary>
        public static string RemoveInvalidChars(string str)
        {
            Array.ForEach(INVALUD_CHARS, c => str = str.Replace(c, string.Empty));
            return str;
        }
    }

}
