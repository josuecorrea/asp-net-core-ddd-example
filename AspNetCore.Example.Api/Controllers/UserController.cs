using AutoMapper;
using AspNetCore.Example.Api.Validators;
using AspNetCore.Example.Application.Mapping.Param;
using AspNetCore.Example.Application.Mapping.Request;
using AspNetCore.Example.Application.Mapping.Response.UserResponse;
using AspNetCore.Example.Domain.Contracts.Repositories;
using AspNetCore.Example.Domain.Entities.UserAgg;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore.Example.Api.Controllers
{
    //[Authorize("Bearer")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        readonly ILogger<UserController> _log;
        readonly IDiagnosticContext _diagnosticContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        static int _callCount;

        public UserController(IUserRepository userRepository,
                              ILogger<UserController> log,
                              IDiagnosticContext diagnosticContext,
                              IMapper mapper,
                              IMediator mediator)
        {
            _userRepository = userRepository;
            _log = log;
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
            _mapper = mapper;
            _mediator = mediator;
        }

        //[Authorize(Roles = "Administrator")]
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

                var validator = await new NewUserValidator().ValidateAsync(newUser);

                if (!validator.IsValid)
                {
                    _log.LogError("Erro durante a validação dos dados {@newUser}", newUser);

                    UserResponse responseErro = new UserResponse();
                    validator.Errors.ToList().ForEach(c => { responseErro.AddUserMessageError(c.ErrorMessage); }) ;
                    return BadRequest(responseErro);
                }

                 var result = await _mediator.Send(newUser);

                return Ok(new UserResponse(result).IsValid());
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

                var validator = await new UpdateUserValidator().ValidateAsync(updateUser);

                if (!validator.IsValid)
                {
                    UserResponse responseErro = new UserResponse();
                    validator.Errors.ToList().ForEach(c => { responseErro.AddUserMessageError(c.ErrorMessage); });
                    _log.LogError("Erro durante a validação dos dados {@updateUser}", updateUser);
                    return BadRequest(responseErro);
                }

                var result = await _mediator.Send(updateUser);

                return Ok(new UserResponse(result).IsValid());
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar alterar o usuário: {@updateUser}", updateUser);
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = "Administrator")]
        [Route("deleteuser")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest deleteUserRequest)
        {
            try
            {                
                var validator = await new DeleteUserValidator().ValidateAsync(deleteUserRequest);                
                _log.LogInformation("Deletando usuário código: {@deleteUserRequest.Id}", deleteUserRequest.Id);
                if (!validator.IsValid)
                {
                    UserResponse responseErro = new UserResponse();
                    validator.Errors.ToList().ForEach(c => { responseErro.AddUserMessageError(c.ErrorMessage); });
                    _log.LogError("Erro durante a validação dos dados {@deleteUserRequest.Id}", deleteUserRequest);
                    return BadRequest(validator.Errors);
                }          
                var result = await _mediator.Send(deleteUserRequest);
                var response = new UserResponse(result);
                return Ok(response.IsValid());
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar deletar usuário: {@deleteUserRequest.Id}", deleteUserRequest.Id);
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = "Administrator")]
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

                var validator = await new RedefinePasswordValidadtor().ValidateAsync(redefinePassword);

                if (!validator.IsValid)
                {
                    _log.LogError("Erro durante a validação dos dados {@newUser}", redefinePassword);

                    UserResponse responseErro = new UserResponse();
                    validator.Errors.ToList().ForEach(c => { responseErro.AddUserMessageError(c.ErrorMessage); });
                    return BadRequest(responseErro);
                }

                var result = await _mediator.Send(redefinePassword);

                return Ok(new UserResponse(result).IsValid());
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

                var validator = await new ChangeLinkWithTheCompanyValidator().ValidateAsync(changeLinkWithTheCompanyRequest);

                if (!validator.IsValid)
                {
                    _log.LogError("Erro durante a validação dos dados {@changeLinkWithTheCompanyRequest}", changeLinkWithTheCompanyRequest);

                    UserResponse responseErro = new UserResponse();
                    validator.Errors.ToList().ForEach(c => { responseErro.AddUserMessageError(c.ErrorMessage); });
                    return BadRequest(responseErro);
                }

                var result = await _mediator.Send(changeLinkWithTheCompanyRequest);

                return Ok(new UserResponse(result).IsValid());
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao redefinir lista de empresa usuário: {@changeLinkWithTheCompanyRequest}", changeLinkWithTheCompanyRequest);
                return BadRequest(ex.Message);
            }
        }
    }
}
