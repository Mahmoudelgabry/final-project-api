using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared.DTOs.UserProfile;
using Shareds.Exceptions;

namespace Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ============================
        // 🔵 Get Profile
        // ============================
        public async Task<GetUserProfileDto> GetUserProfileAsync(int userId)
        {
            var user = await _unitOfWork
                .GetRepository<User, int>()
                .GetAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found");

            var profile = await _unitOfWork
                .UserProfileRepository
                .GetByUserIdAsync(userId);

            if (profile == null)
            {
                return new GetUserProfileDto
                {
                    FullName = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    Phone = "",
                    Address = "",
                    City = "",
                    State = "",
                    ZipCode = ""
                };
            }

            return new GetUserProfileDto
            {
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                Phone = profile.Phone,
                Address = profile.Address,
                City = profile.City,
                State = profile.State,
                ZipCode = profile.ZipCode
            };
        }

        // ============================
        // 🟢 Update / Create Profile
        // ============================
        public async Task UpdateUserProfileAsync(int userId, UpdateUserProfileDto dto)
        {
            var user = await _unitOfWork
                .GetRepository<User, int>()
                .GetAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found");

            var repo = _unitOfWork.UserProfileRepository;

            var profile = await repo.GetByUserIdAsync(userId);

            if (profile == null)
            {
                var newProfile = _mapper.Map<UserProfile>(dto);
                newProfile.UserId = userId;

                await repo.AddAsync(newProfile);
            }
            else
            {
                _mapper.Map(dto, profile);
                repo.Update(profile);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}