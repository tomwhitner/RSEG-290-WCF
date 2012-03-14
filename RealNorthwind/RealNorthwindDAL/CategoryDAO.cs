using System;
using System.Linq;

namespace MyWCFServices.RealNorthwindDAL
{
    /// <summary>
    /// Implements the data access layer for the category service
    /// </summary>
    public class CategoryDAO
    {
        private readonly NorthwindEntities _nwEntities = new NorthwindEntities();

        // 1. Convert the entity object to your entity data object 
        //        v
        public Category GetCategory(int id)
        {
            var categories = from c in _nwEntities.Categories
                                where c.CategoryID == id
                                select c;
            // 2. Return the converted entity data object to business logic layer 
            return categories.SingleOrDefault();
        }

        public bool UpdateCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            // 2. Retrieve the entity object from database 
            var temp = GetCategory(category.CategoryID);

            // 1. Test if the passed in entity data object is a valid category in database 
            if (temp == null)
            {
                return false;
            }
            
            // 3. Update the entity object 
            temp.CategoryName = category.CategoryName;
            temp.CategoryID = category.CategoryID;

            // 4. Submit the changes to database 
            var numRows = _nwEntities.SaveChanges();

            return (numRows == 1);
        }
    }
}