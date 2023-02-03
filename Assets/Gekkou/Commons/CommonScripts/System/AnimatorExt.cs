
using UnityEngine;

namespace Gekkou
{

    public static class AnimatorExt
    {
        /// <summary>
        /// Reset all Animator Triggers
        /// </summary>
        /// <param name="animator"></param>
        public static void ResetTriggers(this Animator animator)
        {
            foreach (AnimatorControllerParameter animatorController in animator.parameters)
            {
                if (animatorController.type == AnimatorControllerParameterType.Trigger)
                {
                    animator.ResetTrigger(animatorController.name);
                }
            }
        }

        /// <summary>
        /// Setting all Animator bools
        /// </summary>
        /// <param name="animator"></param>
        public static void AllSettingBools(this Animator animator, bool value = false)
        {
            foreach (AnimatorControllerParameter animatorController in animator.parameters)
            {
                if (animatorController.type == AnimatorControllerParameterType.Bool)
                {
                    animator.SetBool(animatorController.name, value);
                }
            }
        }

        /// <summary>
        /// Reset all parameters of Animator
        /// </summary>
        /// <param name="animator"></param>
        public static void ResetParameters(this Animator animator)
        {
            foreach (AnimatorControllerParameter animatorController in animator.parameters)
            {
                switch (animatorController.type)
                {
                    case AnimatorControllerParameterType.Float:
                        animator.SetFloat(animatorController.name, 0.0f);
                        break;
                    case AnimatorControllerParameterType.Int:
                        animator.SetInteger(animatorController.name, 0);
                        break;
                    case AnimatorControllerParameterType.Bool:
                        animator.SetBool(animatorController.name, false);
                        break;
                    case AnimatorControllerParameterType.Trigger:
                        animator.ResetTrigger(animator.name);
                        break;
                    default:
                        break;
                }
            }
        }
    }

}
