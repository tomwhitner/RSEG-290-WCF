using System;
using System.Data;
using MyWCFServices.RealNorthwindDAL;
using MyWCFServices.RealNorthwindEntities;
using RealNorthwindLogic.Properties;

namespace MyWCFServices.RealNorthwindLogic
{
    /// <summary>
    /// Implements the business logic layer for the category service
    /// </summary>
    public class CategoryLogic
    {
        private const int MinCategoryID = 1;
        private const int MaxCategoryID = 8;
        private readonly CategoryDAO _categoryDAO = new CategoryDAO();

        public CategoryEntity GetCategory(int id)
        {
            if ((id < MinCategoryID) || (id > MaxCategoryID))
            {
                throw new ArgumentOutOfRangeException("id");
            }

            return _categoryDAO.GetCategory(id);
        }

        public void UpdateCategory(CategoryEntity category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            if (string.IsNullOrEmpty(category.CategoryName))
            {
                throw new NoNullAllowedException(Resources.MSG_NULL_CAT_NAME);
            }

            if (string.IsNullOrEmpty(category.Description))
            {
                throw new NoNullAllowedException(Resources.MSG_NULL_CAT_DESC);
            }

            _categoryDAO.UpdateCategory(category);
        }
    }
}