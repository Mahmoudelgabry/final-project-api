using AutoMapper;
using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CommunityPost, PostDto>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))

                .ForMember(dest => dest.Tags,
                    opt => opt.MapFrom(src => src.PostTags.Select(t => t.Tag.Name)))

                .ForMember(dest => dest.Votes,
                    opt => opt.MapFrom(src => src.Votes.Sum(v => v.VoteType)))

                .ForMember(dest => dest.CommentsCount,
                    opt => opt.MapFrom(src => src.Comments.Count))

                .ForMember(dest => dest.CurrentUserVote,
                    opt => opt.MapFrom((src, dest, _, context) =>
                      src.Votes
                          .FirstOrDefault(v => v.UserId == (int)context.Items["UserId"])
                          ?.VoteType))

                .ForMember(dest => dest.IsSaved,
                   opt => opt.MapFrom((src, dest, _, context) =>
                      src.SavedPosts.Any(s => s.UserId == (int)context.Items["UserId"])));

            CreateMap<CreatePostDto, CommunityPost>()
                .ForMember(dest => dest.PostTags,
                    opt => opt.MapFrom(src =>
                        src.TagIds.Select(tagId => new PostTag { TagId = tagId })
                    ));
        }
    }
}
