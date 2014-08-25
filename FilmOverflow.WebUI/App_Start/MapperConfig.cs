using System;
using AutoMapper;
using FilmOverflow.Domain.Models;
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
				.ForMember(x => x.Date, opt => opt.MapFrom(src => src.Date.ToShortDateString()))
				.ForMember(x => x.Time, opt => opt.MapFrom(src => src.Time.ToShortTimeString()));
			Mapper.CreateMap<SeanceViewModel, SeanceDomainModel>()
				.ForMember(x => x.Date, opt => opt.MapFrom(src => Convert.ToDateTime(src.Date)))
				.ForMember(x => x.Time, opt => opt.MapFrom(src => Convert.ToDateTime(src.Time)));

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