using System.ServiceModel;

namespace ZipCodeService
{
    /// <summary>
    /// Defines the interface for the ZipCode Service
    /// </summary>
    [ServiceContract]
    public interface IZipCodeService
    {
        /// <summary>
        /// Looks us a zipcode for authorized users
        /// </summary>
        /// <param name="caller">The user; will verify caller is autorized</param>
        /// <param name="zipcode">The zipcode to lookup</param>
        /// <returns>City, state for specified zipcode or error description.</returns>
        [OperationContract]
        string Lookup(string caller, string zipcode);
    }
}