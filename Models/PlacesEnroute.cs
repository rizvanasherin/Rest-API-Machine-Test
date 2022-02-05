using System;
using System.Collections.Generic;

namespace TravelAgencyApp.Models
{
    public partial class PlacesEnroute
    {
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public int? DestinationId { get; set; }

        public virtual Destinations Destination { get; set; }
    }
}
