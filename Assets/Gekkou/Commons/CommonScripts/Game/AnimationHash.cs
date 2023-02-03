using UnityEngine;/// <summary>/// アニメーションのパラメーターの管理クラス/// </summary>

namespace Gekkou
{
    public static class AnimationHash
    {
        public static int Side = Animator.StringToHash("Side");
        public static int Front = Animator.StringToHash("Front");
        public static int Damage = Animator.StringToHash("Damage");
        public static int Death = Animator.StringToHash("Death");
        public static int Attack = Animator.StringToHash("Attack");
        public static int AttackNum = Animator.StringToHash("AttackNum");
        public static int MoveSpeed = Animator.StringToHash("MoveSpeed");
    }

}
