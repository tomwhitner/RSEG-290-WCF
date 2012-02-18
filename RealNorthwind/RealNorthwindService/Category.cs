using System.Runtime.Serialization;

namespace MyWCFServices.RealNorthwindService
{
    [DataContract]
    public class Category
    {
        [DataMember]
        public int CategoryID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
