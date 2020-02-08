using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.IRepositories
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetArtistsFollowedById(string userId);
    }
}