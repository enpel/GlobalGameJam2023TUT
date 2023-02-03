using UnityEngine;
using UnityEngine.EventSystems;

namespace Gekkou
{

    /// <summary>
    /// カスタムボタン用基底クラス
    /// </summary>
    public class CustomButton : MonoBehaviour,
        IPointerClickHandler,
        IPointerDownHandler,
        IPointerUpHandler
    {
        public System.Action OnClickCallback;

        public bool IsInteractive = true;

        private void Start()
        {
            SetClikHandler();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (IsInteractive)
                OnClickCallback?.Invoke();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
        }

        public void ChangeInteractive(bool argBool)
        {
            IsInteractive = argBool;
            ChangeInteractiveAction();
        }

        protected virtual void ChangeInteractiveAction()
        {
        }

        protected virtual void SetClikHandler()
        {
        }
    }

}
