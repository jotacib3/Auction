using ContractsDB;
using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext _repoContext;
        private IBrandRepository _brand;
        private ICityRepository _city;
        private IDoorsNumberRepository _doorsNumber;
        private IEmployeeRepository _employee;
        private IFuelRepository _fuel;
        private ILocationRepository _location;
        private IModelRepository _model;
        private IOfferRepository _offer;
        private IPackRepository _pack;
        private IPublicationRepository _publication;
        private IStateRepository _state;
        private ITransmissionRepository _transmission;
        private IVersionRepository _version;
        private IYearRepository _year;
        private IUserRepository _user;
        private IRoleRepository _role;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        public IRoleRepository Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_repoContext);
                }
                return _role;
            }
        }
        public IEmployeeRepository Employee {
            get
            {
                if (_employee == null)
                {
                    _employee = new EmployeeDataRepository(_repoContext);
                }
                return _employee;
            }
        }
        public IBrandRepository Brand {
            get
            {
                if (_brand == null)
                {
                    _brand = new BrandRepository(_repoContext);
                }
                return _brand;
            }
        }
        public ICityRepository City {
            get
            {
                if (_city == null)
                {
                    _city = new CityRepository(_repoContext);
                }
                return _city;
            }
        }
        public IDoorsNumberRepository DoorsNumber {
            get
            {
                if (_doorsNumber == null)
                {
                    _doorsNumber = new DoorsNumberRepository(_repoContext);
                }
                return _doorsNumber;
            }
        }
        public IFuelRepository Fuel {
            get
            {
                if (_fuel== null)
                {
                    _fuel = new FuelRepository(_repoContext);
                }
                return _fuel;
            }
        }
        public ILocationRepository Location {
            get
            {
                if (_location == null)
                {
                    _location = new LocationRepository(_repoContext);
                }
                return _location;
            }
        }
        public IModelRepository Model {
            get
            {
                if (_model == null)
                {
                    _model = new ModelRepository(_repoContext);
                }
                return _model;
            }
        }
        public IOfferRepository Offer {
            get
            {
                if (_offer == null)
                {
                    _offer = new OfferRepository(_repoContext);
                }
                return _offer;
            }
        }
        public IPackRepository Pack {
            get
            {
                if (_pack == null)
                {
                    _pack = new PackRepository(_repoContext);
                }
                return _pack;
            }
        }
        public IPublicationRepository Publication {
            get
            {
                if (_publication == null)
                {
                    _publication = new PublicationRepository(_repoContext);
                }
                return _publication;
            }
        }
        public IStateRepository State {
            get
            {
                if (_state == null)
                {
                    _state = new StateRepository(_repoContext);
                }
                return _state;
            }
        }
        public ITransmissionRepository Transmission {
            get
            {
                if (_transmission == null)
                {
                    _transmission = new TransmissionRepository(_repoContext);
                }
                return _transmission;
            }
        }
        public IVersionRepository Version {
            get
            {
                if (_version == null)
                {
                    _version = new VersionRepository(_repoContext);
                }
                return _version;
            }
        }
        public IYearRepository Year {
            get
            {
                if (_year == null)
                {
                    _year = new YearRepository(_repoContext);
                }
                return _year;
            }
        }
    }
}
