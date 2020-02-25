using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Web.ViewModels.User
{
    public class ListingPageViewModel
    {
        public List<ListingViewModel> Users { get; set; }

        public int TotalUsersCount { get; set; }

        public int CurrentPage { get; set; }

        public int UsersPerPage { get; set; }

        public string OrderParam { get; set; }

        public int LastPage { get; set; }
    }
}
