using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    [DataContract]
    public class ApplicationDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string HTML { get; set; }
        [DataMember]
        public string Form { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; }
    }
}
