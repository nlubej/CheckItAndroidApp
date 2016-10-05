using System.Collections.Generic;
using CheckItAndroidApp.Core.Business.Dtos;
using Android.Content;
using System;
using static CheckItAndroidApp.Core.Data.Utils.Enums;

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

        public List<ChallengeDto> GetChallanges()
        {
            var challangeDtos = new List<ChallengeDto>();
            db.Open();

            var query = string.Format(@"SELECT 
                           c.CHALLENGE_ID, 
                           c.NAME, 
                           c.DURATION,
                           count(ce.CHALLENGE_ENTRY_ID) as ENTRY_COUNT, 
                           MAX(ce.ENTRY_DATE) as MAX_DATE,
                           f.FREQUENCY_ID,
                           f.VALUE,
                           ft.FREQUENCY_TYPE_ID
                        FROM
	                        CHALLENGE c
	                        left JOIN CHALLENGE_ENTRY ce on c.CHALLENGE_ID = ce.CHALLENGE_ID
                            left join FREQUENCY f on  f.FREQUENCY_ID = c.FREQUENCY_ID
                            left join CT_FREQUENCY_TYPE ft on ft.FREQUENCY_TYPE_ID = f.FREQUENCY_TYPE_ID
                        GROUP BY 
	                        c.CHALLENGE_ID", Utils.Utils.DateFormat);

            var cursor = db.ExecuteQuery(query);

            if (cursor.Count == 0)
                return challangeDtos;

            while (cursor.MoveToNext())
            {
                var challenge = new ChallengeDto
                {
                    Id = cursor.GetInt(0),
                    Name = cursor.GetString(1),
                    Duration = cursor.GetInt(2),
                    EntriesCompleted = cursor.GetInt(3),
                    LastEntryDate = Utils.Utils.ToDateTimeNull(cursor.GetString(4)),
                    Frequency = new Frequency
                    {
                        Id = cursor.GetInt(5),
                        Value = cursor.GetInt(6),
                        Type = Utils.Utils.ToFrequencyType(cursor.GetInt(7)),
                    }
                };

                challangeDtos.Add(challenge);
            }

            cursor.Dispose();
            db.Close();

            return challangeDtos;
        }

        public List<Frequency> GetPredefinedFrequencies()
        {
            var frequencyList = new List<Frequency>();
            db.Open();

            var fetchColumns = new string[] { "FREQUENCY_ID", "VALUE", "FREQUENCY_TYPE_ID" };
            var whereClause = string.Format("FREQUENCY_TYPE_ID = {0}", (int) FrequencyType.Predefined);

            var cursor = db.GetFromTable("FREQUENCY", fetchColumns, whereClause);

            if (cursor.Count == 0)
                return null;

            while (cursor.MoveToNext())
            {
                var frequency = new Frequency
                {
                    Id = cursor.GetInt(0),
                    Value = cursor.GetInt(1),
                    Type = Utils.Utils.ToFrequencyType(cursor.GetInt(3)),
                };

                frequencyList.Add(frequency);
            }

            cursor.Dispose();
            db.Close();

            return frequencyList;
        }

        public ChallengeDto GetChallange(int challengeId)
        {
            var challangeDto = new ChallengeDto();
            db.Open();

            var fetchColumns = new string[] { "CHALLENGE_ID", "NAME", "DURATION" };
            var whereClause = string.Format("CHALLENGE_ID = {0}", challengeId);

            var cursor = db.GetFromTable("CHALLENGE", fetchColumns, whereClause);

            if (cursor.Count == 0)
                return null;

            cursor.MoveToFirst();

            return new ChallengeDto
            {
                Id = cursor.GetInt(0),
                Name = cursor.GetString(1),
                Duration = cursor.GetInt(2),
            };
        }

        public bool InsertChallengeEntry(int challengeId, DateTime entryDate)
        {
            db.Open();

            ContentValues values = new ContentValues();
            values.Put("CHALLENGE_ID", challengeId);
            values.Put("ENTRY_DATE", entryDate.ToString(Utils.Utils.DateFormat));

            var ret = db.Insert("CHALLENGE_ENTRY", values) > 0;

            db.Close();

            return ret;
        }
    }
}