using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecifications() : base()
        {
            Includes.Add(p => p.Brand!);
            Includes.Add(p => p.Category!);
        }


        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            Includes.Add(p => p.Brand!);
            Includes.Add(p => p.Category!);
        }


        private void AddIncludes()
        {
            Includes.Add(p => p.Brand!);
            Includes.Add(p => p.Category!);
        }

    }
}
