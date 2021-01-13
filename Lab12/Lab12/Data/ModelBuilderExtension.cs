using Lab12.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Moto"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Gaming"
                }
                ) ;
            modelBuilder.Entity<Article>().HasData(
                new Article()
                {
                    Id = 1,
                    Name = "ArticleOne",
                    Price = 50.0,
                    CategoryId = 1,
                    PathToImage = "",

        },
                new Article()
                {
                    Id = 2,
                    Name = "ArticleTwo",
                    Price = 25.0,
                    CategoryId = 2,
                    PathToImage = "",

        }

                );

        }
    }
}
