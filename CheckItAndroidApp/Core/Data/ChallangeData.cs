using System.Collections.Generic;
using CheckItAndroidApp.Core.Business.Dtos;
using Android.Content;

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

            var cursor = db.GetFromTable("CHALLENGE", new string[] { "CHALLENGE_ID", "NAME", "DURATION" });

            if (cursor.Count == 0)
                return challangeDtos;

            while (cursor.MoveToNext())
            {
                challangeDtos.Add(new ChallangeDto
                {
                    Id = cursor.GetInt(0),
                    Name = cursor.GetString(1),
                    Duration = cursor.GetInt(2),
                });
            }

            cursor.Dispose();
            db.Close();

            return challangeDtos;
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
    }
}