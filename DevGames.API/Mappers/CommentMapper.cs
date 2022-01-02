using AutoMapper;

using DevGames.API.Entities;
using DevGames.API.Models;

namespace DevGames.API.Mappers;

public class CommentMapper : Profile
{
    public CommentMapper()
    {
        CreateMap<AddCommentInputModel, Comment>();
    }
}
