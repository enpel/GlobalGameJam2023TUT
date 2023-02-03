using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Gekkou
{

    public class WindowSizeManager : SingletonMonobehavior<WindowSizeManager>
    {
        public static int ScreenSizeIndex { get; private set; } = 0;

        [SerializeField]
        private GameObject _uiObjs;
        [SerializeField]
        private Vector2Int[] _screenSizes;
        [SerializeField]
        private TMP_Dropdown _dropdown;

        protected override void Awake()
        {
            base.Awake();
            SettingDropdownOption();
        }

        private void Start()
        {
            ChangeWindowIndex(SaveSystemManager.Instance.SaveData.windowSize);
            _dropdown.value = ScreenSizeIndex;
        }

        private void SettingDropdownOption()
        {
            _dropdown.ClearOptions();
            var options = new List<string>();
            options.Add("FullScreen");
            foreach (var size in _screenSizes)
            {
                options.Add($"{size.x}×{size.y}");
            }
            _dropdown.AddOptions(options);
        }

        public void VisibleObject(bool visible)
        {
            _uiObjs.SetActive(visible);
        }

        public void ChangeWindowIndex(int index)
        {
            ScreenSizeIndex = Mathf.Clamp(index, 0, _screenSizes.Length - 1);
            SaveSystemManager.Instance.SavingWindowSize(ScreenSizeIndex);
            ChangeWindowSize();
        }

        private void ChangeWindowSize()
        {
            if (ScreenSizeIndex == 0)
                Screen.SetResolution(_screenSizes[0].x, _screenSizes[0].y, true);
            else
                Screen.SetResolution(_screenSizes[ScreenSizeIndex - 1].x, _screenSizes[ScreenSizeIndex - 1].y, false);
        }
    }

}
