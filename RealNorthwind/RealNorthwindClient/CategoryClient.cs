using System;
using System.ServiceModel;
using RealNorthwindClient.CategoryServiceRef;

namespace RealNorthwindClient
{
    internal class CategoryClient
    {
        private static void Main(string[] args)
        {
            TestGetCategory(0);
            TestGetCategory(1);
            TestGetCategory(9);

            TestUpdateCategory(1, null, "valid descripion");
            TestUpdateCategory(1, "valid name", null);
            TestUpdateCategory(1, "New name", "New descripion");
            TestUpdateCategory(9, "New name", "New descripion");

            Console.ReadKey();
        }

        /// <summary>
        /// Attempts to get a category and then outputs results to console.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve</param>
        private static void TestGetCategory(int id)
        {
            var catService = new CategoryServiceClient();
            try
            {
                Console.WriteLine(string.Format("\nAttempting to retrieve category {0}.", id));
                Category cat = catService.GetCategory(id);
                Console.WriteLine(string.Format(" - Category Retrieved: ID = {0}, Name = {1}, Description = {2}", cat.ID, cat.Name,
                                                cat.Description));
            }
            catch (FaultException<CategoryFault> ex)
            {
                Console.WriteLine(string.Format(" - Get Exception: {0}", ex.Detail.FaultMessage));
            }
            catch (FaultException ex)
            {
                Console.WriteLine(string.Format(" - Get Exception: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Attempts to update the specified category.  Prints results to console.
        /// </summary>
        /// <param name="id">The ID of the category to update</param>
        /// <param name="name">The new name</param>
        /// <param name="description">The new description</param>
        private static void TestUpdateCategory(int id, string name, string description)
        {
            var catService = new CategoryServiceClient();
            try
            {
                Console.WriteLine(string.Format("\nAttempting to update category {0}.", id));
                if (name == null)
                {
                    Console.WriteLine(string.Format(" - Name = null."));
                }
                if (description == null)
                {
                    Console.WriteLine(string.Format(" - Description = null."));
                }
                var cat = new Category
                              {
                                  ID = id,
                                  Name = name,
                                  Description = description
                              };
                catService.UpdateCategory(cat);
                Console.WriteLine(string.Format(" - Category Updated: ID = {0}, Name = {1}, Description = {2}", cat.ID, cat.Name,
                                                cat.Description));
            }
            catch (FaultException<CategoryFault> ex)
            {
                Console.WriteLine(string.Format(" - Update Exception: {0}", ex.Detail.FaultMessage));
            }
            catch (FaultException ex)
            {
                Console.WriteLine(string.Format(" - Update Exception: {0}", ex.Message));
            }
        }
    }
}