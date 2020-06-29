using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Location
    {
        public Location()
        {
            Nodes = new List<Node>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        public Guid CommerceId { get; set; }

        [ForeignKey(nameof(CommerceId))]
        public Commerce Commerce { get; set; }

        public IList<Node> Nodes { get; set; }

    }
}