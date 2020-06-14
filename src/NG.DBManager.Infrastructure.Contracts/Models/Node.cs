using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Node
    {
        public Node()
        {
            Audios = new HashSet<Audio>();
            Images = new HashSet<Image>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int Order { get; set; }


        public Guid CoordinatesId { get; set; }
        public Guid TourId { get; set; }

        public Location Location { get; set; }
        public IEnumerable<Audio> Audios { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}
