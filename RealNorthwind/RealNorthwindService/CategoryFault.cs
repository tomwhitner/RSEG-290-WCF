using System.Runtime.Serialization;

namespace MyWCFServices.RealNorthwindService
{
    [DataContract]
    public class CategoryFault
    {
        public CategoryFault(string msg)
        {
            FaultMessage = msg;
        }

        [DataMember] 
        public string FaultMessage;
    }
}
