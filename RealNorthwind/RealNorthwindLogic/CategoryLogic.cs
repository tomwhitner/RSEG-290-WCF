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
            return _categoryDAO.GetCategory(id);
        }

        public bool UpdateCategory(CategoryEntity category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }
            /*
            if (string.IsNullOrEmpty(category.Description))
            {
                throw new NoNullAllowedException(Resources.MSG_NULL_CAT_DESC);
            }
            */
            return _categoryDAO.UpdateCategory(category);
        }
    }
}