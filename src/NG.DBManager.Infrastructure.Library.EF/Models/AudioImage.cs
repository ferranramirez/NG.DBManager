using System;

namespace NG.DBManager.Infrastructure.Library.EF
{
    public class AudioImage
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public Guid AudioId { get; set; }
        public Guid ImageId { get; set; }

        public Audio Audio { get; set; }
        public Image Image { get; set; }
    }
}
