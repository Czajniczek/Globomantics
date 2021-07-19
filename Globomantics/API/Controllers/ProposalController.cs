using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Linq;
using System;

namespace API.Controllers
{
    // HTTP Status Codes - https://developer.mozilla.org/en-US/docs/Web/HTTP/Status, https://www.restapitutorial.com/httpstatuscodes.html:
    // • 200 - OK 
    // • 201 - Created
    // • 204 - No Content
    // • 304 - Not Modified
    // • 400 - Bad Request
    // • 401 - Unauthorized
    // • 403 - Forbidden
    // • 404 - Not Found
    // • 409 - Conflict
    // • 500 - Internal Server Error

    // Użycie takiego wyrażenia zapewni, że nigdy nie będzie niezgodności między trasą a nazwą kontrolera
    // Akcja nie jest wymieniona w trasie. Właśnie tego chcemy, ponieważ usługa REST powinna odpowiadać na czasowniki HTTP
    // Sufiks "v1" to sposób na utworzenie wersji internetowego interfejsu API. Jeżeli pojawi się wersja 2 ze zmienionymi
    // kontrolerami, można zmienić "v1" na "v2", a następnie udostępnić wersje "v1" i "v2" pod tym samym adresem URL,
    // zapewniając kompatybilność wsteczną
    [Route("v1/[controller]")]

    // Wnioskowanie parametru źródłowego powiązania - https://docs.microsoft.com/pl-pl/aspnet/core/web-api/?view=aspnetcore-5.0
    // • [FromBody] - Treść żądania
    // • [FromForm] - Dane formularza w treści żądania
    // • [FromHeader] - Nagłówek żądania
    // • [FromQuery] - Parametr ciągu zapytania żądania
    // • [FromRoute] - Przekieruj dane z bieżącego żądania
    // • [FromServices] - Usługa żądania wstrzyknięta jako parametr akcji
    [ApiController]
    public class ProposalController : ControllerBase
    {

        private readonly IProposalRepo proposalRepo;

        public ProposalController(IProposalRepo proposalRepo)
        {
            this.proposalRepo = proposalRepo;
        }

        [HttpGet("{conferenceId}")]
        public IActionResult GetAll(int conferenceId)
        {
            var proposals = proposalRepo.GetAllForConference(conferenceId);

            if (!proposals.Any()) return new NoContentResult();

            return new ObjectResult(proposals);
        }

        [HttpGet("single/{id}", Name = "GetById")]
        public ProposalModel GetById(int id)
        {
            return proposalRepo.GetById(id);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProposalModel model)
        {
            var addedProposal = proposalRepo.Add(model);

            // CreatedAtRoute spowoduje zwrócenie kodu stanu HTTP 201, co oznacza, że został utworzony
            // Zwraca on również adres URL, z którego można pobrać wprowadzone dane
            // Jest to 3 model dojrzałości Richardsona - https://devkr.pl/2018/04/10/restful-api-richardson-maturity-model/
            // Adres URL jest generowany poprzez określenie akcji "GetById" dla bieżącego kontrolera
            return CreatedAtRoute("GetById", new { id = addedProposal.Id }, addedProposal);
        }

        // System routingu wie, że musi szukać w żądaniu podanego elementu "proposalId" jako parametru dla atrybutu HttpPut
        // Zostanie to dopasowane do parametru, który ma akcja ("int proposalId")
        [HttpPut("{proposalId}")]
        public IActionResult Approve(int proposalId)
        {
            try
            {
                return new ObjectResult(proposalRepo.Approve(proposalId));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
