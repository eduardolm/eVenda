using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using eVendas.Sales.Context;
using eVendas.Sales.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;

namespace eVendas.SaleTest.Context
{
    public class FakeContext
    {
        public FakeContext(string testName)
        {
           
            FakeOptions = new DbContextOptionsBuilder<MainContext>()
                .UseInMemoryDatabase($"Sales_{testName}")
                .Options;

            var path = @$"{AppDomain.CurrentDomain.BaseDirectory.Split(@"\bin")[0]}\FakeData\";

            DataFileNames.Add(typeof(Product), $"{path}products.json");
            DataFileNames.Add(typeof(Sale), $"{path}sales.json");
        }

        public DbContextOptions<MainContext> FakeOptions { get; }

        private Dictionary<Type, string> DataFileNames { get; } =
            new Dictionary<Type, string>();

        private string FileName<T>()
        {
            return DataFileNames[typeof(T)];
        }

        public void FillWithAll()
        {
            FillWith<Product>();
            FillWith<Sale>();
        }

        public void FillWith<T>() where T : class
        {
            using (var context = new MainContext(FakeOptions, FakeConfiguration().Object))
            {
                if (context.Set<T>().Any()) return;
                foreach (var item in GetFakeData<T>())
                    context.Set<T>().Add(item);
                context.SaveChanges();
            }
        }

        public List<T> GetFakeData<T>()
        {
            var content = File.ReadAllText(FileName<T>());
            return JsonConvert.DeserializeObject<List<T>>(content);
        }

        public Mock<IConfiguration> FakeConfiguration()
        {
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.GetSection(It.IsAny<String>())).Returns(new Mock<IConfigurationSection>().Object);
            return configuration;
        }
        
        // public Mock<IUserService> FakeUserService()
        // {
        //     var service = new Mock<IUserService>();
        //     var userList = GetFakeData<User>();
        //
        //     service.Setup(x => x.GetAll())
        //         .Returns(() => userList.ToList());
        //
        //     service.Setup(x => x.GetById(It.IsAny<int>())).
        //         Returns((int id) => GetFakeData<User>().FirstOrDefault(x => x.Id == id));
        //
        //     service.Setup(x => x.Create(It.IsAny<User>())).
        //         Returns((User user) => {
        //
        //                 if (user.Id == 0)
        //                     user.Id = 999;
        //             
        //                 return user;
        //         });
        //
        //     service.Setup(x => x.Update(It.IsAny<User>()))
        //         .Returns((User user) => user);
        //
        //     service.Setup(x => x.Delete(It.IsAny<int>()))
        //         .Returns((int id) =>
        //         {
        //             userList.Remove(userList[userList.FindIndex(x => x.Id == id)]);
        //             return userList;
        //         });
        //
        //     return service;
        // }
    }
}