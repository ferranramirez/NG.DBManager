using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Library.EF
{
    public class Node
    {
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public int Order { get; set; }


        public Guid PlaceId { get; set; }
        public Guid TourId { get; set; }

        public Place Place { get; set; }
    }
}
