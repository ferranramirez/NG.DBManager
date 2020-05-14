using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using System.Linq;

namespace NG.DBManager.Infrastructure.Impl.EF.Extensions
{
    public static class NodeExtension
    {
        public static void DeleteOnCascade(this NgContext Context, Tour entity)
        {
            var entityId = Context.Tour.Find(entity.Id);
            Context.Tour.Remove(entityId);

            var image = Context.Tour.Select(e => e.Image).FirstOrDefault();
            Context.Image.Remove(image);
        }
    }
}
