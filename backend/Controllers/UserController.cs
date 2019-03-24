using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthRepo;
using backend.Helpers;
using Contracts;
using ContractsDB;
using Entities.Helpers;
using Entities.Models;
using Entities.ModelsView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IAuthRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IRepositoryWrapper repoWrapper,
                              IUnitOfWork unitOfWork, IAuthRepository authRepository)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
            _authRepository = authRepository;
        }

        [HttpGet("inicializar")]
        public async Task<IActionResult> Inicialzar()
        {
            var cities = new List<City>()
            {
                new City(){Nombre = "HonKong"},
                new City(){Nombre = "Havana"},
                new City(){Nombre = "SantaClara"},
                new City(){Nombre = "NewYork"},
                new City(){Nombre = "Paris"},
                new City(){Nombre = "Castelvania"},
                new City(){Nombre = "Konoha"},
                new City(){Nombre = "Caracas"},
                new City(){Nombre = "Berlin"},
                new City(){Nombre = "Moscu"}
            };
            foreach (var city in cities)
                _repoWrapper.City.Create(city);

            var brands = new List<Brand>()
            {
                new Brand(){Nombre = "Ford"},
                new Brand(){Nombre = "RoyRoy"},
                new Brand(){Nombre = "Nissan"},
                new Brand(){Nombre = "Cherolet"}
            };
            foreach (var brand in brands)
                _repoWrapper.Brand.Create(brand);

            var doors = new List<DoorsNumber>()
            {
                new DoorsNumber(){Nombre = "2"},
                new DoorsNumber(){Nombre = "4"},
                new DoorsNumber(){Nombre = "6"},
                new DoorsNumber(){Nombre = "8"}
            };
            foreach (var door in doors)
                _repoWrapper.DoorsNumber.Create(door);

            var fuels = new List<Fuel>()
            {
                new Fuel(){Nombre = "Gasolina"},
                new Fuel(){Nombre = "Petrole"},
                new Fuel(){Nombre = "Diesel"},
                new Fuel(){Nombre = "Arquimia"}
            };
            foreach (var fuel in fuels)
                _repoWrapper.Fuel.Create(fuel);

            var locationss = new List<Location>()
            {
                new Location(){Nombre = "Estados Unidos"},
                new Location(){Nombre = "Caribe"},
                new Location(){Nombre = "Dinamarca"},
                new Location(){Nombre = "Asia"}
            };
            foreach (var location in locationss)
                _repoWrapper.Location.Create(location);

            var models = new List<Model>()
            {
                new Model(){Nombre = "Descapotable"},
                new Model(){Nombre = "Clasico"},
                new Model(){Nombre = "Ferrari"},
                new Model(){Nombre = "Carrera"}
            };
            foreach (var model in models)
                _repoWrapper.Model.Create(model);

            var years = new List<Year>()
            {
                new Year(){Nombre = "1958"},
                new Year(){Nombre = "2017"},
                new Year(){Nombre = "2018"},
                new Year(){Nombre = "2019"}
            };
            foreach (var model in models)
                _repoWrapper.Model.Create(model);
            var packs = new List<Pack>()
            {
                new Pack(){Nombre = "No Se"},
                new Pack(){Nombre = "No Se 1"},
                new Pack(){Nombre = "No Se 2"},
                new Pack(){Nombre = "No Se 3"}
            };
            foreach (var pack in packs)
                _repoWrapper.Pack.Create(pack);

            var states = new List<State>()
            {
                new State(){Nombre = "State1"},
                new State(){Nombre = "State2"},
                new State(){Nombre = "State3"},
                new State(){Nombre = "State4"}
            };
            foreach (var state in states)
                _repoWrapper.State.Create(state);

            var versions = new List<Version>()
            {
                new Version(){Nombre = "version1"},
                new Version(){Nombre = "version16"},
                new Version(){Nombre = "version17"},
                new Version(){Nombre = "version18"}
            };
            foreach (var version in versions)
                _repoWrapper.Version.Create(version);

            var roles = new List<IdentityRole>
            {
                new IdentityRole(){Id="1", Name = UserParams.ROLE_EMPLOYEE},
                new IdentityRole(){Id="2", Name = UserParams.ROLE_DISTRIBUTOR},
                new IdentityRole(){Id="3", Name = UserParams.ROLE_ADMIN},
                new IdentityRole(){Id="4", Name = UserParams.ROLE_ADMINAMDGM}
            };
            foreach (var rol in roles)
                    _repoWrapper.Role.Create(rol);

            await _unitOfWork.SaveChangesAsync();

            List<RegisterModel> models1 = new List<RegisterModel>()
            {
                new RegisterModel(){ Email= "Melisa@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },
                new RegisterModel(){ Email= "Rosa@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                                    
                },
                new RegisterModel(){ Email= "digna@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },
                new RegisterModel(){ Email= "Jotica@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },
                new RegisterModel(){ Email= "Oscar@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },
                new RegisterModel(){ Email= "harold@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },
                new RegisterModel(){ Email= "Tiny@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },

            };

            foreach (var model in models1)
                await _authRepository.Register(model);

           
            var userss = await _repoWrapper.User.FindAll();
            var userPrueba = userss.First();
            var userPrueba1 = userss.Last();
            var ccc = await _repoWrapper.City.GetById(10);
            userPrueba.City = ccc;

            _repoWrapper.Employee.Create(new EmployeeData()
            {
                CellNumber = "55824756",
                CityId = 4,
                FatherSurname = "Juan Jose",
                Colony = "Diezmero",
                InsideNumber = "12356",
                MotherSurname = "Digna",
                FixNumber = "9764",
                NoEmployee = "1",
                OutsideNumber = "654",
                StateId = 1,
                ZipCode = "Codigo Postal",
                UserId = userPrueba.Id
            });

            await _unitOfWork.SaveChangesAsync();

            var publications = new List<Publication>()
            {
                new Publication(){BrandId = 1,Created = System.DateTime.Now,
                                  DoorsNumberId = 3, Enabled = true,EquipmentDetails="Tiene de Todo",
                                  FuelId =1, InsideColor="negor", UserId = userPrueba.Id,
                                  InvoiceNumber = 2, LocationId = 2, ModelId = 3,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 2,
                                  Mileage = 1000, Price = 250000, SerialNumber = "1234",
                                 VersionId = 1, YearId =2 },
                new Publication(){BrandId = 3,Created = new System.DateTime(2015,3,7,9,23,4),
                                  DoorsNumberId = 4, Enabled = false,EquipmentDetails="Este es un Mustang 2",
                                  FuelId =2, InsideColor="azul", UserId = userPrueba1.Id,
                                  InvoiceNumber = 2, LocationId = 4, ModelId = 3,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 1,
                                  Mileage = 9000, Price = 20000000, SerialNumber = "7834",
                                 VersionId = 4, YearId = 3 },
                new Publication(){BrandId = 2,Created = System.DateTime.Now,
                                  DoorsNumberId = 3, Enabled = true,EquipmentDetails="Tiene de Todo",
                                  FuelId =1, InsideColor="negor", UserId = userPrueba1.Id,
                                  InvoiceNumber = 4, LocationId = 3, ModelId = 4,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 2,
                                  Mileage = 1000, Price = 250000, SerialNumber = "1234",
                                 VersionId = 1, YearId =2 }
                ,
                new Publication(){BrandId = 1,Created = System.DateTime.Now,
                                  DoorsNumberId = 3, Enabled = true,EquipmentDetails="Tiene de Todo",
                                  FuelId =1, InsideColor="negor", UserId = userPrueba.Id,
                                  InvoiceNumber = 2, LocationId = 2, ModelId = 3,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 2,
                                  Mileage = 1000, Price = 250000, SerialNumber = "1234",
                                 VersionId = 1, YearId =2 }
                ,
                new Publication(){BrandId = 1,Created = System.DateTime.Now,
                                  DoorsNumberId = 3, Enabled = true,EquipmentDetails="Tiene de Todo",
                                  FuelId =1, InsideColor="negor", UserId = userPrueba1.Id,
                                  InvoiceNumber = 2, LocationId = 2, ModelId = 3,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 2,
                                  Mileage = 1000, Price = 250000, SerialNumber = "1234",
                                 VersionId = 3, YearId =2 }
                ,
                new Publication(){BrandId = 2,Created = System.DateTime.Now,
                                  DoorsNumberId = 3, Enabled = true,EquipmentDetails="Tiene de Todo",
                                  FuelId =1, InsideColor="negor", UserId = userPrueba1.Id,
                                  InvoiceNumber = 2, LocationId = 2, ModelId = 3,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 2,
                                  Mileage = 1000, Price = 250000, SerialNumber = "1234",
                                 VersionId = 4, YearId =2 }
                ,
                new Publication(){BrandId = 1,Created = System.DateTime.Now,
                                  DoorsNumberId = 3, Enabled = true,EquipmentDetails="Tiene de Todo",
                                  FuelId =1, InsideColor="negor", UserId = userPrueba1.Id,
                                  InvoiceNumber = 2, LocationId = 2, ModelId = 3,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 2,
                                  Mileage = 1000, Price = 250000, SerialNumber = "1234",
                                 VersionId = 2, YearId =4 }
                ,
                new Publication(){BrandId = 1,Created = System.DateTime.Now,
                                  DoorsNumberId = 3, Enabled = true,EquipmentDetails="Tiene de Todo",
                                  FuelId =1, InsideColor="negor", UserId = userPrueba.Id,
                                  InvoiceNumber = 2, LocationId = 2, ModelId = 3,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 2,
                                  Mileage = 1000, Price = 250000, SerialNumber = "1234",
                                 VersionId = 1, YearId =2 }
                ,
                new Publication(){BrandId = 1,Created = System.DateTime.Now,
                                  DoorsNumberId = 3, Enabled = true,EquipmentDetails="Tiene de Todo",
                                  FuelId =1, InsideColor="negor", UserId = userPrueba.Id,
                                  InvoiceNumber = 2, LocationId = 2, ModelId = 3,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 2,
                                  Mileage = 1000, Price = 250000, SerialNumber = "1234",
                                 VersionId = 1, YearId =2 }
                ,
                new Publication(){BrandId = 1,Created = System.DateTime.Now,
                                  DoorsNumberId = 3, Enabled = true,EquipmentDetails="Tiene de Todo",
                                  FuelId =1, InsideColor="negor", UserId = userPrueba1.Id,
                                  InvoiceNumber = 2, LocationId = 2, ModelId = 3,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 2,
                                  Mileage = 1000, Price = 250000, SerialNumber = "1234",
                                 VersionId = 1, YearId =2 }
                ,
                new Publication(){BrandId = 1,Created = System.DateTime.Now,
                                  DoorsNumberId = 3, Enabled = true,EquipmentDetails="Tiene de Todo",
                                  FuelId =1, InsideColor="negor", UserId = userPrueba1.Id,
                                  InvoiceNumber = 2, LocationId = 2, ModelId = 3,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 2,
                                  Mileage = 1000, Price = 250000, SerialNumber = "1234",
                                 VersionId = 1, YearId =2 }
                ,
                new Publication(){BrandId = 1,Created = System.DateTime.Now,
                                  DoorsNumberId = 3, Enabled = true,EquipmentDetails="Tiene de Todo",
                                  FuelId =1, InsideColor="negor", UserId = userPrueba.Id,
                                  InvoiceNumber = 2, LocationId = 2, ModelId = 3,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 2,
                                  Mileage = 1000, Price = 250000, SerialNumber = "1234",
                                 VersionId = 1, YearId =2 }
                ,
                new Publication(){BrandId = 1,Created = System.DateTime.Now,
                                  DoorsNumberId = 3, Enabled = true,EquipmentDetails="Tiene de Todo",
                                  FuelId =1, InsideColor="negor", UserId = userPrueba1.Id,
                                  InvoiceNumber = 2, LocationId = 2, ModelId = 3,
                                  OutsideColor ="rojo", PackId = 2, TransmissionId = 2,
                                  Mileage = 1000, Price = 250000, SerialNumber = "1234",
                                 VersionId = 1, YearId =2 }

            };
            foreach (var publication in publications)
                _repoWrapper.Publication.Create(publication);

            await _unitOfWork.SaveChangesAsync();

            var allpubl = await _repoWrapper.Publication.FindAll();
            var publ1 = allpubl.First();
            var publ2 = allpubl.Last();

            System.DateTime fecha = new System.DateTime();

            List<Offer> offers = new List<Offer>()
            {
                new Offer() { Id = 1, Price = 50000, Description = "Un carrito muy bonito",
                    Created = fecha, PublicationId = 2, UserId = "10", Enabled = true, User = userPrueba,
                    Publication = publ1 },

                new Offer() { Id = 2, Price = 100000, Description = "Un bumbumchakata",
                    Created = fecha, PublicationId = 3, UserId = "10", Enabled = false, User = userPrueba1,
                    Publication = publ1 },

                new Offer() { Id = 3, Price = 80000, Description = "Bruuuuuuuummmmm",
                    Created = fecha, PublicationId = 4, UserId = "9", Enabled = true, User = userPrueba,
                    Publication = publ2 },

                new Offer() { Id = 4, Price = 1000000, Description = "Taxi libre",
                    Created = fecha, PublicationId = 1, UserId = "9", Enabled = true, User = userPrueba1,
                    Publication = publ2 }
            };

            foreach (var offer in offers)
            {
                _repoWrapper.Offer.Create(offer);
            }

            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParams param)
        {
            IQueryable<User> query;
            if (param.Role != null)
            {
                var role = await _repoWrapper.Role.FindByCondition(r=>r.Name.Equals(param.Role));
                if (role != null)
                    query = await _repoWrapper.User.FindAllInRole(role.Id);
                else
                    query = _repoWrapper.User.UsersQueryable(); 
            }
            else
            {
                query = _repoWrapper.User.UsersQueryable().Include(c => c.City);
            }

            var users = await PagedList<User>.CreateAsync(query.OrderByDescending(
                      c => c.Id), param.PageNumber, param.PageSize);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users.ToList());           
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _repoWrapper.User.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _repoWrapper.User.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var publications = await _repoWrapper.Publication.FindByConditionAsync(v => v.User.Id.Equals(user.Id));
            foreach (var publ in publications)
                _repoWrapper.Publication.Delete(publ);

            _repoWrapper.User.Delete(user);
            await _unitOfWork.SaveChangesAsync();

            return Ok(user);
        }

        

        [HttpPost("user/authorize/{id}")]
        public async Task<IActionResult> AuthorizeUser(string id)
        {
            var user = await _repoWrapper.User.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Enabled = true;

            _repoWrapper.User.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("users/unauthorize/{id}")]
        public async Task<IActionResult> UnAuthorizeUser(string id)
        {
            var user = await _repoWrapper.User.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            _repoWrapper.User.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Ok(user);
        }
    }
}