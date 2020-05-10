using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Library.EF
{
    public class Place
    {
        public Place()
        {
            Nodes = new HashSet<Node>();
        }

        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal Longitude { get; set; }

        public Restaurant Restaurant { get; set; }
        public IEnumerable<Node> Nodes { get; set; }

        public bool IsRestaurant()
        {
            return !Restaurant.Equals(null);
        }
    }
}