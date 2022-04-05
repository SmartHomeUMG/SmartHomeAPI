using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartBuilding.Models;

namespace SmartBuilding;


    public class SmartBuildingDBContext : DbContext
    {
        public DbSet<HomeVisitor> HomeVisitors {get;set;}
        public string DbPath {get;}

        public SmartBuildingDBContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path,"smarthome.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
    }