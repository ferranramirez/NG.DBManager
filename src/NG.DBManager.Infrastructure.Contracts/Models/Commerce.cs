using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Commerce
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        public Guid? UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        public IList<CommerceDeal> CommerceDeals { get; set; }
    }
}