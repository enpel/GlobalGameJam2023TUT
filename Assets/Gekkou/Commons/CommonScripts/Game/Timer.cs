using UnityEngine;

namespace Gekkou
{

    [System.Serializable]
    public class Timer
    {
        #region Variable
        /// <summary>
        /// 設定時間
        /// </summary>
        [SerializeField]
        private float _intervalTime = 0.0f;

        /// <summary>
        /// 経過時間
        /// </summary>
        [SerializeField, ReadOnly]
        private float _elaspedTime = 0.0f;
        #endregion

        #region Property
        /// <summary>
        /// 設定時間を経過したかどうか
        /// </summary>
        public bool IsTimeUp { get => _intervalTime <= _elaspedTime; }

        /// <summary>
        /// 経過時間/設定時間の割合
        /// </summary>
        public float TimeRate { get => IsTimeUp ? 1.0f : _elaspedTime / _intervalTime; }

        /// <summary>
        /// 1.0f - (経過時間/設定時間)
        /// </summary>
        public float InverseTimeRate { get => 1.0f - TimeRate; }

        /// <summary>
        /// 残り時間
        /// </summary>
        public float LeftTime { get => _intervalTime - _elaspedTime; }

        /// <summary>
        /// 経過時間
        /// </summary>
        public float ElaspedTime { get => _elaspedTime; }
        #endregion

        #region Construct
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="interval">設定時間</param>
        public Timer(float interval = 0.0f)
        {
            _intervalTime = interval;
        }
        #endregion

        #region Public Function
        /// <summary>
        /// 時間の更新
        /// </summary>
        /// <param name="scale">経過速度(1.0fで通常速度)</param>
        /// <returns>経過時間を超えたかどうか</returns>
        public bool UpdateTimer(float scale = 1.0f)
        {
            _elaspedTime += Time.deltaTime * scale;
            return IsTimeUp;
        }

        /// <summary>
        /// ループしながら時間の更新をする
        /// </summary>
        /// <param name="scale">経過速度(1.0fで通常速度)</param>
        /// <returns>経過時間を超えたかどうか</returns>
        public bool LoopUpdateTimer(float scale = 1.0f)
        {
            if (UpdateTimer(scale))
            {
                _elaspedTime -= _intervalTime;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 経過時間のリセット
        /// </summary>
        public void ResetTimer()
        {
            _elaspedTime = 0.0f;
        }

        /// <summary>
        /// 経過時間のリセット
        /// </summary>
        /// <param name="interval">設定時間</param>
        public void ResetTimer(float interval)
        {
            _intervalTime = interval;
            _elaspedTime = 0.0f;
        }
        #endregion
    }

}
