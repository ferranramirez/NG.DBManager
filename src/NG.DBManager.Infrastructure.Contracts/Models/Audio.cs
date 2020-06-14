using System;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Audio
    {
        public Audio()
        {
            //AudioImages = new HashSet<AudioImage>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid NodeId { get; set; }
        // public IEnumerable<AudioImage> AudioImages { get; set; }
    }
}