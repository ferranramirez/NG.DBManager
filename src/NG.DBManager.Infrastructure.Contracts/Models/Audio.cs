using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Audio
    {
        public Audio()
        {
            //AudioImages = new HashSet<AudioImage>();
        }

        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        public Guid NodeId { get; set; }
        // public IEnumerable<AudioImage> AudioImages { get; set; }
    }
}