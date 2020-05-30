using System;
using System.ComponentModel.DataAnnotations;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Review
    {
        [Required]
        public int Score { get; set; }

        public Guid UserId { get; set; }
        public Guid TourId { get; set; }

        public User User { get; set; }
        public Tour Tour { get; set; }

    }
}
