using NG.DBManager.Infrastructure.Contracts.Models;
using System.Collections.Generic;
using System.Reflection;

namespace NG.DBManager.Infrastructure.Contracts.Entities
{
    public class TourWithDealType : Tour
    {
        public TourWithDealType(Tour p)
        {
            foreach (FieldInfo prop in p.GetType().GetFields())
                GetType().GetField(prop.Name).SetValue(this, prop.GetValue(p));

            foreach (PropertyInfo prop in p.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(p, null), null);
        }

        public IEnumerable<DealType> DealTypes { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TourWithDealType tour
                && Id.Equals(tour.Id);
        }

        public override int GetHashCode()
        {
            return new { Id }.GetHashCode();
        }
    }
}
