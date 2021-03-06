using System;
using System.Text.Json.Serialization;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Coupon
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime ValidationDate { get; set; }
        public DateTime GenerationDate { get; set; }

        public User User { get; set; }

        [JsonIgnore]
        public User Validator { get; set; }
        public Node Node { get; set; }

        public Guid UserId { get; set; }
        public Guid NodeId { get; set; }

        public Guid? ValidatorId { get; set; }

        public bool IsValidated => ValidationDate != default;
        public bool IsSelfValidated => ValidatorId == UserId;

        public override bool Equals(object obj)
        {
            return obj is Coupon coupon
                && Id.Equals(coupon.Id);
        }

        public override int GetHashCode()
        {
            return new { Id }.GetHashCode();
        }
    }
}