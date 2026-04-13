using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.DtoLayer.Dtos.LocationDtos
{
    public class ResultLocationDto
    {
        public int LocationId { get; set; }
        public string AuthorizedPerson { get; set; }
        public string LocationName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
