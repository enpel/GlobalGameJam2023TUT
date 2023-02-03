using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Gekkou
{

    public class PlayLog : SingletonMonobehavior<PlayLog>
    {
        [System.Serializable]
        public class LogStyle
        {
            public float _margin = 10;
            public float _padding = 4;
            public int _fontSize = 12;
        }

        public enum LogType
        {
            None,
            Info,
            Warning,
            Error,
        }

        public static void PrintLog(string text, LogType type = LogType.None)
        {
            if (!IsExists)
                return;

            var sb = new StringBuilder();

            if (type != LogType.None)
                sb.Append("[");
            switch (type)
            {
                case LogType.Info:
                    sb.Append("Info");
                    break;
                case LogType.Warning:
                    sb.Append("Warning");
                    break;
                case LogType.Error:
                    sb.Append("Error");
                    break;
            }
            if (type != LogType.None)
                sb.Append("]");

            sb.Append(text);

            Instance.Add(sb.ToString());
        }

        [SerializeField]
        private bool _usePlayLog = false;
        [SerializeField]
        private LogStyle _logStyle;
        [SerializeField]
        private int _maxLog = 5;
        private GUIStyle _guiStyle;
        private List<string> _logList = new List<string>();

        protected override void Awake()
        {
            base.Awake();

            _guiStyle = GUIStyle.none;
            _guiStyle.wordWrap = false;
            _guiStyle.normal.textColor = Color.white;
            _guiStyle.margin = new RectOffset();
            _guiStyle.padding = new RectOffset();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.O) && Input.GetKeyDown(KeyCode.P))
                _usePlayLog = !_usePlayLog;
        }

        private void Add(string text)
        {
            _logList.Add(text);

            if (_maxLog < _logList.Count)
                _logList.RemoveRange(0, _logList.Count - _maxLog);
        }

        private void OnGUI()
        {
            if (!_usePlayLog)
                return;

            var areaCount = _logList.Count;

            if (0 >= areaCount)
                return;

            var margin = _logStyle._margin;
            var padding = _logStyle._padding;
            var fontSize = _logStyle._fontSize;

            var width = (Screen.width) - (margin * 2);
            var height = (areaCount * Mathf.Ceil(fontSize * 1.1f)) + (padding * 2);
            var boxArea = new Rect(margin, margin, width, height);
            GUI.Box(boxArea, "");

            var drawArea = new Rect(boxArea.x + padding, boxArea.y + padding, boxArea.width - padding * 2, boxArea.height - padding * 2);

            GUILayout.BeginArea(drawArea);
            {
                _guiStyle.fontSize = fontSize;

                foreach (var s in _logList)
                {
                    GUILayout.Label(s, _guiStyle);
                }
            }
            GUILayout.EndArea();
        }
    }

}
