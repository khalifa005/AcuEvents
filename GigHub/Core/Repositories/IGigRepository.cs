using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        Gig GetGigWithAttende(int gigId);


        IEnumerable<Gig> GetGigsByArtist(string UserId);
        List<Genre> GetGenres();
        Gig GetGigsDeatilsWithArtistAndGenre(int id);
        IEnumerable<Gig> GetGigsUserAttending(string UserId);

        IEnumerable<Gig> GetFutureGigsInDeatils(string SearchTerm);
        void Add(Gig gig);
    }
}