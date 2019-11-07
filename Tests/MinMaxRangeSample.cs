// Copyright (c) 2019 Nementic Games GmbH. All Rights Reserved.
// Author: Chris Yarbrough

namespace Nementic
{
    using UnityEngine;

    internal class MinMaxRangeSample : MonoBehaviour
    {
#pragma warning disable 0649

        public MinMaxRange audioPitch;

        [MinMaxClamp(0f, 5f)]
        public MinMaxRange volume;

#pragma warning restore 0649
    }
}
