using MagentoIntegrationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagentoIntegrationApp
{
    public interface ICustomer
    {
        Task GetAsync(int customerId);

        Task<bool> Update(Customer entity);

        Task<bool> Delete(int customerId);

       


    }
}


