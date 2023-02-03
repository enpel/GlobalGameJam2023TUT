using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gekkou
{

    /// <summary>
    /// Help Box Type
    /// </summary>
    public enum HelpBoxType
    {
        None,
        Info,
        Warning,
        Error,
    }

    /// <summary>
    /// Show HelpBox in Inspector view
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class HelpBoxAttribute : PropertyAttribute
    {
        public string Message;

        public HelpBoxType BoxType;

        public HelpBoxAttribute(string message, HelpBoxType type = HelpBoxType.None, int order = 0)
        {
            Message = message;
            BoxType = type;
            this.order = order;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(HelpBoxAttribute))]
    public sealed class HelpBoxDrawer : DecoratorDrawer
    {
        private HelpBoxAttribute HelpBoxAttribute { get { return attribute as HelpBoxAttribute; } }

        public override void OnGUI(Rect position)
        {
            var helpBoxPosition = EditorGUI.IndentedRect(position);
            helpBoxPosition.height = GetHelpBoxHeight();

            EditorGUI.HelpBox(helpBoxPosition, HelpBoxAttribute.Message, GetMessageType(HelpBoxAttribute.BoxType));
        }

        public override float GetHeight()
        {
            return GetHelpBoxHeight();
        }

        public MessageType GetMessageType(HelpBoxType type)
        {
            switch (type)
            {
                case HelpBoxType.None: return MessageType.None;
                case HelpBoxType.Info: return MessageType.Info;
                case HelpBoxType.Warning: return MessageType.Warning;
                case HelpBoxType.Error: return MessageType.Error;
            }
            return 0;
        }

        public float GetHelpBoxHeight()
        {
            var style = new GUIStyle("HelpBox");
            var content = new GUIContent(HelpBoxAttribute.Message);
            return Mathf.Max(style.CalcHeight(content, Screen.width - (HelpBoxAttribute.BoxType != HelpBoxType.None ? 53 : 21)), 40);
        }
    }
#endif

}
