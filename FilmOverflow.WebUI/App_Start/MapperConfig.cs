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

            Mapper.CreateMap<SeanceDomainModel, SeanceViewModel>();
            Mapper.CreateMap<SeanceViewModel, SeanceDomainModel>();

            Mapper.CreateMap<TicketDomainModel, TicketViewModel>();
            Mapper.CreateMap<TicketViewModel, TicketDomainModel>();

            Mapper.CreateMap<PaymentMethodDomainModel, PaymentMethodViewModel>();
            Mapper.CreateMap<PaymentMethodViewModel, PaymentMethodDomainModel>();
        }
    }
}