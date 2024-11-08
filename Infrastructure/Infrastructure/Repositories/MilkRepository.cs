using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MilkRepository : BaseRepository, IMilkRepository
    {
        public MilkRepository(AppDbContext context) : base(context)
        {
        }

        public Milk GetMilk(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return context.Milks.Find(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Save(Milk milk)
        {
            try
            {
                Beguin();
                context.Milks.Add(milk);
                Comit();
                Save();
            }
            catch (Exception ex) 
            {
                RollBack();
#pragma warning disable CA2200 // Rethrow to preserve stack details
                throw ex;
#pragma warning restore CA2200 // Rethrow to preserve stack details
            }
        }
    }
}
