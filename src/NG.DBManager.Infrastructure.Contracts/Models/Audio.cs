using System;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class Audio
    {
        public Audio()
        {
            //AudioImages = new List<AudioImage>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public Guid NodeId { get; set; }
        // public IList<AudioImage> AudioImages { get; set; }
    }
}