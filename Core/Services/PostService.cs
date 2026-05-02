using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;
using Shareds.Exceptions;


namespace Services
{
    public class PostService(IUnitOfWork _unitOfWork, IMapper _mapper) : IPostService
    {
        

        public async Task<IEnumerable<PostDto>> GetAllAsync(int userId)
        {
            var posts = await _unitOfWork.PostRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<PostDto>>(posts, opt =>
            {
                opt.Items["UserId"] = userId;
            });
        }

        public async Task<PostDto> GetByIdAsync(int id , int userId)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(id);

            if (post == null)
                throw new NotFoundException("Post not found");

            return _mapper.Map<PostDto>(post, opt =>
            {
                opt.Items["UserId"] = userId;
            });
        }

        public async Task CreateAsync(int userId, CreatePostDto dto)
        {
            var post = _mapper.Map<CommunityPost>(dto);

            post.UserId = userId;

            await _unitOfWork.PostRepository.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteAsync(int userId, int postId, string role)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(postId);

            if (post == null)
                throw new NotFoundException("Post not found");

            
            if (role != "Admin" && post.UserId != userId)
                throw new UnauthorizedException("Not authorized");

            _unitOfWork.PostRepository.Delete(post);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

