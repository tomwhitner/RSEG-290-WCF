using System.Runtime.Serialization;

namespace MyWCFServices.RealNorthwindService
{
    /// <summary>
    /// The category fault is used to communicate failure to clients of the category service
    /// </summary>
    [DataContract]
    public class CategoryFault
    {
        /// <summary>
        /// Describes the reason for the service failure
        /// </summary>
        [DataMember] 
        public string FaultMessage;

        /// <summary>
        /// Construct a new category fault
        /// </summary>
        /// <param name="msg">The description of the service fault</param>
        public CategoryFault(string msg)
        {
            FaultMessage = msg;
        }
    }
}