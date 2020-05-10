using System;

namespace NG.DBManager.Infrastructure.Contracts.Models
{
    public class NodeImage
    {
        public Guid NodeId { get; set; }
        public Guid ImageId { get; set; }

        public Node Node { get; set; }
        public Image Image { get; set; }
    }
}
