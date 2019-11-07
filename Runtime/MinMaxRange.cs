// Copyright (c) 2019 Nementic Games GmbH. All Rights Reserved.
// Author: Chris Yarbrough

using UnityEngine;

namespace Nementic
{
    [System.Serializable]
    public sealed class MinMaxRange
    {
        public float Min;
        public float Max;

        public MinMaxRange() { }

        public MinMaxRange(float min, float max)
        {
            this.Min = min;
            this.Max = max;
        }

        public float RandomValue => Random.Range(Min, Max);

        /// <summary>
        /// Checks whether the given value is within min and max inclusive.
        /// </summary>
        public bool Contains(float value)
        {
            return value >= Min && value <= Max;
        }
    }
}
