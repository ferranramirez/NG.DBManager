﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Coupon
    {
        public Guid Id { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }
        public DateTime ValidationDate { get; set; }
        public DateTime GenerationDate { get; set; }

        public User User { get; set; }
        public Commerce Commerce { get; set; }

        public Guid UserId { get; set; }
        public Guid CommerceId { get; set; }

        public bool IsValidated => ValidationDate != default;

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