using System;
using System.Data;
using MyWCFServices.RealNorthwindEntities;
using RealNorthwindDAL;

namespace RealNorthwindLogic
{
    public class CategoryLogic
    {
        readonly CategoryDAO _categoryDAO = new CategoryDAO();

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

            if (string.IsNullOrEmpty(category.Name))
            {
                throw new NoNullAllowedException("Category name cannot be null.");  // make resource
            }

            if (string.IsNullOrEmpty(category.Description))
            {  
                throw new NoNullAllowedException("Category description cannot be null.");  // make resource
            }

            return _categoryDAO.UpdateCategory(category);
        }
    }
}
