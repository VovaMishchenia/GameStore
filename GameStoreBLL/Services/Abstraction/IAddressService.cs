using GameStoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreBLL.Services.Abstraction
{
    public interface IAddressService
    {
        Address Find(int id);
        void Create(Address address);
        Address FindByAddress(Address address);
        void Update(Address address);

    }
}
