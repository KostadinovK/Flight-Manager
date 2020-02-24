using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Domain;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public interface IFlightService
    {
        void Create(CreateFlightServiceModel flight);

        int GetCount();

        IEnumerable<Flight> GetAll(int page);
    }
}
