using UnityEngine;
using UnityEngine.UI;

namespace Gekkou
{

    [RequireComponent(typeof(Image))]
    public class ImageHitAlpha : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private Image image;

        [SerializeField, Range(0.0f, 1.0f)]
        private float threshold = 1.0f;

#if UNITY_EDITOR
        private void Reset()
        {
            image = GetComponent<Image>();
            image.alphaHitTestMinimumThreshold = threshold;
        }

        private void OnValidate()
        {
            image.alphaHitTestMinimumThreshold = threshold;
        }
#endif
    }

}
