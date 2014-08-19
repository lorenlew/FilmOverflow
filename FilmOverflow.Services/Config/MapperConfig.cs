using AutoMapper;
using FilmOverflow.DAL.Models;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.Services.Config
{
	public static class MapperConfig
	{
		public static void RegisterMaps()
		{
			Mapper.CreateMap<ApplicationUser, ApplicationUserDomainModel>();
			Mapper.CreateMap<ApplicationUserDomainModel, ApplicationUser>();

			Mapper.CreateMap<Cinema, CinemaDomainModel>();
			Mapper.CreateMap<CinemaDomainModel, Cinema>();

			Mapper.CreateMap<Film, FilmDomainModel>();
			Mapper.CreateMap<FilmDomainModel, Film>();

			Mapper.CreateMap<Review, ReviewDomainModel>();
			Mapper.CreateMap<ReviewDomainModel, Review>();

			Mapper.CreateMap<Seance, SeanceDomainModel>();
			Mapper.CreateMap<SeanceDomainModel, Seance>();

			Mapper.CreateMap<Ticket, TicketDomainModel>();
			Mapper.CreateMap<TicketDomainModel, Ticket>();

			Mapper.CreateMap<PaymentMethod, PaymentMethodDomainModel>();
			Mapper.CreateMap<PaymentMethodDomainModel, PaymentMethod>();

			Mapper.CreateMap<Seat, SeatDomainModel>();
			Mapper.CreateMap<SeatDomainModel, Seat>();

			Mapper.CreateMap<Hall, HallDomainModel>();
			Mapper.CreateMap<HallDomainModel, Hall>();

			Mapper.CreateMap<ReservedSeat, ReservedSeatDomainModel>();
			Mapper.CreateMap<ReservedSeatDomainModel, ReservedSeat>();
		}
	}
}