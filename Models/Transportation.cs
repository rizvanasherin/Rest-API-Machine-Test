using System;
using System.Collections.Generic;

namespace TravelAgencyApp.Models
{
    public partial class Transportation
    {
        public Transportation()
        {
            TravelPlans = new HashSet<TravelPlans>();
        }

        public int TransportationId { get; set; }
        public string TransportationName { get; set; }
        public int VehicleChargePerDay { get; set; }

        public virtual ICollection<TravelPlans> TravelPlans { get; set; }
    }
}
