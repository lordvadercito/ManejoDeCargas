using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManejoDeCargas.Models.DB
{
    public class GestorDevesaDbContext
    {
        public static string GetStringConection()
        {
            var _configuration = new ConfigurationBuilder()
              .SetBasePath(System.IO.Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            var connectionStringPortal = _configuration.GetConnectionString("GestorDevesaDbContext");

            return connectionStringPortal;
        }
    }
}
