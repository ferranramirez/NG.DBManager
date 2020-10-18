using System;
using System.ComponentModel.DataAnnotations;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class DealType
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}