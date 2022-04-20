using DeliveryAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryUnitTest
{
    internal class ContextMemoria
    {
        public ApplicationDbContext ObtenerContext()
        {
            //Se va a utilizar una base de datos en memoria
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .ConfigureWarnings
                            (x => x.Ignore(InMemoryEventId
                                    .TransactionIgnoredWarning))
                            .UseInMemoryDatabase(databaseName: "Test")
                                    .Options;

            var context = new ApplicationDbContext(options);

            return context;
        }
    }
}
