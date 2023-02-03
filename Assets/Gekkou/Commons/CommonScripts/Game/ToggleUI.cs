using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;
using UnityEngine.Events;

namespace Gekkou
{

    public class ToggleUI : MonoBehaviour
    {
        [SerializeField]
        private Image backGroundImage;
        [SerializeField]
        private Color trueColor;
        [SerializeField]
        private Color falseColor;

        [SerializeField]
        private UnityEvent<bool> eventActions;

        [SerializeField]
        private bool toggle = false;

        [SerializeField]
        private Transform toggleTrans;
        [SerializeField]
        private float moveSpeed = 0.2f;

        private void Start()
        {
            var posX = Mathf.Abs(toggleTrans.localPosition.x) * (toggle ? 1 : -1);
            //toggleTrans.DOLocalMoveX(posX, moveSpeed);
            StartCoroutine(MoveX(toggleTrans, posX, moveSpeed));
            ChangeColor();
        }

        public void ChangeToggle(bool argBool)
        {
            MoveToggleSwitch();
            ChangeColor();
        }

        public void OnToggleButtonClicked()
        {
            toggle = !toggle;
            MoveToggleSwitch();
            ChangeColor();
            eventActions.Invoke(toggle);
        }

        private void MoveToggleSwitch()
        {
            //toggleTrans.DOLocalMoveX(-toggleTrans.localPosition.x, moveSpeed);
            StartCoroutine(MoveX(toggleTrans, -toggleTrans.localPosition.x, moveSpeed));
        }

        private void ChangeColor()
        {
            backGroundImage.color = toggle ? trueColor : falseColor;
        }

        private IEnumerator MoveX(Transform targetTrans, float posX, float moveSpeed)
        {
            var pos = targetTrans.localPosition;
            var startX = pos.x;
            var t = 0.0f;

            while (t < 1.0f)
            {
                t += Time.deltaTime * moveSpeed;

                pos.x = Mathf.Lerp(startX, posX, t);

                targetTrans.localPosition = pos;

                yield return null;
            }

            pos.x = posX;
            targetTrans.localPosition = pos;
        }
    }

}
