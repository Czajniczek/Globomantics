using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globomantics.Services
{
    public interface IConferenceService
    {
        Task Add(ConferenceModel model);
        Task<ConferenceModel> GetById(int id);
        Task<IEnumerable<ConferenceModel>> GetAll();
        Task<StatisticsModel> GetStatistics();
    }
}
