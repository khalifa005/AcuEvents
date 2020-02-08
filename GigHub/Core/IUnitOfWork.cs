using GigHub.Core.IRepositories;
using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IAttendanceRepository AttendanceRepository { get; }
        IApplicationUserRepository ApplicationUserRepository { get; set; }

        IFollowingRepository FollowingRepository { get; set; }
        IGenreRepository GenreRepository { get; set; }

        INotificationRepository NotificationRepository { get; set; }

        void Complete();

       
    }
}