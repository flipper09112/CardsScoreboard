using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using static Android.Renderscripts.Sampler;
using Context = Android.Content.Context;

namespace CardsScoreboard.Helpers
{
    public static class ScreenHelper
    {

        public static int GetScreenWidth(Context context)
        {
            var metrics = new DisplayMetrics();
            (context as Activity)?.WindowManager.DefaultDisplay.GetMetrics(metrics);
            return metrics.WidthPixels;
        }

        public static int DpToPx(Context context, float value)
        {
            return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, value, context.Resources.DisplayMetrics);
        }
    }
}