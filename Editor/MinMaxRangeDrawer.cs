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
        private const float paddingWhenSliderShown = 5f;
        private const float miniLabelWidth = 24f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);
            var sliderRange = (MinMaxClampAttribute[])fieldInfo.GetCustomAttributes(typeof(MinMaxClampAttribute), true);
            if (sliderRange.Length > 0)
                height = (height * 2f) + paddingWhenSliderShown;
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

        private void DrawProperty(Rect position, SerializedProperty property)
        {
            var minProperty = property.FindPropertyRelative("Min");
            var maxProperty = property.FindPropertyRelative("Max");

            float minValue = minProperty.floatValue;
            float maxValue = maxProperty.floatValue;

            float columnWidth = position.width / 2f;
            float defaultWidth = position.width;
            float defaultXMin = position.xMin;

            // Adjust min value float field rect.
            var leftRect = new Rect(position)
            {
                width = columnWidth - (miniLabelWidth / 2f),
                height = EditorGUIUtility.singleLineHeight
            };

            // Get slider max and min range. If no attribute is used fall back to not clamping.
            float rangeMin = minValue;
            float rangeMax = maxValue;

            var sliderRanges = (MinMaxClampAttribute[])fieldInfo.GetCustomAttributes(typeof(MinMaxClampAttribute), true);
            if (sliderRanges.Length > 0)
            {
                rangeMin = sliderRanges[0].Min;
                rangeMax = sliderRanges[0].Max;
            }

            // Adjust min value.
            float minFieldValue = FloatFieldWithMiniLabel(leftRect, miniLabelWidth, "Min", minValue);
            minFieldValue = minFieldValue > maxValue ? maxValue : minFieldValue;

            if (sliderRanges.Length > 0)
                minFieldValue = Mathf.Clamp(minFieldValue, rangeMin, rangeMax);

            minValue = minFieldValue;

            position.xMin += leftRect.width;
            int paddingBetweenLeftRight = 5;
            position.x = leftRect.xMax + paddingBetweenLeftRight;

            int additionalMaxLabelSize = 4;

            // Adjust max value float field rect.
            var rightRect = new Rect(position)
            {
                width = columnWidth + additionalMaxLabelSize + paddingBetweenLeftRight - 2,
                height = EditorGUIUtility.singleLineHeight
            };

            // Adjust max value.
            var maxFieldValue = FloatFieldWithMiniLabel(rightRect, miniLabelWidth + 4, "Max", maxValue);
            maxFieldValue = maxFieldValue < minValue ? minValue : maxFieldValue;

            if (sliderRanges.Length > 0)
                maxFieldValue = Mathf.Clamp(maxFieldValue, rangeMin, rangeMax);

            maxValue = maxFieldValue;

            position.y += EditorGUIUtility.singleLineHeight;
            position.xMin = defaultXMin;
            position.width = defaultWidth;
            position.height = EditorGUIUtility.singleLineHeight;

            // Slider
            if (sliderRanges.Length > 0)
                EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);

            minProperty.floatValue = minValue;
            maxProperty.floatValue = maxValue;
        }

        private float FloatFieldWithMiniLabel(Rect position, float miniLabelWidth, string miniPrefixLabel, float value)
        {
            float widthBefore = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = miniLabelWidth;
            var minFieldValue = EditorGUI.FloatField(position, miniPrefixLabel, value);
            EditorGUIUtility.labelWidth = widthBefore;
            return minFieldValue;
        }
    }
}
#endif