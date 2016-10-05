using System;
using static CheckItAndroidApp.Core.Data.Utils.Enums;

namespace CheckItAndroidApp.Core.Business.Dtos
{
    public class ChallengeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public Frequency Frequency { get; set; }
        public int EntriesCompleted { get; set; }
        public DateTime? LastEntryDate { get; set; }
        public bool CanCheck
        {
            get
            {
                return (EntriesCompleted == 0 && Duration > 0 ||
                    (LastEntryDate.HasValue && DateTime.Today > LastEntryDate.Value.Date));
            }
        }
        public bool IsCompleted
        {
            get
            {
                return EntriesCompleted == Duration;
            }
        }

        public static ChallengeDto New(string name, int duration)
        {
            return new ChallengeDto
            {
                Name = name,
                Duration = duration,
                EntriesCompleted = 0,
                LastEntryDate = null,
            };
        }
    }
}