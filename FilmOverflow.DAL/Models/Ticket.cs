using System;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.DAL.Models
{
    public class Ticket : Entity
    {
        public string ApplicationUserId { get; set; }

        public string SeanceId { get; set; }

        public int SeatNumber { get; set; }

        public string PaymentMethodId { get; set; }

        public DateTime PaymentDate { get; set; }

    }
}
