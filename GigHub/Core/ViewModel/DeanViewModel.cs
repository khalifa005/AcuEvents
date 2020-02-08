using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModel
{
    public class DeanViewModel
    {

        //related to chart pie chart
        public string name { get; set; }
        public int count { get; set; }


    
        //related to chart
      public IEnumerable<Gig> list { get; set; }


        public string names { get; set; }
        public string lists { get; set; }
        public int countt { get; internal set; }
    }
}