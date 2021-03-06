﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;

namespace MComponents
{
    public static class Extensions
    {
        public const string MFORM_IN_TABLE_ROW_TD_STYLE_ATTRIBUTE = "mcomponent_mform_in_table_row_style";

        internal static void AddEventStopPropagationClicksAttribute(this RenderTreeBuilder builder, int sequence)
        {
            builder.AddEventStopPropagationAttribute(sequence, "onclick", true);
            builder.AddEventStopPropagationAttribute(sequence, "ondblclick", true);
        }

        internal static void AddStyleWithAttribute(this RenderTreeBuilder builder, int sequence, string pAttributeName, double pLeftOffset, BoundingBox pBoundingBox)
        {
            if (pBoundingBox == null)
                return;

            string width = $"{Convert.ToString(pBoundingBox.Width, CultureInfo.InvariantCulture) }px";
            string height = $"{Convert.ToString(pBoundingBox.Height, CultureInfo.InvariantCulture) }px";

            string top = $"{Convert.ToString(pBoundingBox.BorderTop, CultureInfo.InvariantCulture) }px";
            string left = $"{Convert.ToString(pLeftOffset, CultureInfo.InvariantCulture) }px";

            builder.AddAttribute(sequence, pAttributeName, $"width: {width}; height: {height}; top: {top}; left: {left}");
        }

        internal static double FromPixelToDouble(this string pPixelString)
        {
            string value = pPixelString.Contains("px") ? pPixelString.Substring(0, pPixelString.IndexOf("px")) : pPixelString;
            return double.Parse(value, CultureInfo.InvariantCulture);
        }

        public static void AddMComponents(this IServiceCollection pServices)
        {
            pServices.AddLocalization(options => options.ResourcesPath = "Resources");
            pServices.Configure<RequestLocalizationOptions>(options =>
            {
                options.SupportedUICultures = MComponentsLocalization.SupportedCultures;
            });
        }
    }

}
