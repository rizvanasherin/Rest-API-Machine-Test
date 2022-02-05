using System;
using System.Collections.Generic;

namespace TravelAgencyApp.Models
{
    public partial class PlananddestinationDetails
    {
        public int PId { get; set; }
        public int? PlanId { get; set; }
        public int? DestinationId { get; set; }

        public virtual Destinations Destination { get; set; }
        public virtual TravelPlans Plan { get; set; }
    }
}
