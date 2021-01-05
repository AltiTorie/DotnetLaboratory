using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab11.ViewModels;

namespace Lab11.DataContext
{
    public interface IDataContext
    {
        List<UserViewModel> GetUsers();
        UserViewModel GetUser(int id);
        void AddUser(UserViewModel user);
        void RemoveUser(int id);
        void UpdateUser(UserViewModel user);

    }
}
