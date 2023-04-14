using System.Collections.Generic;

namespace RhythmsGonnaGetYou
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsExplicit { get; set; }
        public string ReleaseDate { get; set; }
        public int BandId { get; set; }
        public List<Band> Band { get; set;}
    }
}