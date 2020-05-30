using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Commerce
    {
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(80)")]
        public string Name { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; }
    }
}