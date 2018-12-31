// Authors: Damienne DUFAUX - Arthur MATHIEU
// Editors: Tinaël DEVRESSE - Guillaume SERVAIS

using System.Linq;

namespace DAL {
    public static class IExtensionIEnumerable {
        public static IQueryable<T> TakePage<T> (this IQueryable<T> query, int? pageIndex = 0, int? pageSize = 5) {
            return query
                .Take (pageSize.Value)
                .Skip (pageIndex.Value * pageSize.Value);
        }
    }
}