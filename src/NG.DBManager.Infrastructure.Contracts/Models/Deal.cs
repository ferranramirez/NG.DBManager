﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Deal
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}