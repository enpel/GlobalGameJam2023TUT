using System.Collections.Generic;

namespace Gekkou
{

    public static class BoolExt
    {
        public static List<bool> AllChangeBool(this List<bool> list, bool argBool)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = argBool;
            }
            return list;
        }

        public static bool[] AllChangeBool(this bool[] list, bool argBool)
        {
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = argBool;
            }
            return list;
        }
    }

}
