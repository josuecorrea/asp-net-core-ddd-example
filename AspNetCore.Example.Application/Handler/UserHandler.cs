using AutoMapper;
using AspNetCore.Example.Application.Mapping.Param;
using AspNetCore.Example.Application.Mapping.Request;
using AspNetCore.Example.Domain.Contracts.Repositories;
using AspNetCore.Example.Domain.Entities.UserAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore.Example.Application.Handler
{
    public class UserHandler :
        IRequestHandler<NewUserRequest, string>,
        IRequestHandler<UpdateUserRequest, string>,
        IRequestHandler<DeleteUserRequest, string>,
        IRequestHandler<RedefinePasswordRequest, string>,
        IRequestHandler<ChangeLinkWithTheCompanyRequest, string>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserHandler( IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteUserById(request.Id.Value);
            return await Task.FromResult(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<string> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
           await _userRepository.UpdateUser(_mapper.Map<UpdateUserRequest, User>(request));
            return await Task.FromResult(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<string> Handle(NewUserRequest request, CancellationToken cancellationToken)
        {
            _userRepository.Add(_mapper.Map<User>(request));
            return await Task.FromResult(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<string> Handle(RedefinePasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id.Value);
            if (!user.ValidatePassword(request.OldPassword))
                return await Task.FromResult(Message.SenhasDivergentes);

            request.GeneratePassword();

            await _userRepository.RedefinePassword(request.Id.Value, request.NewPassword);
            return await Task.FromResult(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<string> Handle(ChangeLinkWithTheCompanyRequest request, CancellationToken cancellationToken)
        {
           var result = await _userRepository.ChangeLinkWithTheCompany(request.Id.Value, request.UserCompanies);
           return result ? await Task.FromResult(Message.OperacaoRealizadaComSucesso) : await Task.FromResult("Error");
        }
    }
}
