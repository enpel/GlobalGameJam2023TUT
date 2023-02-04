using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gekkou
{

    public class RangeVectorAttribute : PropertyAttribute
    {
        public float min;
        public float max;
        public RangeVectorAttribute(float min = -1.0f, float max = 1.0f)
        {
            this.min = min;
            this.max = max;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(RangeVectorAttribute))]
    public class RangeVectorDrawer : PropertyDrawer
    {
        // Necessary since some properties tend to collapse smaller than their content
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        // Draw a disabled property field
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (RangeVectorAttribute)attribute;
            switch (property.type)
            {
                case "Vector2":
                    property.vector2Value = new Vector2(
                        Mathf.Clamp(property.vector2Value.x, attr.min, attr.max),
                        Mathf.Clamp(property.vector2Value.y, attr.min, attr.max)
                    );
                    break;
                case "Vector3":
                    property.vector3Value = new Vector3(
                        Mathf.Clamp(property.vector3Value.x, attr.min, attr.max),
                        Mathf.Clamp(property.vector3Value.y, attr.min, attr.max),
                        Mathf.Clamp(property.vector3Value.z, attr.min, attr.max)
                    );
                    break;
            }
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
#endif
}
