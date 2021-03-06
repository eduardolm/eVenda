﻿using Microsoft.EntityFrameworkCore;

namespace eVendas.Warehouse.Context
{
    public class DbInitializer
    {
        public static DbContextOptions<MainContext> DbOptions { get; set; }

        public static void Initialize(MainContext context)
        {
            DbOptions = new DbContextOptionsBuilder<MainContext>()
                .Options;

            context.Database.EnsureCreated();
        }
    }
}