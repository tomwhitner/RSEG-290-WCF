using System;
using System.Data;
using System.Linq;
using MyWCFServices.RealNorthwindEntities;

namespace MyWCFServices.RealNorthwindDAL
{
    /// <summary>
    /// Implements the data access layer for the category service
    /// </summary>
    public class CategoryDAO
    {
        public CategoryEntity GetCategory(int id)
        {
            var cat = InternalGetCategory(id);

            // 1. Convert the entity object to your entity data object 
            var catEnt = TranslateCategoryDataEntityToCategoryEntity(cat);

            // 2. Return the converted entity data object to business logic layer 
            return catEnt;
        }

        private Category InternalGetCategory(int id)
        {
            using (var nwEntities = new NorthwindEntities())
            {
                IQueryable<Category> categories = from c in nwEntities.Categories
                                                  where c.CategoryID == id
                                                  select c;
                return categories.SingleOrDefault(); 
            }
        }

        public bool UpdateCategory(CategoryEntity category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            using (var nwEntities = new NorthwindEntities())
            {
                // 2. Retrieve the entity object from database 
                var cat = InternalGetCategory(category.CategoryID);

                // 1. Test if the passed in entity data object is a valid category in database 
                if (cat == null)
                {
                    return false;
                }

                // 3. Update the entity object 
                cat.CategoryName = category.CategoryName;
                cat.CategoryID = category.CategoryID;
                cat.Description = category.Description;

                // 4. Submit the changes to database 
                int numRows = nwEntities.SaveChanges();

                return (numRows == 1);
            }
        }

        #region Translation methods

        private static CategoryEntity TranslateCategoryDataEntityToCategoryEntity(
            Category category)
        {
            return (category == null
                        ? null
                        : new CategoryEntity
                        {
                            CategoryID = category.CategoryID,
                            CategoryName = category.CategoryName,
                            Description = category.Description
                        });
        }

        #endregion
    }

    partial class Category
    {
        partial void OnCategoryNameChanging(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new NoNullAllowedException("Category name cannot be null.");
            }
        }
        partial void OnDescriptionChanging(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new NoNullAllowedException("Category description cannot be null.");
            }
        }
    }
}