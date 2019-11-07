// Copyright (c) 2019 Nementic Games GmbH. All Rights Reserved.
// Author: Chris Yarbrough

namespace Nementic
{
    public class MinMaxClampAttribute : System.Attribute
    {
        public float Min { get; private set; }
        public float Max { get; private set; }

        public MinMaxClampAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}
