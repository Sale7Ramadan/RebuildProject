using BusinceLayer.Services;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Interfaces
{
    public interface ICommentService 
    {
        Task<CommentDto> AddCommentAsync(CreateCommentDto dto, int userId);
       
    }
}
