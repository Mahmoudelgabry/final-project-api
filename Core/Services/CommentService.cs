using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;
using Shareds.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CommentService(IUnitOfWork unitOfWork, IMapper mapper) : ICommentService
    {
       
        public async Task<IEnumerable<CommentDto>> GetByPostAsync(int postId)
        {
            var comments = await unitOfWork.CommentRepository.GetByPostIdAsync(postId);

            return mapper.Map<IEnumerable<CommentDto>>(comments);
        }

        public async Task AddAsync(int userId, int postId, CreateCommentDto dto)
        {
            var comment = new Comment
            {
                Content = dto.Content,
                UserId = userId,
                PostId = postId
            };

            await unitOfWork.CommentRepository.AddAsync(comment);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId, int commentId)
        {
            var comment = await unitOfWork.CommentRepository.GetByIdAsync(commentId);

            if (comment == null)
                throw new NotFoundException("Comment not found");

            if (comment.UserId != userId)
                throw new UnauthorizedException("Unauthorized");

            unitOfWork.CommentRepository.Delete(comment);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
