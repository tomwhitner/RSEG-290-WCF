using System;
using MyWCFServices.RealNorthwindEntities;
using System.Linq;
using RealNorthwindDAL.Properties;

namespace MyWCFServices.RealNorthwindDAL
{
    /// <summary>
    /// Implements the data access layer for the category service
    /// </summary>
    public class CategoryDAO
    {
        private readonly NorthwindEntities _nwEntities = new NorthwindEntities();

        public Category GetCategory(int id)
        {
            var categories = from c in _nwEntities.Categories
                                where c.CategoryID == id
                                select c;

            var category = categories.SingleOrDefault();

            // 1. Test if the passed in entity data object is a valid category in database 
            if (category == null)
            {
                throw new ArgumentException(Resources.MSG_NO_SUCH_CATEGORY);
            }

            return category;
        }

        public bool UpdateCategory(Category category)
        {
            // 1. Test if the passed in entity data object is a valid category in database 
            // taken care of in get...

            // 2. Retrieve the entity object from database 
            var temp = GetCategory(category.CategoryID);

            // 3. Update the entity object 
            temp.CategoryName = category.CategoryName;
            temp.CategoryID = category.CategoryID;

            // 4. Submit the changes to database 
            var numRows = _nwEntities.SaveChanges();

            return (numRows == 1);
        }
    }
}