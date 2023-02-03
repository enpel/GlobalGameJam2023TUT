using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    /// <summary>
    /// Log表示。Debug.Logを管理
    /// </summary>
    public static class Log
    {
        private static void Message(string message, PlayLog.LogType type = PlayLog.LogType.None)
        {
#if UNITY_EDITOR
            switch (type)
            {
                case PlayLog.LogType.Warning:
                    Debug.LogWarning(message);
                    break;
                case PlayLog.LogType.Error:
                    Debug.LogError(message);
                    break;
                default:
                    Debug.Log(message);
                    break;
            }
#endif
            PlayLog.PrintLog(message, type);
        }

        public static void Print(string text)
        {
            Message(text);
        }

        public static void Print(Type instance, string text)
        {
            Message(GetHeader(instance) + text);
        }

        public static void Print<T>(T instance, string text)
        {
            Message(GetHeader(typeof(T)) + text);
        }

        public static void Info(string text)
        {
            Message(text);
        }

        public static void Info(Type instance, string text)
        {
            Message(GetHeader(instance) + text, PlayLog.LogType.Info);
        }

        public static void Info<T>(T instance, string text)
        {
            Message(GetHeader(typeof(T)) + text, PlayLog.LogType.Info);
        }

        public static void Info(Type instance, string text, params object[] args)
        {
            Message(string.Format(GetHeader(instance) + text, args), PlayLog.LogType.Info);
        }

        public static void Info<T>(T instance, string text, params object[] args)
        {
            Message(string.Format(GetHeader(typeof(T)) + text, args), PlayLog.LogType.Info);
        }

        public static void Error(Type instance, string text, params object[] args)
        {
            Message(string.Format(GetHeader(instance) + text, args), PlayLog.LogType.Error);
        }

        public static void Error<T>(T instance, string text, params object[] args)
        {
            Message(string.Format(GetHeader(typeof(T)) + text, args), PlayLog.LogType.Error);
        }

        private static string GetHeader(Type type)
        {
            if (type == null)
            {
                return string.Format("[{0}]", Time.frameCount, type);
            }

            return string.Format("[{0}][{1}]", Time.frameCount, type);
        }
    }

}
