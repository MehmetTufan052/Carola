using AutoMapper;
using Carola.DtoLayer.Dtos.BrandDtos;
using Carola.DtoLayer.Dtos.CarDtos;
using Carola.DtoLayer.Dtos.CategoryDtos;
using Carola.DtoLayer.Dtos.CustomerDtos;
using Carola.DtoLayer.Dtos.LocationDtos;
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

                CreateMap<Category, ResultCategoryDto>().ReverseMap();
                CreateMap<Category, CreateCategoryDto>().ReverseMap();
                CreateMap<Category, UpdateCategoryDto>().ReverseMap();
                CreateMap<Category, GetCategoryByIdDto>().ReverseMap();
                CreateMap<GetCategoryByIdDto, UpdateCategoryDto>().ReverseMap();

                CreateMap<Car, ResultCarDto>().ReverseMap();
                CreateMap<Car, CreateCarDto>().ReverseMap();
                CreateMap<Car, UpdateCarDto>().ReverseMap();
                CreateMap<Car, GetCarByIdDto>().ReverseMap();

                CreateMap<Location, ResultLocationDto>().ReverseMap();
                CreateMap<Location, CreateLocationDto>().ReverseMap();
                CreateMap<Location, UpdateLocationDto>().ReverseMap();
                CreateMap<Location, GetLocationByIdDto>().ReverseMap();
                CreateMap<GetLocationByIdDto, UpdateLocationDto>().ReverseMap();
        }
    }
}
