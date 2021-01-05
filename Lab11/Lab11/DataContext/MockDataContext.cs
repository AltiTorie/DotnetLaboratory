using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab11.ViewModels;

namespace Lab11.DataContext
{
    public class MockDataContext : IDataContext
    {

        List<UserViewModel> users = new List<UserViewModel>()
        {
            new UserViewModel(0,"Arek","Arek@gmail.com", "12-345", Category.Admin),
            new UserViewModel(1, "Tomek", "Tom123@yahoo.com", "96-243", Category.Buyer)
        };
        public void AddUser(UserViewModel person)
        {
            int nextId = users.Max(u => u.Id) + 1;
            person.Id = nextId;
            users.Add(person);
        }

        public UserViewModel GetUser(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        public List<UserViewModel> GetUsers()
        {
            return users;
        }

        public void RemoveUser(int id)
        {
            UserViewModel userToRemove = users.FirstOrDefault(u => u.Id == id);
            if (userToRemove != null)
                users.Remove(userToRemove);
        }

        public void UpdateUser(UserViewModel person)
        {
            UserViewModel userToUpdate = users.FirstOrDefault(u => u.Id == person.Id);
            users = users.Select(u => (u.Id == person.Id) ? person : u).ToList();
        }
    }
}
