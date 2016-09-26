using System.Collections.Generic;
using System.Linq;
using Android.Database.Sqlite;
using System;
using Android.Content;
using Android.Database;

namespace CheckItAndroidApp.Core.Data
{
    public class DatabaseHelper
    {
        private string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static string dbName = "ChallengeDb.sqlite";
        private static int dbVersion = 1;
        private SQLiteDatabase db;
        private Context context;
        private SQLiteHelper dbHelper;            

        public DatabaseHelper(Context context)
        {
            this.context = context;
            dbHelper = new SQLiteHelper(context);
        }

        public class SQLiteHelper : SQLiteOpenHelper
        {
            public SQLiteHelper(Context context) : base(context, dbName, null, dbVersion)
            {
            }

            public override void OnCreate(Android.Database.Sqlite.SQLiteDatabase db)
            {
                //Create tables
                string challengeTable = "create table if not exists CHALLENGE (CHALLENGE_ID integer primary key autoincrement, NAME varchar not null, DURATION integer not null, CREATED_ON TIMESTAMP DEFAULT CURRENT_TIMESTAMP);";
                db.ExecSQL(challengeTable);
            }

            public override void OnUpgrade(Android.Database.Sqlite.SQLiteDatabase db, int oldVersion, int newVersion)
            {
                if (oldVersion == 1 && newVersion == 2)
                {
                    //do modifications
                    return;
                }
            }
        }

        public void Open()
        {
            db = dbHelper.WritableDatabase;
        }

        public void Close()
        {
            dbHelper.Close();
        }

        /// <summary>
        /// Helper method for selecting simple data from database with where condition
        /// </summary>
        /// <param name="tableName"> Table name</param>
        /// <param name="columns"> Columns to be selected</param>
        /// <param name="condition"> optional parameter for confition</param>
        /// <returns></returns>
        public ICursor GetFromTable(string tableName, string[] columns, string condition = "")
        {
            return SimpleSelect(tableName, columns, condition);
        }
    }
}