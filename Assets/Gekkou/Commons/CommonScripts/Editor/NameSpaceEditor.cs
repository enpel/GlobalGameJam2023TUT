using System.Text;

namespace Gekkou
{

    /// <summary>
    /// 入力されたスクリプトにnamespaceを追加・削除する。
    /// インデントや改行が崩れる事があるので、適宜修正をすること。
    /// </summary>
    public static class NameSpaceEditor
    {
        /// <summary>
        /// 対象のスクリプト内にある namespace ** { } を削除する。
        /// ただし、一番外側かつ一つのみで、インデントは揃えない。
        /// </summary>
        public static string RemoveNameSpace(string data)
        {
            var namespacePos = data.IndexOf("namespace");
            if (namespacePos < 0)
                return data;

            var firstBracePos = data.IndexOf("{");

            data = data.Remove(namespacePos, firstBracePos - namespacePos + 1);

            var endBracePos = data.LastIndexOf("}");

            return data.Remove(endBracePos);
        }

        /// <summary>
        /// 対象のスクリプトに namespace ** { } を追加する。
        /// ただし、最後のusing文の直下に作成し、インデントは揃えない。
        /// </summary>
        public static string AddNameSpace(string data, string spaceName)
        {
            var lastUsingPos = data.LastIndexOf("using"); // 最後のusingの位置
            int insertPos = 0;
            if (lastUsingPos >= 0)
            {
                var usingLastPos = data.IndexOf("\n", lastUsingPos); // 最後のusingの改行位置
                insertPos = usingLastPos + 1;
            }

            var newData = new StringBuilder(data);

            newData.Insert(insertPos,
                "\n"
                + "namespace " + spaceName + "\n"
                + "{\n");
            newData.Append("\n}\n");

            return newData.ToString();
        }
    }
}
