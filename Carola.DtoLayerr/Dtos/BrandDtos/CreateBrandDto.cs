using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.DtoLayer.Dtos.BrandDtos
{
    public class CreateBrandDto
    {
        public string BrandName { get; set; }
        public string LogoUrl { get; set; }
        public bool Status { get; set; }
    }
}
