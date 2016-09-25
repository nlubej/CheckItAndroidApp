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

namespace CheckItAndroidApp.Core.Client
{
    public class Student : Parcelable
    {
        private String id;
        private String name;
        private String grade;

        // Constructor
        public Student(String id, String name, String grade)
        {
            this.id = id;
            this.name = name;
            this.grade = grade;
        }
        // Getter and setter methods
    }
}