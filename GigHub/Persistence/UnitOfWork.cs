using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core;
using GigHub.Core.IRepositories;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }

        public IAttendanceRepository AttendanceRepository { get; private set; }


        public IFollowingRepository FollowingRepository { get; set; }

        public IGenreRepository GenreRepository { get; set; }
        public IApplicationUserRepository ApplicationUserRepository { get; set; }

        public INotificationRepository NotificationRepository { get; set; }



        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs=new GigRepository(context);
            AttendanceRepository=new AttendanceRepository(context);
            FollowingRepository=new FollowingRepository(context);
            GenreRepository=new GenreRepository(context);

            ApplicationUserRepository=new ApplicationUserRepository(context);
            NotificationRepository=new NotificationRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}