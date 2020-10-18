using System;
using System.ComponentModel.DataAnnotations;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class DealType
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DealType dealType
                && Id.Equals(dealType.Id);
        }

        public override int GetHashCode()
        {
            return new { Id }.GetHashCode();
        }
    }
}