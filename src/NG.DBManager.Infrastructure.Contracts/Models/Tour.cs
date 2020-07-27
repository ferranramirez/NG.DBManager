using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Tour
    {
        public Tour()
        {
            Nodes = new List<Node>();
            TourTags = new List<TourTag>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string GeoJson { get; set; }
        public int Duration { get; set; }
        public bool IsPremium { get; set; }
        public bool IsFeatured { get; set; }

        public Guid? ImageId { get; set; }

        [JsonIgnore]
        public virtual Image Image { get; set; }

        [JsonIgnore]
        public DateTime Created { get; set; }

        public IList<Node> Nodes { get; set; }
        public IList<TourTag> TourTags { get; set; }

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
