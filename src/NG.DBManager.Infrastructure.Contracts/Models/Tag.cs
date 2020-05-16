using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(40)")]
        public string Name { get; set; }
    }
}