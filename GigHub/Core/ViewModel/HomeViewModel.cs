using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModel
{


    //related to chart
    public class test
    {
        public string name { get; set; }
        public byte list { get; set; }

    }
    public class HomeViewModel
    {
        //related to chart
        public string name { get; set; }
        public int count { get; set; }
        //related to chart
        public List<byte> list { get; set; }

        public bool ShowAction { get;  set; }
        public IEnumerable<Gig> UpcomingGig { get; set; }
        public string Heading { get; internal set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendance> attendences { get; internal set; }
    }
}