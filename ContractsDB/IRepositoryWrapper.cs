using System;
using System.Collections.Generic;
using System.Text;

namespace ContractsDB
{
    public interface IRepositoryWrapper
    {
        IEmployeeRepository Employee { get; }
        IBrandRepository Brand { get; }
        ICityRepository City { get; }
        IDoorsNumberRepository DoorsNumber { get; }
        IFuelRepository Fuel { get; }
        ILocationRepository Location { get; }
        IModelRepository Model { get; }
        IOfferRepository Offer { get; }
        IPackRepository Pack { get; }
        IPublicationRepository Publication { get; }
        IStateRepository State { get; }
        ITransmissionRepository Transmission { get; }
        IVersionRepository Version { get; }
        IYearRepository Year { get; }
        IUserRepository User { get; }
        IRoleRepository Role { get; }
    }
}
