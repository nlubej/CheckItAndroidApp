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
using static CheckItAndroidApp.Core.Data.Utils.Enums;

namespace CheckItAndroidApp.Core.Client.Callbacks
{
    public class DialogResult<T>
    {
        public ResultStatus ResultStatus { get; set; }
        public T Result { get; set; }
    }
}