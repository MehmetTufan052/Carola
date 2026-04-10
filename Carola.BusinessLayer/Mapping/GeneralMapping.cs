using AutoMapper;
using Carola.DtoLayer.Dtos.BrandDtos;
using Carola.DtoLayer.Dtos.CustomerDtos;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
                CreateMap<Customer, ResultCustomerDto>().ReverseMap();
                CreateMap<Customer, CreateCustomerDto>().ReverseMap();
                CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
                CreateMap<Customer, GetCustomerByIdDto>().ReverseMap();

                CreateMap<Brand, ResultBrandDto>().ReverseMap();
                CreateMap<Brand, CreateBrandDto>().ReverseMap();
                CreateMap<Brand, UpdateBrandDto>().ReverseMap();
                CreateMap<Brand, GetBrandByIdDto>().ReverseMap();
        }
    }
}
