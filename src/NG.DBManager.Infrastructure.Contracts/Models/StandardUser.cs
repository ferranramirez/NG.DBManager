using NG.DBManager.Infrastructure.Contracts.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class StandardUser
    {

        [Key]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public string Password { get; set; }

        public bool EmailConfirmed { get; set; }
        public User User { get; set; }
    }
}
