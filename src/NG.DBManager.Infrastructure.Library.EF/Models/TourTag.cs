using System;

namespace NG.DBManager.Infrastructure.Library.EF
{
    public class TourTag
    {
        public Guid TourId { get; set; }
        public Guid TagId { get; set; }

        public Tour Tour { get; set; }
        public Tag Tag { get; set; }
    }
}
