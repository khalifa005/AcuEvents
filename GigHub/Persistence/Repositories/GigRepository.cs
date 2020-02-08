using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context ;
        }

        public Gig GetGigWithAttende(int gigId)
        {
            return _context.Gigs.Include(g=>Enumerable.Select<Attendance, ApplicationUser>(g.Attendances, l=>l.Attendee))
                .SingleOrDefault(c => c.Id == gigId );
        }

        public IEnumerable<Gig> GetGigsByArtist(string UserId)
        {
           return _context.Gigs
                .Where(f =>f.ArtistId==UserId && f.DateTime > DateTime.Now &&f.IsCanceled==false)
                .Include(c=>c.Genre)
                .ToList();
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }


        public IEnumerable<Gig> GetFutureGigsInDeatils(string SearchTerm)
        {
            var upcomingGigs= _context.Gigs
                .Include(c => c.Artist)
                .Include(c=>c.Genre)
                .Where(c=>c.DateTime > DateTime.Now &&c.IsCanceled==false);

            //for search filter
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(SearchTerm) ||
                                g.Genre.Name.Contains(SearchTerm) ||
                                g.Venue.Contains(SearchTerm));
            }
            return upcomingGigs.ToList();
        }




        public Gig GetGigsDeatilsWithArtistAndGenre(int id)
        {
            return _context.Gigs
                .Include(c => c.Artist)
                .Include(c=>c.Genre)
                .SingleOrDefault(c=> c.Id == id);
        }


        public IEnumerable<Gig> GetGigsUserAttending(string UserId)
        {
            return _context.Attendances
                .Where(g => g.AttendeeId == UserId)
                .Select(a => a.Gig)
                .Include(g=>g.Genre)
                .Include(g => g.Artist)
                .ToList();
        }

        public void Add(Gig gig)
        {
            
            _context.Gigs.Add(gig);
        }
    }
}