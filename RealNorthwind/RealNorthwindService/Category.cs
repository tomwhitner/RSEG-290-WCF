using System.Runtime.Serialization;

namespace MyWCFServices.RealNorthwindService
{
    /// <summary>
    /// Data transfer object for the category service
    /// </summary>
    [DataContract]
    public class Category
    {
        /// <summary>
        /// Retruns the category's ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// Retruns the category's name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Retruns the category's description
        /// </summary>
        [DataMember]
        public string Description { get; set; }
    }
}