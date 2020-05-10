using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Library.EF
{
    public class Featured
    {
        public Featured()
        {
            Tours = new HashSet<Tour>();
        }

        [Key]
        public Guid TourId { get; set; }

        [ForeignKey("TourId")]
        public IEnumerable<Tour> Tours { get; set; }
    }
}
