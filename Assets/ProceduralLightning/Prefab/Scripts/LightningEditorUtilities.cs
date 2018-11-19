//
// Procedural Lightning for Unity
// (c) 2015 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;

#endif

namespace DigitalRuby.ThunderAndLightning
{
    [System.Serializable]
    public struct RangeOfIntegers
    {
        [Tooltip("Minimum value (inclusive)")]
        public int Minimum;

        [Tooltip("Maximum value (inclusive)")]
        public int Maximum;

		public int Random() { return UnityEngine.Random.Range(Minimum, Maximum + 1); }
        public int Random(System.Random r) { return r.Next(Minimum, Maximum + 1); }
    }

    [System.Serializable]
    public struct RangeOfFloats
    {
        [Tooltip("Minimum value (inclusive)")]
        public float Minimum;

        [Tooltip("Maximum value (inclusive)")]
        public float Maximum;

		public float Random() { return UnityEngine.Random.Range(Minimum, Maximum); }
        public float Random(System.Random r) { return Minimum + ((float)r.NextDouble() * (Maximum - Minimum)); }
    }

    public class SingleLineAttribute : PropertyAttribute
    {
        public SingleLineAttribute(string tooltip) { Tooltip = tooltip; }

        public string Tooltip { get; private set; }
    }

    public class SingleLineClampAttribute : SingleLineAttribute
    {
        public SingleLineClampAttribute(string tooltip, double minValue, double maxValue) : base(tooltip)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public double MinValue { get; private set; }
        public double MaxValue { get; private set; }
    }

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(SingleLineAttribute))]
    [CustomPropertyDrawer(typeof(SingleLineClampAttribute))]
    public class SingleLineDrawer : PropertyDrawer
    {
        private void DrawIntTextField(Rect position, string text, string tooltip, SerializedProperty prop)
        {
            EditorGUI.BeginChangeCheck();
            int value = EditorGUI.IntField(position, new GUIContent(text, tooltip), prop.intValue);
            SingleLineClampAttribute clamp = attribute as SingleLineClampAttribute;
            if (clamp != null)
            {
                value = Mathf.Clamp(value, (int)clamp.MinValue, (int)clamp.MaxValue);
            }
            if (EditorGUI.EndChangeCheck())
            {
                prop.intValue = value;
            }
        }

        private void DrawFloatTextField(Rect position, string text, string tooltip, SerializedProperty prop)
        {
            EditorGUI.BeginChangeCheck();
            float value = EditorGUI.FloatField(position, new GUIContent(text, tooltip), prop.floatValue);
            SingleLineClampAttribute clamp = attribute as SingleLineClampAttribute;
            if (clamp != null)
            {
                value = Mathf.Clamp(value, (float)clamp.MinValue, (float)clamp.MaxValue);
            }
            if (EditorGUI.EndChangeCheck())
            {
                prop.floatValue = value;
            }
        }

        private void DrawRangeField(Rect position, SerializedProperty prop, bool floatingPoint)
        {
            EditorGUIUtility.labelWidth = 30.0f;
            EditorGUIUtility.fieldWidth = 40.0f;
            float width = position.width * 0.49f;
            float spacing = position.width * 0.02f;
            position.width = width;
            if (floatingPoint)
            {
                DrawFloatTextField(position, "Min", "Minimum value", prop.FindPropertyRelative("Minimum"));
            }
            else
            {
                DrawIntTextField(position, "Min", "Minimum value", prop.FindPropertyRelative("Minimum"));
            }
            position.x = position.xMax + spacing;
            position.width = width;
            if (floatingPoint)
            {
                DrawFloatTextField(position, "Max", "Maximum value", prop.FindPropertyRelative("Maximum"));
            }
            else
            {
                DrawIntTextField(position, "Max", "Maximum value", prop.FindPropertyRelative("Maximum"));
            }
        }

        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, prop);
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent(label.text, (attribute as SingleLineAttribute).Tooltip));

            switch (prop.type)
            {
                case "RangeOfIntegers":
                    DrawRangeField(position, prop, false);
                    break;

                case "RangeOfFloats":
                    DrawRangeField(position, prop, true);
                    break;

                default:
                    EditorGUI.HelpBox(position, "[SingleLineDrawer] doesn't work with type '" + prop.type + "'", MessageType.Error);
                    break;
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }

#endif

}
