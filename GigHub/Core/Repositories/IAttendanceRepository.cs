using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        //bool GetAttendance(int gigId,string userId);
        //IEnumerable<Attendance> GetFutureAttendances(string UserId);

        Attendance GetAttendance(int gigId,string userId);
        IEnumerable<Attendance> GetFutureAttendances(string UserId);


        void Add(Attendance attendance);
        void Remove(Attendance attendance);




    }
}