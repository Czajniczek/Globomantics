using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globomantics.Services
{
    public class ConferenceMemoryService : IConferenceService
    {
        private readonly List<ConferenceModel> conferences = new List<ConferenceModel>();

        public ConferenceMemoryService()
        {
            conferences.Add(new ConferenceModel
            {
                Id = 1,
                Name = "Konferencja 1",
                Location = "Warszawa",
                Start = new DateTime(2021, 7, 16),
                AttendeeTotal = 1234
            });

            conferences.Add(new ConferenceModel
            {
                Id = 2,
                Name = "Konferencja 2",
                Location = "Białystok",
                Start = new DateTime(2021, 7, 17),
                AttendeeTotal = 4321
            });
        }

        public Task Add(ConferenceModel model)
        {
            model.Id = conferences.Max(c => c.Id) + 1;
            conferences.Add(model);

            return Task.CompletedTask;
        }

        public Task<ConferenceModel> GetById(int id)
        {
            return Task.Run(() => conferences.First(c => c.Id == id));
        }

        public Task<IEnumerable<ConferenceModel>> GetAll()
        {
            return Task.Run(() => conferences.AsEnumerable());
        }

        public Task<StatisticsModel> GetStatistics()
        {
            return Task.Run(() =>
            {
                return new StatisticsModel
                {
                    NumberOfAttendees = conferences.Sum(c => c.AttendeeTotal),
                    AverageConferenceAttendees = (int)conferences.Average(c => c.AttendeeTotal)
                };
            });
        }
    }
}
