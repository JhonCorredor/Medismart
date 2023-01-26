using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Logic;
using Model.DTO;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialCargasController : Controller
    {

        LHistorialCargas objLHistorialCargas = new LHistorialCargas();
        [HttpGet("All")]
        public ActionResult<ApiResponse<IEnumerable<HistorialCargasDTO>>> GetAll()
        {
            try
            {
                var data =  objLHistorialCargas.Consultar();

                if (data == null)
                {
                    var responseNull = new ApiResponse<IEnumerable<HistorialCargasDTO>>(null, false, "Registro no encontrado", null);
                    return NotFound(responseNull);
                }
                List<HistorialCargasDTO> lstHistorialCargasDTO = new List<HistorialCargasDTO>();
                foreach (var item in data)
                {
                    lstHistorialCargasDTO.Add(ConvertirDto(item));
                }

                var response = new ApiResponse<IEnumerable<HistorialCargasDTO>>((IEnumerable<HistorialCargasDTO>)data, true, "Ok", null);

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<HistorialCargasDTO>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Obtener por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<HistorialCargasDTO>> Get(int id)
        {
            try
            {
                var data = objLHistorialCargas.Consultar(id);

                if (data == null)
                {
                    var responseNull = new ApiResponse<HistorialCargasDTO>(null, false, "Registro no encontrado", null);
                    return NotFound(responseNull);
                }

                HistorialCargasDTO usersDTO = ConvertirDto(data);
                var response = new ApiResponse<HistorialCargasDTO>(usersDTO, true, "Ok", null);

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<HistorialCargasDTO>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] HistorialCargasDTO dataDto)
        {
            try
            {
                HistorialCargas users = ConvertirModel(dataDto);
                HistorialCargas data = objLHistorialCargas.Guardar(users);
                var response = new ApiResponse<HistorialCargas>(data, true, "Registro almacenado exitosamente", null);

                return new CreatedAtRouteResult(new { id = dataDto.Id }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<HistorialCargas>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put( [FromBody] HistorialCargasDTO dataDto)
        {
            try
            {
                HistorialCargas users = ConvertirModel(dataDto);
                HistorialCargas data = objLHistorialCargas.Actualizar(users);

                var response = new ApiResponse<HistorialCargasDTO>(null, true, "Registro actualizado exitosamente", null);

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<HistorialCargasDTO>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                objLHistorialCargas.Eliminar(id);
                return Ok(new ApiResponse<HistorialCargasDTO>(null, true, "Registro eliminado exitosamente", null));
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<HistorialCargasDTO>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        public HistorialCargasDTO ConvertirDto(HistorialCargas users)
        {
            HistorialCargasDTO usersDTO = new HistorialCargasDTO();
            usersDTO.Id = users.Id;
            usersDTO.Nombres = users.Nombres;
            usersDTO.ApellidoMaterno= users.ApellidoMaterno;
            usersDTO.ApellidoPaterno= users.ApellidoPaterno;
            usersDTO.Direccion = users.Direccion;
            usersDTO.FechaNacimento = users.FechaNacimento;
            
            return usersDTO;
        }

        public HistorialCargas ConvertirModel(HistorialCargasDTO usersDTO)
        {
            HistorialCargas users = new HistorialCargas();
            users.Id = usersDTO.Id;
            users.Nombres = usersDTO.Nombres;
            users.ApellidoMaterno = usersDTO.ApellidoMaterno;
            users.ApellidoPaterno = usersDTO.ApellidoPaterno;
            users.Direccion = usersDTO.Direccion;
            users.FechaNacimento= usersDTO.FechaNacimento;
            
            return users;
        }
    }
}
