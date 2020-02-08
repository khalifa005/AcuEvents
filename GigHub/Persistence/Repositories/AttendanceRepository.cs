using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context ;
        }


        public Attendance GetAttendance(int gigId,string userId)
        {
            return  _context.Attendances
                .SingleOrDefault(c => c.GigId == gigId && c.AttendeeId == userId);
        }

        public IEnumerable<Attendance> GetFutureAttendances(string UserId)
        {
            return _context.Attendances
                .Where(a=>a.AttendeeId==UserId && a.Gig.DateTime>DateTime.Now)
                .ToList();
        }

        public void Add(Attendance attendance)
        {

            _context.Attendances.Add(attendance);
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }

    }
}