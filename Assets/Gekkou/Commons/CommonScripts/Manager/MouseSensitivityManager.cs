using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Gekkou
{

    public class MouseSensitivityManager : SingletonMonobehavior<MouseSensitivityManager>
    {
        [SerializeField]
        private float _minSensi = 0.1f;
        [SerializeField]
        private float _maxSensi = 2.0f;
        public float CurrentSensitivity { get => _currentSensitivity; }
        private float _currentSensitivity = 1.0f;

        [SerializeField]
        private GameObject _sensiPanel;
        [SerializeField]
        private bool _isEnable = false;

        [SerializeField]
        private Slider _sensiSlider;
        [SerializeField]
        private TextMeshProUGUI _valueLabel;

        public bool IsEnable { get => _isEnable; }

        public UnityEngine.Events.UnityAction _disenableAction;

        private void Start()
        {
            Init();

            SceneManager.activeSceneChanged += CheckPanelEnable;
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= CheckPanelEnable;
        }

        /// <summary>
        /// Scene切り替え時に、表示中だったら非表示に変える
        /// </summary>
        private void CheckPanelEnable(UnityEngine.SceneManagement.Scene thisScene
            , UnityEngine.SceneManagement.Scene nextScene)
        {
            if (!_isEnable)
                return;

            _isEnable = false;
            _sensiPanel.SetActive(false);
        }

        private void Init()
        {
            _currentSensitivity = SaveSystemManager.Instance.SaveData.mouseDouble;

            _sensiSlider.minValue = _minSensi;
            _sensiSlider.maxValue = _maxSensi;
            _sensiSlider.value = _currentSensitivity;

            _sensiSlider.onValueChanged.AddListener(num => ChangeSensitivity(num));

            ChangeValueLabel();
        }

        private void ChangeValueLabel()
        {
            _valueLabel.SetText(_currentSensitivity.ToString("0.00"));
        }

        public void ChangeSensitivity(float value)
        {
            _currentSensitivity = value;

            ChangeValueLabel();
        }

        public void ResetValue()
        {
            _currentSensitivity = SaveSystemManager.Instance.SaveData.mouseDouble;
            _sensiSlider.value = _currentSensitivity;

            ChangeValueLabel();
        }

        public void EnablePanel()
        {
            Init();
            _sensiPanel.SetActive(true);
        }

        public void DisenablePanel()
        {
            SaveSystemManager.Instance.SavingMouseSensitivity(_currentSensitivity);

            _sensiPanel.SetActive(false);

            _disenableAction?.Invoke();
        }
    }

}
