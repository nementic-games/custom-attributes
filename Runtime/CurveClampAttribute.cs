// Copyright (c) 2019 Nementic Games GmbH. All Rights Reserved.
// Author: Chris Yarbrough

namespace Nementic
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Attribute used to constrain an <see cref="AnimationCurve"/> within a specified range.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class CurveClampAttribute : PropertyAttribute
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        /// <summary>
        ///     The HTML color as a string to display the curve in.
        ///     Supported format: #RGB, #RRGGBB
        ///     Supported plain names: red, cyan, blue, darkblue, 
        ///     lightblue, purple, yellow, lime, fuchsia, white, 
        ///     silver, grey, black, orange, brown, maroon, green, 
        ///     olive, navy, teal, aqua, magenta.
        /// </summary>
        public string HtmlColor
        {
            get => htmlColor;
            set
            {
                htmlColor = value;
                cachedColor = null;
            }
        }

        private string htmlColor;
        private Color? cachedColor;

        public Color Color
        {
            get
            {
                if (cachedColor.HasValue == false)
                {
                    if (!ColorUtility.TryParseHtmlString(HtmlColor, out Color c))
                        cachedColor = Color.green;
                    else
                        cachedColor = c;
                }
                return cachedColor.Value;
            }
        }

        /// <summary>
        ///     The default curve range from (0,0) to (1,1) and green color.
        /// </summary>
        public CurveClampAttribute() : this(0f, 0f, 1f, 1f)
        {
        }

        /// <summary>
        ///     Constrains the curve within a rectangle.
        /// </summary>
        /// <param name="normalized">
        ///     If true, the first and last keys of the curve
        ///     will be locked to (0,0) and (1,1) respectively.
        /// </param>
        public CurveClampAttribute(
            float x, float y, float width, float height,
            string color = HexColor.Green)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.HtmlColor = color;
        }
    }

    public static class HexColor
    {
        public const string Black = "#000";
        public const string White = "#FFF";
        public const string Grey = "#808080";
        public const string Red = "#F00";
        public const string Green = "#0F0";
        public const string Blue = "#00F";
        public const string Cyan = "#0FF";
        public const string Magenta = "#F0F";
        public const string Yellow = "#FFEB04";
    }
}