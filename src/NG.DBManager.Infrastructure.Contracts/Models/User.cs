using NG.DBManager.Infrastructure.Contracts.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class User
    {

        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(70)")]
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(254)")]
        public string Email { get; set; }

        [Column(TypeName = "text")]
        public string Password { get; set; }
        public Role Role { get; set; }

        public Image Image { get; set; }
    }
}
