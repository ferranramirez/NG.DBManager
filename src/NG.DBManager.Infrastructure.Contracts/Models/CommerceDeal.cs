using System;
using System.Text.Json.Serialization;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class CommerceDeal
    {
        public Guid CommerceId { get; set; }
        public Guid DealId { get; set; }

        [JsonIgnore]
        public Commerce Commerce { get; set; }
        public Deal Deal { get; set; }
    }
}