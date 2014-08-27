using System;
using System.Globalization;
using System.Security.Cryptography;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.WebUI.App_Start.ValueResolvers;
using FilmOverflow.WebUI.ViewModels;

namespace FilmOverflow.WebUI
{
	public static class MapperConfig
	{
		public static void RegisterMaps()
		{
			Mapper.CreateMap<ApplicationUserDomainModel, ApplicationUserViewModel>();
			Mapper.CreateMap<ApplicationUserViewModel, ApplicationUserDomainModel>();

			Mapper.CreateMap<CinemaDomainModel, CinemaViewModel>();
			Mapper.CreateMap<CinemaViewModel, CinemaDomainModel>();

			Mapper.CreateMap<FilmDomainModel, FilmViewModel>();
			Mapper.CreateMap<FilmViewModel, FilmDomainModel>();

			Mapper.CreateMap<ReviewDomainModel, ReviewViewModel>();
			Mapper.CreateMap<ReviewViewModel, ReviewDomainModel>();

			Mapper.CreateMap<SeanceDomainModel, SeanceViewModel>()
				.ForMember(dest => dest.Date, opt => opt.MapFrom(src => String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(src.Date))))
				.ForMember(dest => dest.Time, opt => opt.MapFrom(src => String.Format("{0:HH:mm}", Convert.ToDateTime(src.Time))));
			Mapper.CreateMap<SeanceViewModel, SeanceDomainModel>()
				.ForMember(dest => dest.Date, opt => opt.ResolveUsing<ToDomainDateResolver>())
				.ForMember(dest => dest.Time, opt => opt.ResolveUsing<ToDomainTimeResolver>());

			Mapper.CreateMap<TicketDomainModel, TicketViewModel>();
			Mapper.CreateMap<TicketViewModel, TicketDomainModel>();

			Mapper.CreateMap<PaymentMethodDomainModel, PaymentMethodViewModel>();
			Mapper.CreateMap<PaymentMethodViewModel, PaymentMethodDomainModel>();

			Mapper.CreateMap<SeatDomainModel, SeatViewModel>();
			Mapper.CreateMap<SeatViewModel, SeatDomainModel>();

			Mapper.CreateMap<HallDomainModel, HallViewModel>();
			Mapper.CreateMap<HallViewModel, HallDomainModel>();

			Mapper.CreateMap<ReservedSeatDomainModel, ReservedSeatViewModel>();
			Mapper.CreateMap<ReservedSeatViewModel, ReservedSeatDomainModel>();
		}
	}
}