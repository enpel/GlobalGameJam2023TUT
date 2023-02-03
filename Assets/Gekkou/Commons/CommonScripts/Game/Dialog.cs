using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Gekkou
{

    /// <summary>
    /// ダイアログ
    /// </summary>
    public class Dialog : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _messageLabel;
        [SerializeField]
        private Button _normalButton; // 非表示専用ボタン
        [SerializeField]
        private Button _selectButton; // 決定ボタン
        [SerializeField]
        private Button _cancelButton; // キャンセルボタン

        public void SetMessage(string message)
        {
            _messageLabel.SetText(message);

            _normalButton.onClick.AddListener(CloseDialog);

            _normalButton.gameObject.SetActive(true);
            _selectButton.gameObject.SetActive(false);
            _cancelButton.gameObject.SetActive(false);
        }

        public void SetMessage(string message, UnityAction selectAction, UnityAction cancelAction = null)
        {
            _messageLabel.SetText(message);

            if (cancelAction != null)
                _cancelButton.onClick.AddListener(cancelAction);
            _cancelButton.onClick.AddListener(CloseDialog);

            _selectButton.onClick.AddListener(selectAction);
            _selectButton.onClick.AddListener(CloseDialog);

            _normalButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(true);
            _cancelButton.gameObject.SetActive(true);
        }

        public void CloseDialog()
        {
            _normalButton.onClick.RemoveAllListeners();
            _selectButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.RemoveAllListeners();

            _normalButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(false);
            _cancelButton.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

}
