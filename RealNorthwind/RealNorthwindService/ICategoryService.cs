using System.ServiceModel;

namespace MyWCFServices.RealNorthwindService
{
    /// <summary>
    /// The Category Service provides operations to retrieve and update category details
    /// </summary>
    [ServiceContract]
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieves the specified category
        /// </summary>
        /// <param name="id">The id of the category to retrieve</param>
        /// <returns>The requested category</returns>
        [OperationContract]
        [FaultContract(typeof (CategoryFault))]
        Category GetCategory(int id);

        /// <summary>
        /// Updates the specified category
        /// </summary>
        /// <param name="category">The category to update</param>
        [OperationContract]
        [FaultContract(typeof (CategoryFault))]
        bool UpdateCategory(Category category);
    }
}