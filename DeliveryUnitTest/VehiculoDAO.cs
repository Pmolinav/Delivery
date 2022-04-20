using DeliveryAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryUnitTest
{
    internal class VehiculoDAO
    {
        private readonly ApplicationDbContext _context;
        public VehiculoDAO(ApplicationDbContext contexto)
        {
            _context = contexto;
        }
        public bool ClaveRepetida(int id, int clave)
        {
            var registroRepetido = _context.Vehiculos
                                     .FirstOrDefault(c => c.Id ==
                                          clave && c.Id != id);
            return registroRepetido != null;
        }
    }
}
