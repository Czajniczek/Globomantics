using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models
{
    // Informacje o statystykach odnośnie konferencji
    public class StatisticsModel
    {
        // Średnia liczba uczestników konferencji
        public int AverageConferenceAttendees { get; set; }

        // Liczba uczestników
        public int NumberOfAttendees { get; set; }
    }
}
