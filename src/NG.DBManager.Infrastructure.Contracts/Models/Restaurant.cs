using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Restaurant
    {
        [Key]
        [ForeignKey(nameof(Commerce))]
        public Guid CommerceId { get; set; }

        public Commerce Commerce { get; set; }
    }
}