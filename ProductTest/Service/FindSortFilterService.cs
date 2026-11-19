using ProductTest.Helper;

namespace ProductTest.Service
{
    public class FindSortFilterService
    {
        public List<ProductView> Apply(IEnumerable<ProductView> products, string? search, string? category, int sortIndex)
        {
            var result = Sort(Filter(Find(products, search), category), sortIndex);
            return result.ToList();
        }

        private IEnumerable<ProductView> Find(IEnumerable<ProductView> list, string? search)
        {
            search = search?.Trim().ToLower();
            if (string.IsNullOrEmpty(search)) return list;

            return list.Where(p =>
                (p.Name != null && p.Name.Contains(search) ||
                (p.Description != null && p.Description.Contains(search))));
        }

        private IEnumerable<ProductView> Filter(IEnumerable<ProductView> list, string? category)
        {
            if (category == null) return list;
            return list.Where(p => p.Category == category);
        }

        private IEnumerable<ProductView> Sort(IEnumerable<ProductView> list, int sortIndex)
        {
            if (sortIndex == 1) return list.OrderByDescending(p => p.DiscountPrice);
            if (sortIndex == 2) return list.OrderBy(p => p.DiscountPrice);
            return list;
        }
    }
}