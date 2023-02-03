using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    public class Test : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        [HelpBox("TestCode", HelpBoxType.Info)]
        private bool _fading = false;

        [SerializeField, RangeVector(-5,10)]
        private Vector2 _randomVec;

        [SerializeField]
        private Timer _timer;

        private enum ListName
        {
            A,
            B,
            C,
        }

        [SerializeField, EnumIndex(typeof(ListName))]
        private List<string> _testList = new List<string>();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                DialogSystemManager.Instance.PopupMessage("A Input");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                DialogSystemManager.Instance.PopupMessage("S Input", () => PlayLog("Select"), () => Log.Error(this, "Cancel"));
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (!_fading)
                    StartCoroutine(Fading());
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Log.Info(this, "list : {0}", _testList.GetRandom());
            }

            if (_timer.LoopUpdateTimer())
            {
                Log.Print("Time");
            }
        }

        private void PlayLog(string log)
        {
            Log.Info(log);
        }

        private IEnumerator Fading()
        {
            _fading = true;
            Log.Print("Start Fade");
            yield return StartCoroutine(GameUIManager.Instance.FadeInOut());
            _fading = false;
        }
    }

}
