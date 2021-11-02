using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    [DataContract]
    public class PageFileDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int LocaleId { get; set; }
        [DataMember]
        public int PageId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string FileUrl { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public string Lang { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; }
    }
}
