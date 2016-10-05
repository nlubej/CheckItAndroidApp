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

namespace CheckItAndroidApp.Core.Business.Dtos
{
    public class Frequency
    {
        public int Id { get; set; }
        public FrequencyType Type { get; set; }
        public int Value { get; set; }
    }
}