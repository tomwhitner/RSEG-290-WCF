using System.ServiceModel;

namespace MyWCFServices.RealNorthwindService
{
    [ServiceContract]
    public interface ICategoryService
    {
        [OperationContract]
        Category GetCategory(int id);

        [OperationContract]
        [FaultContract(typeof(CategoryFault))]
        void UpdateCategory(Category category);
    }
}