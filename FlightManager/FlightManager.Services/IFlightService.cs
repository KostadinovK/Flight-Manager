using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public interface IFlightService
    {
        void Create(CreateFlightServiceModel flight);
    }
}
