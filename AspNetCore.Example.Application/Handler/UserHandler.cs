using AspNetCore.Example.Application.Mapping.Param;
using AspNetCore.Example.Application.Mapping.Request;
using AspNetCore.Example.Application.Mapping.Response;
using AspNetCore.Example.Domain.Contracts.Repositories;
using AspNetCore.Example.Domain.Entities.UserAgg;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore.Example.Application.Handler
{
    public class UserHandler :
        IRequestHandler<NewUserRequest, Response>,
        IRequestHandler<UpdateUserRequest, Response>,
        IRequestHandler<DeleteUserRequest, Response>,
        IRequestHandler<RedefinePasswordRequest, Response>,
        IRequestHandler<ChangeLinkWithTheCompanyRequest, Response>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var deleteResult = await _userRepository.DeleteUserById(request.Id.Value);

            if (!deleteResult)
            {
                response = new Response("Falha ao deletar informações!");
            }

            return response;
        }

        public async Task<Response> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var updateResult = await _userRepository.UpdateUser(_mapper.Map<UpdateUserRequest, User>(request));

            if (!updateResult)
            {
                response = new Response("Falha ao atualizar informações!");
            }

            return response;
        }

        public async Task<Response> Handle(NewUserRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                _userRepository.Add(_mapper.Map<User>(request));
            }
            catch (Exception ex)
            {
                return new Response(ex.Message);
            }

            return response;
        }

        public async Task<Response> Handle(RedefinePasswordRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var user = await _userRepository.GetUserById(request.Id.Value);

            if (!user.ValidatePassword(request.OldPassword))
                return new Response("Falha na alteração da senha!");

            user.CreateHashPassword(request.NewPassword);

            var updateResult = await _userRepository.SetNewPassword(request.Id.Value, request.NewPassword);

            if (!updateResult)
            {
                response = new Response("Falha ao atualizar informações!");
            }

            return response;
        }

        public async Task<Response> Handle(ChangeLinkWithTheCompanyRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var updateResult = await _userRepository.ChangeLinkWithTheCompany(request.Id.Value, request.UserCompanies);

            if (!updateResult)
            {
                response = new Response("Falha ao atualizar informações!");
            }

            return response;
        }
    }
}
