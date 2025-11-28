using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Services;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Interfaces
{
    public interface ICommentService : IBaseService<Comment, CommentDto, CreateCommentDto,UpdateCommentDto>
    {
        Task<CommentDto> AddCommentAsync(CreateCommentDto dto, int userId);

        Task<IEnumerable<CommentDto>> GetCommentsByReportAsync(int reportId);

    }
}
