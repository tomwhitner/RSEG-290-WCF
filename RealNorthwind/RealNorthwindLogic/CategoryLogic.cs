using System;
using System.Data;
using MyWCFServices.RealNorthwindDAL;
using MyWCFServices.RealNorthwindEntities;
using MyWCFServices.RealNorthwindLogic.Properties;

namespace MyWCFServices.RealNorthwindLogic
{
    /// <summary>
    /// Implements the business logic layer for the category service
    /// </summary>
    public class CategoryLogic
    {
        private readonly CategoryDAO _categoryDAO = new CategoryDAO();

        public CategoryEntity GetCategory(int id)
        {
            var c = _categoryDAO.GetCategory(id);
            return TranslateCategoryDataEntityToCategoryEntity(c);
        }

        public bool UpdateCategory(CategoryEntity category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            if (string.IsNullOrEmpty(category.Description))
            {
                throw new NoNullAllowedException(Resources.MSG_NULL_CAT_DESC);
            }

            var c = TranslateCategoryEntityToCategoryDataEntity(category);
            return _categoryDAO.UpdateCategory(c);
        }

        #region Translation methods

        private Category TranslateCategoryEntityToCategoryDataEntity(
            CategoryEntity categoryEntity)
        {
            return (categoryEntity == null
                        ? null
                        : new Category
                              {
                                  CategoryID = categoryEntity.CategoryID,
                                  CategoryName = categoryEntity.CategoryName,
                                  Description = categoryEntity.Description
                              });
        }

        private CategoryEntity TranslateCategoryDataEntityToCategoryEntity(
            Category category)
        {
            return (category == null
                        ? null
                        :new CategoryEntity
                       {
                           CategoryID = category.CategoryID,
                           CategoryName = category.CategoryName,
                           Description = category.Description
                       });
        }

        #endregion
    }
}