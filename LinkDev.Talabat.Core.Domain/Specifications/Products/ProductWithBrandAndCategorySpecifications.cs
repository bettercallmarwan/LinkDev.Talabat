using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId)
            : base(
                  //p =>
                  //      (!brandId.HasValue || p.BrandId == brandId.Value)
                  //          &&
                  //      (!categoryId.HasValue || p.CategoryId == categoryId.Value)

                  p =>
                        ((brandId.HasValue) ? p.BrandId == brandId.Value : true)
                            &&
                        ((categoryId.HasValue) ? p.CategoryId == categoryId.Value : true)
                  )
        {
            AddIncludes();

            AddOrderBy(p => p.Name);


            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "nameDesc":
                        AddOrderByDesc(p => p.Name);
                        break;
                    case "priceAsc":    
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

            if(brandId is not null)
            {
                 
            }
        }


        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            AddIncludes();
        }

        private protected override void AddIncludes()
        {
            base.AddIncludes();

            Includes.Add(p => p.Brand!);
            Includes.Add(p => p.Category!);

        }


    }
}
