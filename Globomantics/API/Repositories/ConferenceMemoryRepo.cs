using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models;

namespace API.Repositories
{
    public class ConferenceMemoryRepo : IConferenceRepo
    {
        private readonly List<ConferenceModel> conferences = new List<ConferenceModel>();

        public ConferenceMemoryRepo()
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

        public IEnumerable<ConferenceModel> GetAll()
        {
            return conferences;
        }

        public ConferenceModel GetById(int id)
        {
            return conferences.First(c => c.Id == id);
        }

        public ConferenceModel Add(ConferenceModel model)
        {
            model.Id = conferences.Max(c => c.Id) + 1;
            conferences.Add(model);

            return model;
        }
    }
}
