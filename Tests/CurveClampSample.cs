// Copyright (c) 2019 Nementic Games GmbH. All Rights Reserved.
// Author: Chris Yarbrough

namespace Nementic
{
    using UnityEngine;

    internal sealed class CurveClampSample : MonoBehaviour
    {
#pragma warning disable 0649

        [CurveClamp]
        public AnimationCurve defaultCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [CurveClamp(0f, 0f, 3f, 1f)]
        public AnimationCurve wideCurve = AnimationCurve.Linear(0, 0, 2.5f, 1);

        [CurveClamp(HtmlColor = HexColor.Cyan)]
        public AnimationCurve cyanCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [CurveClamp(HtmlColor = "lightblue")]
        public AnimationCurve lightBlueCurve = AnimationCurve.Linear(0, 0, 1, 1);

#pragma warning restore 0649
    }
}