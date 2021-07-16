using System;
using System.ComponentModel;

namespace Shared.Models
{
    // Informacje o konferencji
    public class ConferenceModel
    {
        public ConferenceModel()
        {
            // Domyślna data rozpoczęcia to teraz
            Start = DateTime.Now;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public DateTime Start { get; set; }

        // Łączna liczba uczestników
        [DisplayName("Attendee total")]
        public int AttendeeTotal { get; set; }
    }
}
