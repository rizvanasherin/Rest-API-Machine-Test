using System;
using System.Collections.Generic;

namespace TravelAgencyApp.Models
{
    public partial class TravelPeriod
    {
        public int PeriodId { get; set; }
        public int PeriodOfDays { get; set; }
        public int? PlanId { get; set; }

        public virtual TravelPlans Plan { get; set; }
    }
}
