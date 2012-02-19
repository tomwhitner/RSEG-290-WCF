using System;
using System.Data;
using System.ServiceModel;
using MyWCFServices.RealNorthwindEntities;
using MyWCFServices.RealNorthwindLogic;

namespace MyWCFServices.RealNorthwindService
{
    /// <summary>
    /// The Category Service provides operations to retrieve and update category details
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly CategoryLogic _categoryLogic = new CategoryLogic();

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
                CategoryEntity categoryEntity = _categoryLogic.GetCategory(id);
                return TranslateCategoryEntityToCategoryContractData(categoryEntity);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new FaultException<CategoryFault>(new CategoryFault(ex.Message), "Category Fault");
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
                CategoryEntity categoryEntity = TranslateCategoryContractDataToCategoryEntity(category);
                _categoryLogic.UpdateCategory(categoryEntity);
            }
            catch (NoNullAllowedException ex)
            {
                throw new FaultException<CategoryFault>(new CategoryFault(ex.Message), "Category Fault");
            }
        }

        #endregion

        private Category TranslateCategoryEntityToCategoryContractData(
            CategoryEntity categoryEntity)
        {
            return new Category
                       {
                           ID = categoryEntity.CategoryID,
                           Name = categoryEntity.CategoryName,
                           Description = categoryEntity.Description
                       };
        }

        private CategoryEntity TranslateCategoryContractDataToCategoryEntity(
            Category category)
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