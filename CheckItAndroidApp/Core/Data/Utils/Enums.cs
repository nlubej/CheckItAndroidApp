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

namespace CheckItAndroidApp.Core.Data.Utils
{
    public class Enums
    {
        public enum ResultStatus
        {
            Added = 1,
            Modified = 2,
            Deleted = 3,
        }

        public enum FrequencyType
        {
            Custom = 1,
            Predefined = 2
        }
    }
}