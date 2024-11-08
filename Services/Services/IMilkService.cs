using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IMilkService
    {
        void AddMilkExistences(Milk milk);
        Milk GetMilkExistences(Guid id);
        List<Milk> GetMilkByOwner(string nombre, string apellido); // Añadir esta línea
    }
}
