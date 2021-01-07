using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab11.ViewModels;

namespace Lab11.DataContext
{
    public class MockDataContext : IDataContext
    {
        Dictionary<int, UserViewModel> users = new Dictionary<int, UserViewModel>()
        {
            { 0, new UserViewModel(0,"Arek321","Arek@gmail.com", "12-345", Category.Admin) },
            { 1,  new UserViewModel(1, "Tomek", "Tom123@yahoo.com", "96-243", Category.Buyer) },
            { 2,  new UserViewModel(2, "Potomek", "PamelaAnderson@gmail.com", "75-143", Category.Moderator) },
            { 3,  new UserViewModel(3, "Anijapania", "JakisEmail@emailowo.com", "671-24", Category.Seller) }
        };
        public void AddUser(UserViewModel person)
        {
            int nextId = users.Max(u => u.Key) + 1;
            person.Id = nextId;
            users.Add(nextId, person);
        }

        public UserViewModel GetUser(int id)
        {
            return users.FirstOrDefault(u => u.Key == id).Value;
        }

        public List<UserViewModel> GetUsers()
        {
            return users
                .Select(u => u.Value)
                .ToList<UserViewModel>();
        }

        public void RemoveUser(int id)
        {
            if (users.ContainsKey(id))
                users.Remove(id);
        }

        public void UpdateUser(UserViewModel person)
        {
            if (users.ContainsKey(person.Id))
            {
                users[person.Id] = person;
            }
            else throw new Exception("User does not exist");
        }
    }
}
