// Copyright (c) 2019 Nementic Games GmbH. All Rights Reserved.
// Author: Chris Yarbrough

#if UNITY_EDITOR
namespace Nementic
{
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(CurveClampAttribute))]
    public sealed class CurveClampDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.AnimationCurve)
            {
                var curveRange = (CurveClampAttribute)attribute;
                Rect rect = new Rect(curveRange.X, curveRange.Y, curveRange.Width, curveRange.Height);
                EditorGUI.CurveField(position, property, curveRange.Color, rect);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use CurveRange with AnimationCurve.");
            }
        }
    }
}
#endif