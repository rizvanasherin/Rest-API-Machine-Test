using System;
using System.Collections.Generic;

namespace TravelAgencyApp.Models
{
    public partial class Destinations
    {
        public Destinations()
        {
            PlacesEnroute = new HashSet<PlacesEnroute>();
            PlananddestinationDetails = new HashSet<PlananddestinationDetails>();
        }

        public int DestinationId { get; set; }
        public string DestinationName { get; set; }

        public virtual ICollection<PlacesEnroute> PlacesEnroute { get; set; }
        public virtual ICollection<PlananddestinationDetails> PlananddestinationDetails { get; set; }
    }
}
