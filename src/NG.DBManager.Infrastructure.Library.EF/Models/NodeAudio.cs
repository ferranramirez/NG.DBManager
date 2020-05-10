using System;

namespace NG.DBManager.Infrastructure.Library.EF
{
    public class NodeAudio
    {
        public int Order { get; set; }

        public Guid NodeId { get; set; }
        public Guid AudioId { get; set; }

        public Node Node { get; set; }
        public Audio Audio { get; set; }
    }
}
