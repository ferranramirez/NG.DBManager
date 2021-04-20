using System;
using System.Text.Json.Serialization;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Visit
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public Guid CommerceId { get; set; }
        [JsonIgnore]
        public Commerce Commerce { get; set; }

        public Guid TourId { get; set; }
        [JsonIgnore]
        public Tour Tour { get; set; }

        public DateTime RegistryDate { get; set; }
    }
}
