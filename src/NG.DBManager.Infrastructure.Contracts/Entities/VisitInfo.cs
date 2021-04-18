using NG.DBManager.Infrastructure.Contracts.Models;
using System;

namespace NG.DBManager.Infrastructure.Contracts.Entities
{
    public class VisitInfo
    {
        public TourInfo TourInfo { get; set; }
        public Deal Deal { get; set; }
        public DateTime RegistryDate { get; set; }
        public UserInfo UserInfo { get; set; }
        public bool IsSelfValidated { get; set; }
    }
}
