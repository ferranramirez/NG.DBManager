using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Featured
    {
        [Key]
        [ForeignKey(nameof(Tour))]
        public Guid TourId { get; set; }

        public Tour Tour { get; set; }
    }
}
