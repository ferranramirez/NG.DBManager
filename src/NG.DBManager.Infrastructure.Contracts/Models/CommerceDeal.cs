using System;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class CommerceDeal
    {
        public Guid CommerceId { get; set; }
        public Guid DealId { get; set; }

        public Commerce Commerce { get; set; }
        public Deal Deal { get; set; }
    }
}