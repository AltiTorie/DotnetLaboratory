using Lab12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12.Data
{
    public class Cart
    {
        public Dictionary<int, int> Articles { get; private set; }

        public Cart()
        {
            Articles = new Dictionary<int, int>();
        }

        public void AddItem(Article article)
        {
            if (Articles.ContainsKey(article.Id))
            {
                Articles[article.Id] = Articles[article.Id] + 1;
            }
            else
            {
                Articles.Add(article.Id, 1);
            }
        }

        public void RemoveOneItem(Article article)
        {
            if (Articles[article.Id] > 1)
            {
                Articles[article.Id] = Articles[article.Id] - 1;
            }
            else
            {
                Articles.Remove(article.Id);
            }
        }

        public void RemoveItem(Article article)
        {
            if (Articles.ContainsKey(article.Id))
            {
                Articles.Remove(article.Id);
            }
        }
    }
}
