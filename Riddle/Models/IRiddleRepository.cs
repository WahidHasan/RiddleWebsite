using Riddle.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riddle.Models
{
    public interface IRiddleRepository
    {
        RiddlePost GetRiddle(int Id);
        IEnumerable<RiddlePost> GetAllRiddle();
        List<RiddlePost> GetAllRiddle(string riddleType);
        void Update(RiddlePost riddlePost);
        void DeleteRiddle(int Id);
        void Add(RiddlePost riddlePost);
        void AddSubComment(SubComment comment);

        Task<bool> SaveChangesAsync();
    }
}
