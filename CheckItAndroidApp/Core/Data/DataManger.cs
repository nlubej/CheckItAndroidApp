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
using CheckItAndroidApp.Core.Business.Dtos;

namespace CheckItAndroidApp.Core.Data
{
    public class DataManger
    {
        /// <summary>
        /// Manages Challange related queries
        /// </summary>
        private ChallangeData challangeData { get; set; }
        public ChallangeData ChallangeData
        {
            get { return challangeData; }
            set { challangeData = value; }
        }

        public DataManger(Context context)
        {
            challangeData = new ChallangeData(context);
        }
    }
}