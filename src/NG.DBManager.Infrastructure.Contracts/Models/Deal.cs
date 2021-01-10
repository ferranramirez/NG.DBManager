using System;
using System.ComponentModel.DataAnnotations;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Deal
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string BusinessMessage { get; set; }
        public string Conditions { get; set; }
        public string UserMessage { get; set; }

        public DealType DealType { get; set; }

        public Guid? DealTypeId { get; set; }
    }
}