﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using eVendas.Warehouse.Context;
using eVendas.Warehouse.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;

namespace eVendas.WarehouseTest.Context
{
    public class FakeContext
    {
        public FakeContext(string testName)
        {
           
            FakeOptions = new DbContextOptionsBuilder<MainContext>()
                .UseInMemoryDatabase($"Products_{testName}")
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
    }
}