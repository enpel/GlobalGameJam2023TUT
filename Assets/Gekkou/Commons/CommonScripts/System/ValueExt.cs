using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    /// <summary>
    /// 数値の拡張クラス
    /// </summary>
    public static class ValueExt
    {

        /// <summary>
        /// 0より大きいと1、0未満は-1、0は0を返す
        /// </summary>  
        public static int PosNega(this int value)
        {
            return value > 0 ? 1 : value < 0 ? -1 : 0;
        }

        /// <summary>
        /// 0より大きいと1、0未満は-1、0は0を返す
        /// </summary>  
        public static int PosNega(this float value)
        {
            return value > 0 ? 1 : value < 0 ? -1 : 0;
        }
    }

}
