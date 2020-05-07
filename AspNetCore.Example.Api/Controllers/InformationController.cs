using AspNetCore.Example.Application.Mapping.Request;
using AspNetCore.Example.Application.Mapping.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AspNetCore.Example.Api.Controllers
{
    [Route("api/information")]
    public class InformationController : Controller
    {
        private readonly ILogger<InformationController> _logger;
        private readonly IMediator _mediator;

        public InformationController(ILogger<InformationController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Route("getinfomationbydocument/{document}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfomationByDocument(string document)
        {
            try
            {
                var getInfomationByDocumentRequest = new GetInfomationByDocumentRequest { Document = document };                

                var response = await _mediator.Send(getInfomationByDocumentRequest);

                if (!response.IsValid())
                {
                    return BadRequest(response.Errors);
                }

                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                var erroMessage = "Deu um erro!";

                _logger.LogError(ex, erroMessage);

                var response = new Response(erroMessage);

                return BadRequest(response.Errors);
            }           
        }
    }
}
