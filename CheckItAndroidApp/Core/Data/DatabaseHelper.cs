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
                string challengeTable = "create table if not exists CHALLENGE (CHALLENGE_ID integer primary key autoincrement, NAME varchar not null, DURATION integer not null, FREQUENCY_ID, CREATED_ON TIMESTAMP DEFAULT CURRENT_TIMESTAMP, FOREIGN KEY (FREQUENCY_ID) REFERENCES FREQUENCY (FREQUENCY_ID));";
                string challengeEntryTable = "create table if not exists CHALLENGE_ENTRY (CHALLENGE_ENTRY_ID integer primary key autoincrement, CHALLENGE_ID integer not null, ENTRY_DATE TIMESTAMP DEFAULT CURRENT_TIMESTAMP, FOREIGN KEY (CHALLENGE_ID) REFERENCES CHALLENGE (CHALLENGE_ID));";
                string frequencyTable = "create table if not exists FREQUENCY (FREQUENCY_ID integer primary key autoincrement, VALUE integer not null, FREQUENCY_TYPE_ID integer not null, FOREIGN KEY (FREQUENCY_TYPE_ID) REFERENCES CT_FREQUENCY_TYPE (FREQUENCY_TYPE_ID));";
                string frequencyTypeTable = "create table if not exists CT_FREQUENCY_TYPE (FREQUENCY_TYPE_ID integer primary key autoincrement, DESCRIPTION varchar);";

                string insert = "INSERT INTO CHALLENGE (NAME, DURATION) VALUES ('Go to bed early', 20);";
                string insert2 = "INSERT INTO CHALLENGE (NAME, DURATION) VALUES ('Drink more water', 40);";
                string insert3 = "INSERT INTO CHALLENGE (NAME, DURATION) VALUES ('Call up a bro', 1);";
                string insert4 = "INSERT INTO CHALLENGE_ENTRY (CHALLENGE_ID, ENTRY_DATE) VALUES (1, '2016-10-03 10:10:10');";


                db.ExecSQL(challengeTable);
                db.ExecSQL(challengeEntryTable);
                db.ExecSQL(frequencyTable);
                db.ExecSQL(frequencyTypeTable);
                db.ExecSQL(insert);
                db.ExecSQL(insert2);
                db.ExecSQL(insert3);
                db.ExecSQL(insert4);
              //  db.ExecSQL(insert5);
               // db.ExecSQL(insert6);
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

        public long Insert(string tableName, ContentValues contentValues)
        {
            return db.Insert(tableName, null, contentValues);
        }
        public ICursor ExecuteQuery(string query)
        {
            return db.RawQuery(query, null);
        }

    }
}