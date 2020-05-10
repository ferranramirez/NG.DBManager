using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Node
    {
        public Node()
        {
            NodeAudios = new HashSet<NodeAudio>();
            NodeImages = new HashSet<NodeImage>();
        }

        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public int Order { get; set; }


        public Guid CoordinatesId { get; set; }
        public Guid TourId { get; set; }

        public Location Location { get; set; }
        public IEnumerable<NodeAudio> NodeAudios { get; set; }
        public IEnumerable<NodeImage> NodeImages { get; set; }
    }
}
