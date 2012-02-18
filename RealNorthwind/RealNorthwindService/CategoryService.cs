using System.Data;
using System.ServiceModel;
using MyWCFServices.RealNorthwindEntities;
using RealNorthwindLogic;

namespace MyWCFServices.RealNorthwindService
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryLogic _categoryLogic = new CategoryLogic();

        public Category GetCategory(int id)
        {
            var categoryEntity = _categoryLogic.GetCategory(id);
            return TranslateCategoryEntityToCategoryContractData(categoryEntity);
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                var categoryEntity = TranslateCategoryContractDataToCategoryEntity(category);
                _categoryLogic.UpdateCategory(categoryEntity);
            }
            catch (NoNullAllowedException ex)
            {
                throw new FaultException<CategoryFault>(new CategoryFault(ex.Message), "Category Fault");
            }
        }

        private Category TranslateCategoryEntityToCategoryContractData(
            CategoryEntity categoryEntity)
        {
            return new Category
                       {
                           CategoryID = categoryEntity.CategoryID,
                           Name = categoryEntity.Name,
                           Description = categoryEntity.Description
                       };
        }

        private CategoryEntity TranslateCategoryContractDataToCategoryEntity(
            Category category)
        {
            return new CategoryEntity
                       {
                           CategoryID = category.CategoryID,
                           Name = category.Name,
                           Description = category.Description
                       };
        }
    }
}