using System;
using System.ComponentModel.DataAnnotations;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Restaurant
    {
        [Key]
        public Guid LocationId { get; set; }
    }
}