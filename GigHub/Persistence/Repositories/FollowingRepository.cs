using System.Linq;
using GigHub.Core.IRepositories;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {

        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string userId,string artistId)
        {
            return  _context.Followings
                .SingleOrDefault(a=>a.FolloweeId==artistId &&
                                    a.FollowerId==userId);
        }


        public bool GetFollowingBool(string userId,string artistId)
        {
            return  _context.Followings
                .Any(a=>a.FolloweeId==artistId &&
                                    a.FollowerId==userId);
        }



        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }

    }
}