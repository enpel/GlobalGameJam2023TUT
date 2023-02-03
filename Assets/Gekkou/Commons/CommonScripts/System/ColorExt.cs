using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gekkou
{

    public static class ColorExt
    {
        /// <summary>
        /// Set the opacity of the Image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="alpha"></param>
        public static void SetOpacity(this Image image, float alpha)
        {
            var c = image.color;
            image.color = new Color(c.r, c.g, c.b, alpha);
        }

        /// <summary>
        /// Set the opacity of the RawImage
        /// </summary>
        /// <param name="image"></param>
        /// <param name="alpha"></param>
        public static void SetOpacity(this RawImage image, float alpha)
        {
            var c = image.color;
            image.color = new Color(c.r, c.g, c.b, alpha);
        }

        /// <summary>
        /// Set the opacity of the Text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="alpha"></param>
        public static void SetOpacity(this Text text, float alpha)
        {
            var c = text.color;
            text.color = new Color(c.r, c.g, c.b, alpha);
        }

        /// <summary>
        /// Set the opacity of the SpriteRenderer
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="alpha"></param>
        public static void SetOpacity(this SpriteRenderer sr, float alpha)
        {
            var c = sr.color;
            sr.color = new Color(c.r, c.g, c.b, alpha);
        }

        public static void SetOpacity(this TextMeshProUGUI text, float alpha)
        {
            var c = text.color;
            text.color = new Color(c.r, c.g, c.b, alpha);
        }

        /// <summary>
        /// Returns the value with the opacity of the Color set
        /// </summary>
        /// <param name="color"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Color GetOpacity(this Color color, float alpha)
        {
            var c = color;
            return new Color(c.r, c.g, c.b, alpha);
        }
    }

}
