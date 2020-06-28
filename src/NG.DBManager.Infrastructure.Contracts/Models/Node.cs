using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Node
    {
        public Node()
        {
            Audios = new List<Audio>();
            Images = new List<Image>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public Guid TourId { get; set; }

        [Required]
        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        public Guid DealId { get; set; }
        public Deal Deal { get; set; }

        public IList<Audio> Audios { get; set; }
        public IList<Image> Images { get; set; }
    }
}
