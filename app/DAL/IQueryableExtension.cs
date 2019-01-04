// Authors: Damienne DUFAUX - Arthur MATHIEU
// Editors: Tinaël DEVRESSE - Guillaume SERVAIS

using System.Linq;

namespace DAL
{
    public static class IExtensionIEnumerable
    {
        public static IQueryable<T> TakePage<T>(this IQueryable<T> query, int? pageIndex = Model.Constants.Page.Index, int? pageSize = Model.Constants.Page.Size)
        {
            var obj= query.Skip(pageIndex.Value * pageSize.Value).Take(pageSize.Value);
            return obj;
        }
    }
}