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
    public class UsersController : Controller
    {

        LUsers objLUsers = new LUsers();
        [HttpGet("All")]
        public ActionResult<ApiResponse<IEnumerable<UsersDTO>>> GetAll()
        {
            try
            {
                var data =  objLUsers.Consultar();

                if (data == null)
                {
                    var responseNull = new ApiResponse<IEnumerable<UsersDTO>>(null, false, "Registro no encontrado", null);
                    return NotFound(responseNull);
                }
                List<UsersDTO> lstUsersDTO = new List<UsersDTO>();
                foreach (var item in data)
                {
                    lstUsersDTO.Add(ConvertirDto(item));
                }

                var response = new ApiResponse<IEnumerable<UsersDTO>>((IEnumerable<UsersDTO>)data, true, "Ok", null);

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<UsersDTO>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Obtener por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<UsersDTO>> Get(int id)
        {
            try
            {
                var data = objLUsers.Consultar(id);

                if (data == null)
                {
                    var responseNull = new ApiResponse<UsersDTO>(null, false, "Registro no encontrado", null);
                    return NotFound(responseNull);
                }

                UsersDTO usersDTO = ConvertirDto(data);
                var response = new ApiResponse<UsersDTO>(usersDTO, true, "Ok", null);

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<UsersDTO>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsersDTO dataDto)
        {
            try
            {
                Users users = ConvertirModel(dataDto);
                Users data = objLUsers.Guardar(users);
                var response = new ApiResponse<Users>(data, true, "Registro almacenado exitosamente", null);

                return new CreatedAtRouteResult(new { id = dataDto.Id }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<Users>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put( [FromBody] UsersDTO dataDto)
        {
            try
            {
                Users users = ConvertirModel(dataDto);
                Users data = objLUsers.Actualizar(users);

                var response = new ApiResponse<UsersDTO>(null, true, "Registro actualizado exitosamente", null);

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<UsersDTO>>(null, false, ex.Message.ToString(), null);
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
                objLUsers.Eliminar(id);
                return Ok(new ApiResponse<UsersDTO>(null, true, "Registro eliminado exitosamente", null));
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<UsersDTO>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        public UsersDTO ConvertirDto(Users users)
        {
            UsersDTO usersDTO = new UsersDTO();
            usersDTO.Id = users.Id;
            usersDTO.Identificacion = users.Identificacion;
            usersDTO.UserName = users.UserName;
            usersDTO.Password = users.Password;
            usersDTO.HistorialCargasDTO = new HistorialCargasDTO()
            {
                Id = users.HistorialCargas.Id,
            };
            return usersDTO;
        }

        public Users ConvertirModel(UsersDTO usersDTO)
        {
            Users users = new Users();
            users.Id = usersDTO.Id;
            users.Identificacion = usersDTO.Identificacion;
            users.UserName = usersDTO.UserName;
            users.Password = usersDTO.Password;
            users.HistorialCargas = new HistorialCargas()
            {
                Id = usersDTO.HistorialCargasDTO.Id,
            };
            return users;
        }
    }
}
