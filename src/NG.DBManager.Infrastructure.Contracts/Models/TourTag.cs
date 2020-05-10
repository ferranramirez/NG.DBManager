using System;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class TourTag
    {
        public Guid TourId { get; set; }
        public Guid TagId { get; set; }

        public Tour Tour { get; set; }
        public Tag Tag { get; set; }
    }
}
