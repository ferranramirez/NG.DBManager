using NG.DBManager.Infrastructure.Contracts.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(70)")]
        public string Surname { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(254)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }

        public virtual Commerce Commerce { get; set; }
        public virtual Image Image { get; set; }


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
