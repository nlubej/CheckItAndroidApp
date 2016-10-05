using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Java.Util.Jar;
using Android.Util;

namespace CheckItAndroidApp.Core.Business
{
    public class BiggerFloatingActionButton : FloatingActionButton
    {

        public BiggerFloatingActionButton(Context context) : base(context)
        {

        }

        public BiggerFloatingActionButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public BiggerFloatingActionButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

            int width = MeasuredWidth;
            int height = MeasuredHeight;
            SetMeasuredDimension((int)(width * 1.2f), (int)(height * 1.2f));
        }
    }
}