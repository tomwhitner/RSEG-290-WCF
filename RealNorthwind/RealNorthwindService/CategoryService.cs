using System;
using System.ServiceModel;
using MyWCFServices.RealNorthwindEntities;
using MyWCFServices.RealNorthwindLogic;
using RealNorthwindService.Properties;

namespace MyWCFServices.RealNorthwindService
{
    /// <summary>
    /// The Category Service provides operations to retrieve and update category details
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private const String FaultSource = "Category Fault";
        private readonly CategoryLogic _categoryLogic = new CategoryLogic();

        #region ICategoryService Members

        /// <summary>
        /// Retrieves the specified category
        /// </summary>
        /// <param name="id">The id of the category to retrieve</param>
        /// <returns>The requested category</returns>
        public Category GetCategory(int id)
        {
            CategoryEntity category;

            try
            {
                category = _categoryLogic.GetCategory(id);
            }
            catch (Exception ex)
            {
                throw new FaultException<CategoryFault>(new CategoryFault(ex.Message), FaultSource);
            }
            if (category == null)
            {
                throw new FaultException<CategoryFault>(new CategoryFault(Resources.MSG_CAT_NOT_EXISTS), FaultSource);
            }
            return TranslateCategoryEntityToCategoryContractData(category);
        }

        /// <summary>
        /// Updates the specified category
        /// </summary>
        /// <param name="category">The category to update</param>
        public bool UpdateCategory(Category category)
        {
            try
            {
                CategoryEntity c = TranslateCategoryContractDataToCategoryEntity(category);
                return _categoryLogic.UpdateCategory(c);
            }
            catch (Exception ex)
            {
                throw new FaultException<CategoryFault>(new CategoryFault(ex.Message), FaultSource);
            }
        }

        #endregion

        #region Translation methods

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
        #endregion
    }
}