using GameStoreBLL.Services.Abstraction;
using GameStoreDAL.Entities;
using GameStoreDAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreBLL.Services.Implemantation
{
    public class AddressService : IAddressService
    {
        private readonly IGenericRepository<Address> repo;
        

        public AddressService(IGenericRepository<Address> _repoAddress)
        {
            repo = _repoAddress;
        }
        public void Create(Address address)
        {
            repo.Create(address);
        }

        public Address Find(int id)
        {
            return repo.Find(id);
        }

        public Address FindByAddress(Address address)
        {

            return repo.GetAll().Where(x => x.Country == address.Country && x.City == address.City && x.Street == address.Street && x.Building == address.Building).FirstOrDefault();
             
        }

        public void Update(Address address)
        {
            repo.Update(address);
        }
    }
}
