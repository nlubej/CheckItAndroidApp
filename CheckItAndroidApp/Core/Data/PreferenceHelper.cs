using System;
using Android.Content;
using Android.Preferences;

namespace CheckItAndroidApp.Core.Data
{
    public class PreferenceHelper
    {
        private Context context;

        public PreferenceHelper(Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Insert preference
        /// </summary>
        public void Insert<T>(string key, T value)
        {
            var prefManager = PreferenceManager.GetDefaultSharedPreferences(context);
            var editor = prefManager.Edit();

            if(typeof(T) == typeof(int))
            {
                editor.PutInt(key, Convert.ToInt32(value));
            }
            else if(typeof(T) == typeof(string))
            {
                editor.PutString(key, value.ToString());
            }

            editor.Apply();
        }

        /// <summary>
        /// Get preference with type int
        /// </summary>
        public int GetInt(string key)
        {
            var prefManager = PreferenceManager.GetDefaultSharedPreferences(context);
            return prefManager.GetInt(key, -1);
        }

        /// <summary>
        /// Get preference with type string
        /// </summary>
        public string GetString(string key)
        {
            var prefManager = PreferenceManager.GetDefaultSharedPreferences(context);
            return prefManager.GetString(key, "");
        }
    }
}