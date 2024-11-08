using ApiSampleFinal.Models.MilkModels;
using AutoMapper;
using Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiSampleFinal.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MilkMapper();
        }

        private void MilkMapper()
        {
            CreateMap<Milk, MilkDTO>()
            .ReverseMap();

        }

    }

    
}
