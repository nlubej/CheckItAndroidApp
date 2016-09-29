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

            public override void OnCreate(SQLiteDatabase db)
            {
                //Create tables
                string challengeTable = "create table if not exists CHALLENGE (CHALLENGE_ID integer primary key autoincrement, NAME varchar not null, DURATION integer not null, CREATED_ON TIMESTAMP DEFAULT CURRENT_TIMESTAMP);";
                string challengeEntryTable = "create table if not exists CHALLENGE_ENTRY (CHALLENGE_ENTRY_ID integer primary key autoincrement, CHALLENGE_ID integer not null, ENTRY_DATE TIMESTAMP DEFAULT CURRENT_TIMESTAMP, FOREIGN KEY (CHALLENGE_ID) REFERENCES CHALLENGE (CHALLENGE_ID));";


                string insert = "INSERT INTO CHALLENGE (NAME, DURATION) VALUES ('Go to bed early', 20);";
                string insert2 = "INSERT INTO CHALLENGE (NAME, DURATION) VALUES ('Drink more water', 40);";
                string insert3 = "INSERT INTO CHALLENGE (NAME, DURATION) VALUES ('Call up a bro', 15);";


                db.ExecSQL(challengeTable);
                db.ExecSQL(challengeEntryTable);
                db.ExecSQL(insert);
                db.ExecSQL(insert2);
                db.ExecSQL(insert3);
            }

            public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
            {
                if (oldVersion == 1 && newVersion == 2)
                {
                    //do modifications like alter table
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
            return SelectWhere(tableName, columns, condition);
        }

        private ICursor SelectWhere(string tableName, string[] columns, string condition)
        {
            return db.Query(tableName, columns, condition, null, null, null, null);
        }
    }
}