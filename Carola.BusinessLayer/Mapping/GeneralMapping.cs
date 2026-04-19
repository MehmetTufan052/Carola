using AutoMapper;
using Carola.DtoLayer.Dtos.BookingDtos;
using Carola.DtoLayer.Dtos.EmailDtos;
using Carola.DtoLayer.Dtos.BrandDtos;
using Carola.DtoLayer.Dtos.CarDtos;
using Carola.DtoLayer.Dtos.CategoryDtos;
using Carola.DtoLayer.Dtos.CustomerDtos;
using Carola.DtoLayer.Dtos.LocationDtos;
using Carola.DtoLayer.Dtos.ReservationDtos;
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
            CreateMap<GetCarByIdDto, UpdateCarDto>().ReverseMap();

            CreateMap<Location, ResultLocationDto>().ReverseMap();
            CreateMap<Location, CreateLocationDto>().ReverseMap();
            CreateMap<Location, UpdateLocationDto>().ReverseMap();
            CreateMap<Location, GetLocationByIdDto>().ReverseMap();
            CreateMap<GetLocationByIdDto, UpdateLocationDto>().ReverseMap();

            CreateMap<Booking, ResultBookingDto>().ReverseMap();
            CreateMap<Booking, CreateBookingDto>().ReverseMap();
            CreateMap<Booking, UpdateBookingDto>().ReverseMap();
            CreateMap<Booking, GetBookingByIdDto>().ReverseMap();
            CreateMap<GetBookingByIdDto, UpdateBookingDto>().ReverseMap();
            CreateMap<GetBookingByIdDto, BookingApprovalEmailDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DailyPrice, opt => opt.MapFrom(src => src.DailyPrice))
                .ForMember(dest => dest.TotalDay, opt => opt.MapFrom(src => src.TotalDay))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.DailyPrice * src.TotalDay));
            CreateMap<BookingClientFormDto, CreateBookingDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    string.IsNullOrWhiteSpace(src.Status) ? "Onay Bekleniyor" : src.Status));
             
            CreateMap<Reservation, ResultReservationDto>().ReverseMap();
            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();
            CreateMap<Reservation, GetReservationByIdDto>().ReverseMap();
            CreateMap<GetReservationByIdDto, UpdateReservationDto>().ReverseMap();
            CreateMap<GetCarByIdDto, BookingApprovalEmailDto>()
                .ForMember(dest => dest.CarDisplayName, opt => opt.MapFrom(src => $"{src.Brand} {src.Model}".Trim()))
                .ForMember(dest => dest.PlateNumber, opt => opt.MapFrom(src => src.PlateNumber))
                .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => src.FuelType))
                .ForMember(dest => dest.TransmissionType, opt => opt.MapFrom(src => src.TransmissionType));
            CreateMap<BookingClientFormDto, CreateReservationDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Note))
                .ForMember(dest => dest.ReservationStatus, opt => opt.MapFrom(src =>
                    string.IsNullOrWhiteSpace(src.ReservationStatus) ? "Onay Bekleniyor" : src.ReservationStatus));
        }
    }
}
