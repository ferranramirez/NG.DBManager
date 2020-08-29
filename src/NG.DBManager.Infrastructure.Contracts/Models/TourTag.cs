using System;
using System.Text.Json.Serialization;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class TourTag
    {
        public Guid TourId { get; set; }
        public Guid TagId { get; set; }

        [JsonIgnore]
        public Tour Tour { get; set; }
        [JsonIgnore]
        public Tag Tag { get; set; }
    }
}
