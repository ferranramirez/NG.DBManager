﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Location
    {
        public Location()
        {
            Nodes = new HashSet<Node>();
        }

        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(9, 6)")]
        public decimal Latitude { get; set; }

        [Required]
        [Column(TypeName = "decimal(9, 6)")]
        public decimal Longitude { get; set; }

        public IEnumerable<Node> Nodes { get; set; }

    }
}