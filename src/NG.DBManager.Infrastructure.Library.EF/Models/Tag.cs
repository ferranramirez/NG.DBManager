using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Library.EF
{
    public class Tag
    {
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }
    }
}