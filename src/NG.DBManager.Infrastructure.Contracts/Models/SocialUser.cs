using NG.DBManager.Infrastructure.Contracts.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class SocialUser
    {
        public Guid UserId { get; set; }
        
        public string Provider { get; set; }
        
        public Guid SocialId { get; set; }

        public User User { get; set; }
    }
}
