using System;
using System.ComponentModel.DataAnnotations;

namespace NG.DBManager.Infrastructure.Library.EF
{
    public class Restaurant
    {
        [Key]
        public Guid PlaceId { get; set; }

        public Place Place { get; set; }
    }
}