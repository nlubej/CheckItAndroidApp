using System.Collections.Generic;
using CheckItAndroidApp.Core.Business.Dtos;
using Android.Content;
using System;

namespace CheckItAndroidApp.Core.Data
{
    public class ChallangeData
    {
        private DatabaseHelper db;

        public ChallangeData(Context context)
        {
            //init database helper with application context
            db = new DatabaseHelper(context);
        }

        public List<ChallangeDto> GetChallanges()
        {
            var challangeDtos = new List<ChallangeDto>();
            db.Open();

            var query = @"SELECT 
                           c.CHALLENGE_ID, 
                           c.NAME, 
                           c.DURATION,
                           count(ce.CHALLENGE_ENTRY_ID) as ENTRY_COUNT, 
                           MAX(ce.ENTRY_DATE) as MAX_DATE
                        FROM
	                        CHALLENGE c
	                        left JOIN CHALLENGE_ENTRY ce on c.CHALLENGE_ID = ce.CHALLENGE_ID
                        GROUP BY 
	                        c.CHALLENGE_ID";

            var cursor = db.ExecuteQuery(query);

            if (cursor.Count == 0)
                return challangeDtos;

            while (cursor.MoveToNext())
            {
                challangeDtos.Add(new ChallangeDto
                {
                    Id = cursor.GetInt(0),
                    Name = cursor.GetString(1),
                    Duration = cursor.GetInt(2),
                    EntriesCompleted = cursor.GetInt(3),
                    LastEntryDate = ToDateTimeNull(cursor.GetString(4)),
                });
            }

            cursor.Dispose();
            db.Close();

            return challangeDtos;
        }

        private DateTime? ToDateTimeNull(string dateString)
        {
            DateTime date;
            if (DateTime.TryParse(dateString, out date))
            {
                return date;
            }

            return null;
        }

        public ChallangeDto GetChallange(int challengeId)
        {
            var challangeDto = new ChallangeDto();
            db.Open();

            var fetchColumns = new string[] { "CHALLENGE_ID", "NAME", "DURATION" };
            var whereClause = string.Format("CHALLENGE_ID = {0}", challengeId);

            var cursor = db.GetFromTable("CHALLENGE", fetchColumns, whereClause);

            if (cursor.Count == 0)
                return null;

            cursor.MoveToFirst();

            return new ChallangeDto
            {
                Id = cursor.GetInt(0),
                Name = cursor.GetString(1),
                Duration = cursor.GetInt(2),
            };
        }

        public bool InsertChallengeEntry(int challengeId)
        {
            db.Open();

            ContentValues values = new ContentValues();
            values.Put("CHALLENGE_ID", challengeId);

            var ret = db.Insert("CHALLENGE_ENTRY", values) > 0;

            db.Close();

            return ret;
        }
    }
}