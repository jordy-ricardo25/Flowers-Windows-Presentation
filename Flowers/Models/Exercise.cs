using System.Runtime.Serialization;

namespace Flowers.Models
{
    [DataContract]
    public sealed class Exercise
    {
        [DataMember]
        public string Title
        {
            get;
            set;
        }

        [DataMember]
        public string Description
        {
            get;
            set;
        }

        [DataMember]
        public int Index
        {
            get;
            set;
        }
    }
}
