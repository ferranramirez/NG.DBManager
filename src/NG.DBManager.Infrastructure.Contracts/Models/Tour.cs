using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Tour
    {
        public Tour()
        {
            Nodes = new HashSet<Node>();
            TourTags = new HashSet<TourTag>();
        }

        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool IsPremium { get; set; }

        public Guid FeaturedId { get; set; }
        public Featured Featured { get; set; }
        public Image Image { get; set; }
        public IEnumerable<Node> Nodes { get; set; }
        public IEnumerable<TourTag> TourTags { get; set; }
    }
}
