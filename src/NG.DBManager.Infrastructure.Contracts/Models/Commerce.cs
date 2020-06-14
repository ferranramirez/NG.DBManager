﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Commerce
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        public Guid? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}