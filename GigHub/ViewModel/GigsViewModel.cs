using GigHub.Models;
using System;
using System.Collections.Generic;

namespace GigHub.ViewModel
{
    public class GigsViewModel
    {


        public String Venue { get; set; }
        public String Date { get; set; }
        public string Time { get; set; }

  
        public int Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}