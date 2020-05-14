using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Coupon
    {
        public Guid Id { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }
        public DateTime Redemption { get; set; }

        public User User { get; set; }
        public Commerce Commerce { get; set; }

        public Guid UserId { get; set; }
        public Guid CommerceId { get; set; }

        public bool IsRedeemed => Redemption != default;
    }
}