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
        private const int MaxCategoryID = 18;
        private readonly CategoryDAO _categoryDAO = new CategoryDAO();

        public Category GetCategory(int id)
        {
            if ((id < MinCategoryID) || (id > MaxCategoryID))
            {
                throw new ArgumentOutOfRangeException("id");
            }

            Category category = null;

            try
            {
                category = _categoryDAO.GetCategory(id);  
            }
            catch (ArgumentException ex)
            {
                throw new DataException(ex.Message, ex); 
            }

            return category;
        }

        public void UpdateCategory(Category category)
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

            if (!_categoryDAO.UpdateCategory(category))
            {
                throw new DataException(Resources.MSG_UPDATE_FAILED);
            }
        }
    }
}