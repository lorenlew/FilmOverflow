using System;

namespace FilmOverflow.WebUI.ViewModels
{
    public class TicketViewModel : EntityViewModel
    {
        public string ApplicationUserId { get; set; }

        public string SeanceId { get; set; }

        public int SeatNumber { get; set; }

        public string PaymentMethodId { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}