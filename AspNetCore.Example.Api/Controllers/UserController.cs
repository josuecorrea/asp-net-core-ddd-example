using AspNetCore.Example.Application.Mapping.Param;
using AspNetCore.Example.Application.Mapping.Request;
using AspNetCore.Example.Domain.Contracts.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AspNetCore.Example.Api.Controllers
{
    [Authorize("Bearer")]
    [Route("api/user")]
    public class UserController : Controller
    {
        readonly ILogger<UserController> _log;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> log,
                              IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [Authorize(Roles = "Administrator")]
        [Route("newuser")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewUser([FromBody] NewUserRequest newUser)
        {
            try
            {

                _log.LogInformation("Criando novo usuário: {@newUser}", newUser);

                var result = await _mediator.Send(newUser);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar criar um novo usuário: {@newUser}", newUser);
                return BadRequest(ex.Message);
            }
        }

        [Route("updateuser")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest updateUser)
        {
            try
            {
                _log.LogInformation("Alterando usuário: {@updateUser}", updateUser);

                var result = await _mediator.Send(updateUser);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar alterar o usuário: {@updateUser}", updateUser);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [Route("deleteuser")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest deleteUserRequest)
        {
            try
            {
                _log.LogInformation("Deletando usuário código: {@deleteUserRequest.Id}", deleteUserRequest.Id);

                var result = await _mediator.Send(deleteUserRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar deletar usuário: {@deleteUserRequest.Id}", deleteUserRequest.Id);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [Route("redefinepassword")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RedefinePassword([FromBody] RedefinePasswordRequest redefinePassword)
        {
            try
            {
                _log.LogInformation("Redefinir senha do usuário: {@redefinePassword}", redefinePassword);

                var result = await _mediator.Send(redefinePassword);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao redefinir senha do usuário: {@redefinePassword}", redefinePassword);
                return BadRequest(ex.Message);
            }
        }


        [Route("updateusercompanies")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserCompanies([FromBody] ChangeLinkWithTheCompanyRequest changeLinkWithTheCompanyRequest)
        {
            try
            {
                _log.LogInformation("Redefinir lista de empresa do usuário: {@changeLinkWithTheCompanyRequest}", changeLinkWithTheCompanyRequest);

                var result = await _mediator.Send(changeLinkWithTheCompanyRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao redefinir lista de empresa usuário: {@changeLinkWithTheCompanyRequest}", changeLinkWithTheCompanyRequest);

                return BadRequest(ex.Message);
            }
        }
    }
}
