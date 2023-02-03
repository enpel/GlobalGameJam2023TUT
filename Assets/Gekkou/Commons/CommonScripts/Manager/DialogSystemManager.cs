using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gekkou
{

    public class DialogSystemManager : SingletonMonobehavior<DialogSystemManager>
    {
        [SerializeField]
        private Transform _viewCanvasTrans;
        [SerializeField]
        private GameObject _dialogPrefab;

        private List<Dialog> _dialogObjList = new List<Dialog>();

        public void PopupMessage(string message)
        {
            for (int i = 0; i < _dialogObjList.Count; i++)
            {
                if (!_dialogObjList[i].gameObject.activeSelf)
                {
                    _dialogObjList[i].gameObject.SetActive(true);
                    _dialogObjList[i].SetMessage(message);
                    return;
                }
            }

            var dialog = Instantiate(_dialogPrefab, _viewCanvasTrans).GetComponent<Dialog>();
            _dialogObjList.Add(dialog);
            dialog.SetMessage(message);
        }

        public void PopupMessage(string message, UnityAction selectAction, UnityAction cancelAction = null)
        {
            for (int i = 0; i < _dialogObjList.Count; i++)
            {
                if (!_dialogObjList[i].gameObject.activeSelf)
                {
                    _dialogObjList[i].gameObject.SetActive(true);
                    _dialogObjList[i].SetMessage(message, selectAction, cancelAction);
                    return;
                }
            }

            var dialog = Instantiate(_dialogPrefab, _viewCanvasTrans).GetComponent<Dialog>();
            _dialogObjList.Add(dialog);
            dialog.SetMessage(message, selectAction, cancelAction);
        }
    }

}
