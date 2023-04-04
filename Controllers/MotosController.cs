using Microsoft.AspNetCore.Mvc;
using MotosBackEnd.Dominio;
using MotosBackEnd.Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MotosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotosController : ControllerBase
    {

        private iDataApi DataApi; //Esto sumado a lo de abajo es la INYECCION DE DEPENDENCIA 

        public MotosController() // La llave de entrada para usar los metodos en el controlador 
        {
            DataApi= new DataApi();
        }

        
        [HttpGet("/Motos")]
        public IActionResult GetMotos()
        {
            return Ok(DataApi.ConsultarDB());
        }

       
        [HttpGet("/MotosPorModelos")]
        public IActionResult GetConModelo(string Modelo)
        {
            if(Modelo == null) 
                  return BadRequest("Debe ingresar un modelo!");
            return Ok(DataApi.ConsultarDBconParam(Modelo));
            
        }


        [HttpPost("/AgregarMotos")]
        public IActionResult PostMotos(Moto moto) // Busca por modelo con exito
        {
            try
            {
                if (moto == null)
                    return BadRequest("Falan datos para agregar una moto");
                if (DataApi.ModificarDB(moto))
                    return Ok("La moto fue agregada correctamente!");
                else
                    return Ok("La moto no pudo ser agregada");
            } 
            catch (Exception) 
            {
                throw;
            }
        }

        // PUT api/<MotosController>/5
        [HttpPut("/ModificarMotos")]
        public IActionResult PutMotos(Moto moto) // Modificacion de una moto con exito
        {
           
            try
            {
                if (moto == null)
                    return BadRequest("Los datos ingresados son incorrectos.");

                if (DataApi.ModificarDB(moto))
                    return Ok("La moto fue modificada con exito!");
                else
                    return Ok("La moto no pudo ser modificada");

            }
            catch (Exception) 
            {
                throw;
            }
        }

        // DELETE api/<MotosController>/5
        [HttpDelete("/EliminarMotos")]
        public IActionResult DeleteMotos(int codigo) // Elimina una moto con exito 
        {
            try
            {
                if (codigo == 0)
                    return BadRequest("Ingrese el codigo de la moto que quiere eliminar");

                if (DataApi.DeleteMotos(codigo))
                    return Ok("La moto fue eliminada con exito!");
                else
                    return Ok("La moto no pudo ser eliminada");

            }
            catch (Exception)
            {
                throw;
            }
        }
       
    }
}
