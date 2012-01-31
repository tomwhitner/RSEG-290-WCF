using System.ServiceModel;

namespace ZipCodeService
{
    [ServiceContract]
    public interface IZipCodeService
    {
        [OperationContract]
        string Lookup(string caller, string zipcode);
    }
}