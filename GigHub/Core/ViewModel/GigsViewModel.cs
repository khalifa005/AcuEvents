using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModel
{
    public class GigsViewModel
    {
        //rename to gigs form viewModel

        public int Id { get; set; }
        [Required]
        public String Venue { get; set; }

        [Required]
        [FutureDate]
        public String Date { get; set; }

        [Required]
        [ValidTime]
        public String Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }

        public string Action { get {
                //if id=any val return action Update if null return Create
                //in refactoring remove magic string so if we change the name of our action the code will break
                return (Id != 0) ? "Update" : "Create";

            } }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }

    }
}