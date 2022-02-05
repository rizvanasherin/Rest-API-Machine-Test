using System;
using System.Collections.Generic;

namespace TravelAgencyApp.Models
{
    public partial class TravelPlans
    {
        public TravelPlans()
        {
            PlananddestinationDetails = new HashSet<PlananddestinationDetails>();
            TravelPeriod = new HashSet<TravelPeriod>();
        }

        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public int PricePerDay { get; set; }
        public int? TransportationId { get; set; }

        public virtual Transportation Transportation { get; set; }
        public virtual ICollection<PlananddestinationDetails> PlananddestinationDetails { get; set; }
        public virtual ICollection<TravelPeriod> TravelPeriod { get; set; }
    }
}
