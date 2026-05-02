using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;


namespace Services
{
    public class SavedPostService(IUnitOfWork unitOfWork, IMapper mapper) : ISavedPostService
    {
        

        public async Task ToggleSaveAsync(int userId, int postId)
        {
            var existing = await unitOfWork.SavedPostRepository.GetAsync(userId, postId);

            if (existing == null)
            {
                await unitOfWork.SavedPostRepository.AddAsync(new SavedPost
                {
                    UserId = userId,
                    PostId = postId
                });
            }
            else
            {
                unitOfWork.SavedPostRepository.Delete(existing);
            }

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostDto>> GetSavedPostsAsync(int userId)
        {
            var posts = await unitOfWork.SavedPostRepository.GetSavedPosts(userId);

            return mapper.Map<IEnumerable<PostDto>>(posts, opt =>
            {
                opt.Items["UserId"] = userId;
            });
        }
    }
}
