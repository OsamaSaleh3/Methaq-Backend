using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Users.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ErrorOr<Success>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProfileCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                return UpdateProfileErrors.NotFound;

            if (!string.IsNullOrWhiteSpace(request.FirstName)) 
                user.FirstName = request.FirstName;
            if (!string.IsNullOrWhiteSpace(request.SecondName))
                user.SecondName = request.SecondName;
            if (!string.IsNullOrWhiteSpace(request.ThirdName))
                user.ThirdName = request.ThirdName;
            if (!string.IsNullOrWhiteSpace(request.LastName)) 
                user.LastName = request.LastName;
            if (!string.IsNullOrWhiteSpace(request.PhoneNumber)) 
                user.PhoneNumber = request.PhoneNumber;
            if (request.Address != null) 
                user.Address = request.Address;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
