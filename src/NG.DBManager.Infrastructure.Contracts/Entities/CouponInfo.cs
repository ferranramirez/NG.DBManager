using NG.DBManager.Infrastructure.Contracts.Models;
using System;

namespace NG.DBManager.Infrastructure.Contracts.Entities
{
    public class CouponInfo
    {
        public TourInfo TourInfo { get; set; }
        public DateTime ValidationDate { get; set; }
        public DealType DealType { get; set; }
        public string UserName { get; set; }
        public bool IsSelfValidated { get; set; }
    }
}
