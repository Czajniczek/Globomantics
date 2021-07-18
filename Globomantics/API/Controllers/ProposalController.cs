using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Linq;
using System;

namespace API.Controllers
{
    [Route("v1/[controller]")]
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
            return CreatedAtRoute("GetById", new { id = addedProposal.Id }, addedProposal);
        }

        // System routingu wie, że musi szukać w żądaniu podanego elementu "proposalId" jako parametru dla atrybutu HttpPut
        // Zostanie to dopasowane do parametru, który ma akcja
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
