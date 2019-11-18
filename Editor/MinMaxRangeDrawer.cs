// Copyright (c) 2019 Nementic Games GmbH. All Rights Reserved.
// Author: Chris Yarbrough

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nementic
{
    [CustomPropertyDrawer(typeof(MinMaxRange))]
    public sealed class MinMaxRangeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);
            if (TryGetAttribute(out MinMaxClampAttribute clamp))
                height = (height * 2f) + 5f;

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            int defaultIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            DrawProperty(position, property);

            EditorGUI.indentLevel = defaultIndentLevel;
            EditorGUI.EndProperty();
        }

        private void DrawProperty(Rect rect, SerializedProperty property)
        {
            SerializedProperty minProp = property.FindPropertyRelative("Min");
            SerializedProperty maxProp = property.FindPropertyRelative("Max");

            rect.height = EditorGUIUtility.singleLineHeight;
            Rect originalRect = rect;

            rect.width = (rect.width / 2f) - 2f;
            FloatFieldWithMiniLabel(rect, minProp, 24, new GUIContent("Min"));
            rect.x = rect.xMax + 4f;
            FloatFieldWithMiniLabel(rect, maxProp, 28, new GUIContent("Max"));

            if (TryGetAttribute(out MinMaxClampAttribute clamp))
            {
                rect = originalRect;
                rect.y += EditorGUIUtility.singleLineHeight;

                if (property.hasMultipleDifferentValues)
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUI.LabelField(rect, "—");
                    }
                    return;
                }

                minProp.floatValue = Mathf.Clamp(minProp.floatValue, clamp.Min, clamp.Max);
                maxProp.floatValue = Mathf.Clamp(maxProp.floatValue, clamp.Min, clamp.Max);

                float minValue = minProp.floatValue;
                float maxValue = maxProp.floatValue;
                EditorGUI.BeginChangeCheck();
                EditorGUI.MinMaxSlider(rect, ref minValue, ref maxValue, clamp.Min, clamp.Max);
                if (EditorGUI.EndChangeCheck())
                {
                    EditorGUI.FocusTextInControl(null);
                    minProp.floatValue = minValue;
                    maxProp.floatValue = maxValue;
                }
            }
        }

        private void FloatFieldWithMiniLabel(Rect position, SerializedProperty property, float labelWidth, GUIContent label)
        {
            float widthBefore = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = labelWidth;
            EditorGUI.PropertyField(position, property, label);
            EditorGUIUtility.labelWidth = widthBefore;
        }

        private bool TryGetAttribute<T>(out T attribute) where T : PropertyAttribute
        {
            var attributes = fieldInfo.GetCustomAttributes(typeof(T), true);
            if (attributes.Length > 0)
            {
                attribute = (T)attributes[0];
                return true;
            }
            attribute = null;
            return false;
        }
    }
}
#endif