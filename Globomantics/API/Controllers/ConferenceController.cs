using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Linq;

namespace API.Controllers
{
    // Użycie takiego wyrażenia zapewni, że nigdy nie będzie niezgodności między trasą a nazwą kontrolera
    // Akcja nie jest wymieniona w trasie. Właśnie tego chcemy, ponieważ usługa REST powinna odpowiadać na czasowniki HTTP
    // Sufiks "v1" to sposób na utworzenie wersji internetowego interfejsu API. Jeżeli pojawi się wersja 2 ze zmienionymi
    // kontrolerami, można zmienić "v1" na "v2", a następnie udostępnić wersje "v1" i "v2" pod tym samym adresem URL,
    // zapewniając kompatybilność wsteczną
    [Route("v1/[controller]")]
    [ApiController]
    public class ConferenceController : ControllerBase
    {
        private readonly IConferenceRepo repo;

        public ConferenceController(IConferenceRepo repo)
        {
            this.repo = repo;
        }

        public IActionResult GetAll()
        {
            var conferences = repo.GetAll();

            if (!conferences.Any()) return new NoContentResult();

            return new ObjectResult(conferences);
        }

        [HttpPost]
        public ConferenceModel Add(ConferenceModel conference)
        {
            return repo.Add(conference);
        }
    }
}
