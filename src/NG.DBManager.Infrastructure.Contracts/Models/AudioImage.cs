using System;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class CommerceDeal
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public Guid AudioId { get; set; }
        public Guid ImageId { get; set; }

        public Audio Audio { get; set; }
        public Image Image { get; set; }
    }
}
