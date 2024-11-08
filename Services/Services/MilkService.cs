using Domain;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MilkService : IMilkService
    {
        IMilkRepository MilkRepository { get; set; }
        public MilkService(IMilkRepository MilkRepository)
        {
            this.MilkRepository = MilkRepository;
        }
        public void AddMilkExistences(Milk milk)
        {
            MilkRepository.Save(milk);
        }

        public Milk GetMilkExistences(Guid id)
        {
            Milk milk = MilkRepository.GetMilk(id);
            return milk; // Corregir para devolver la leche obtenida
        }

        public List<Milk> GetMilkByOwner(string nombre, string apellido)
        {
            // Lógica para obtener leche por nombre y apellido del dueño
            // Esto es solo un ejemplo básico; debes ajustarlo a tus necesidades reales

            var milkList = new List<Milk>
            {
                new Milk { Id = Guid.NewGuid(), Farm = "Farm A", ExpirationDate = DateTime.Now.AddDays(30), Litters = 100 },
                new Milk { Id = Guid.NewGuid(), Farm = "Farm B", ExpirationDate = DateTime.Now.AddDays(15), Litters = 200 }
            };

            // Filtrar o buscar en tu base de datos real aquí
            return milkList; // Aquí deberías devolver los resultados reales basados en nombre y apellido
        }
    }
}
