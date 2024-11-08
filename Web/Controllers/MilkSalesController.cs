using ApiSampleFinal.Models.MilkModels;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ApiSampleFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilkSalesController : ControllerBase
    {
        public IMilkService MilkService { get; set; }
        //adicionar imapper por injeccion de dependencias
        public IMapper Mapper { get; set; }

        public MilkSalesController(IMilkService service, IMapper mapper)
        {
            MilkService = service;
            Mapper = mapper;
        }   

        [HttpGet("GetMilkExistences")]
        public IActionResult GetMilkExistences(Guid id)
        {
            Milk Availablemilk = MilkService.GetMilkExistences(id);
            return Ok(Mapper.Map<MilkDTO>(Availablemilk));
        }

        [HttpPost("AddMilkExistences")]
        public IActionResult AddMilkExistences([FromBody] MilkDTO milk)
        {
            MilkService.AddMilkExistences(Mapper.Map<Milk>(milk));
            return Ok();
        }

        // Nuevo método para aceptar nombre y apellido
        [HttpGet("GetMilkByOwner")]
        public IActionResult GetMilkByOwner(string nombre, string apellido)
        {
            var result = MilkService.GetMilkByOwner(nombre, apellido);
            return Ok(result);
        }
    }
}
