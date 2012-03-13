using System;
using System.Data;
using System.ServiceModel;
using MyWCFServices.RealNorthwindLogic;
using CategoryEntity = MyWCFServices.RealNorthwindEntities.Category;

namespace MyWCFServices.RealNorthwindService
{
    /// <summary>
    /// The Category Service provides operations to retrieve and update category details
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly CategoryLogic _categoryLogic = new CategoryLogic();
        private const String FaultSource = "Category Fault";

        #region ICategoryService Members

        /// <summary>
        /// Retrieves the specified category
        /// </summary>
        /// <param name="id">The id of the category to retrieve</param>
        /// <returns>The requested category</returns>
        public Category GetCategory(int id)
        {
            try
            {
                var category = _categoryLogic.GetCategory(id);
                return TranslateCategoryEntityToCategoryContractData(category);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new FaultException<CategoryFault>(new CategoryFault(ex.Message), FaultSource);
            }
            catch (DataException ex)
            {
                throw new FaultException<CategoryFault>(new CategoryFault(ex.Message), FaultSource);
            }
        }

        /// <summary>
        /// Updates the specified category
        /// </summary>
        /// <param name="category">The category to update</param>
        public void UpdateCategory(Category category)
        {
            try
            {
                CategoryEntity c = TranslateCategoryContractDataToCategoryEntity(category);
                _categoryLogic.UpdateCategory(c);
            }
            catch (NoNullAllowedException ex)
            {
                 throw new FaultException<CategoryFault>(new CategoryFault(ex.Message), FaultSource);
            }
            catch (DataException ex)
            {
                 throw new FaultException<CategoryFault>(new CategoryFault(ex.Message), FaultSource);
            }
        }

        #endregion

        private Category TranslateCategoryEntityToCategoryContractData(CategoryEntity category)
        {
            return new Category
            {
                ID = category.CategoryID,
                Name = category.CategoryName,
                Description = category.Description
            };
        }

        private CategoryEntity TranslateCategoryContractDataToCategoryEntity(Category category)
        {
            return new CategoryEntity
            {
                CategoryID = category.ID,
                CategoryName = category.Name,
                Description = category.Description
            };
        }
    }
}