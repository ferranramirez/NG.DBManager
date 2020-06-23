using NG.DBManager.Infrastructure.Contracts.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }

        public Commerce Commerce { get; set; }
        public Image Image { get; set; }


        public override bool Equals(object obj)
        {
            return obj is User user
                && Id.Equals(user.Id);
        }

        public override int GetHashCode()
        {
            return new { Id }.GetHashCode();
        }
    }
}
