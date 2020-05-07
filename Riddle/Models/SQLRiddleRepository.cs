using Microsoft.EntityFrameworkCore;
using Riddle.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riddle.Models
{
    public class SQLRiddleRepository : IRiddleRepository
    {
        private readonly AppDbContext _context;

        public SQLRiddleRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(RiddlePost riddlePost)
        {
            _context.RiddlePosts.Add(riddlePost);
        }

        public RiddlePost GetRiddle(int Id)
        {
            return _context.RiddlePosts
                .Include(p => p.MainComments)
                    .ThenInclude(mc => mc.SubComments)
                .FirstOrDefault(p => p.Id == Id);
        }

        public void DeleteRiddle(int Id)
        {
            _context.RiddlePosts.Remove(GetRiddle(Id));
        }

        public IEnumerable<RiddlePost> GetAllRiddle()
        {
            return _context.RiddlePosts;
        }

        public List<RiddlePost> GetAllRiddle(string riddleType)
        {
            return _context.RiddlePosts
                .Where(type => type.RiddleType.ToString().ToLower().Equals(riddleType.ToLower()))
                .ToList() ;// Not finished
        }

        public void Update(RiddlePost riddlePost)
        {
            _context.RiddlePosts.Update(riddlePost);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void AddSubComment(SubComment comment)
        {
            _context.SubComments.Add(comment);
        }
    }
}
