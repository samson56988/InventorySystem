using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSofware.Models;

namespace InventoryManagementSofware.BuisnessInterface.AuthenticationService
{
    public interface IAuthenticationRepository
    {
        IEnumerable <Login> Login(Login login);

        void Registerusers(Login login);

        IList<Login> GetUsers();
    }
}
