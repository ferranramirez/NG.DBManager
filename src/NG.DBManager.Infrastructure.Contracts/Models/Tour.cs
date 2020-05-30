using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool IsPremium { get; set; }
        public bool IsFeatured { get; set; }

        public Guid ImageId { get; set; }

        public IEnumerable<Node> Nodes { get; set; }
        public IEnumerable<TourTag> TourTags { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Tour tour
                && Id.Equals(tour.Id);
        }

        public override int GetHashCode()
        {
            return new { Id }.GetHashCode();
        }
    }
}
