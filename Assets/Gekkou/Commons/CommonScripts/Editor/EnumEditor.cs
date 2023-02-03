using System.Text;
using System.Collections.Generic;

namespace Gekkou
{
    /// <summary>
    /// Edior上でEnumを自動生成する
    /// </summary>
    public static class EnumEditor
    {
        private static StringBuilder _code = new StringBuilder();

        private static void Init(string enumName, string summary)
        {
            _code = new StringBuilder();

            if (!string.IsNullOrEmpty(summary))
            {
                _code.AppendLine("/// <summary>")
                    .AppendFormat($"/// {summary}").AppendLine()
                    .AppendLine("/// </summary>");
            }

            _code.AppendFormat($"public enum {enumName}").AppendLine()
                .AppendLine("{");
        }

        private static void Export(string exportPath, string enumName)
        {
            _code.Append("}");

            FileExporter.FileExport(_code.ToString(), exportPath, "create enum is complete.");
        }

        public static string FindPath(string name = "default", string path = "Assets")
        {
            return UnityEditor.EditorUtility.SaveFilePanel("Enum File Export", path, name + FileExporter.DEFAULT_EXTENSION, FileExporter.DEFAULT_EXTENSION);
        }

        public static int Create(string enumName, string[] itemNameList, string exportPath, string summary = "", bool isNumber = false, int startNumber = 0)
        {
            return Create(enumName, new List<string>(itemNameList), exportPath, summary, isNumber, startNumber);
        }

        public static int Create(string enumName, List<string> itemNameList, string exportPath, string summary = "", bool isNumber = false, int startNumber = 0)
        {
            if (!FileExporter.CanCreate())
                return 1;

            Init(enumName, summary);

            int num = startNumber;
            foreach (var item in itemNameList)
            {
                _code.Append("\t").AppendFormat($"{item}");
                if (isNumber)
                {
                    _code.AppendFormat($" = {num}");
                    num++;
                }
                _code.AppendLine(",");
            }

            Export(exportPath, enumName);

            return 0;
        }
    }

}
