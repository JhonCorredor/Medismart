using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Data.Entity.Infrastructure;
using System;
using Model;
using Logic;
using WebMedismart.Models;
using System.Web.Mvc;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebMedismart.Controllers
{
    public class UsersController : Controller
    {
        LUsers objLUsers = new LUsers();

        [NonAction]
        private List<UsersDTO> Consultar()
        {
            LUsers objLUsers = new LUsers();
            List<UsersDTO> UserssDTO = new List<UsersDTO>();
            List<Users> Userss = objLUsers.Consultar();
            foreach (var item in Userss)
            {
                UserssDTO.Add(Convertir(item));
            }
            return UserssDTO;
        }

        [HttpGet]
        public IEnumerable<UsersDTO> GetAllUsers()
        {
            return Consultar();
        }

        [HttpGet("{id}")]
        public  ActionResult<ApiResponse<UsersDTO>> Get(int id)
        {
            try
            {
                var data = await business.GetById(id);

                if (data == null)
                {
                    var responseNull = new ApiResponse<AreasDto>(null, false, "Registro no encontrado", null);
                    return NotFound(responseNull);
                }

                var response = new ApiResponse<AreasDto>(data, true, "Ok", null);

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<AreasDto>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        public HttpResponseMessage PostUsers([FromBody] dynamic value)
        {
            string identificacion = value["identificacion"];
            string userName = value["userName"];
            string password = value["password"];
            int idHistorialCargas = Int32.Parse((string)value["idHistorialCargas"]);

            Users objUsers = objLUsers.Guardar(identificacion, userName, password, idHistorialCargas);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, Convertir(objUsers));
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = objUsers.Id }));
            return response;
        }

        [HttpPut]
        public HttpResponseMessage PutUsers([FromBody] dynamic value)
        {
            try
            {

                int id = Int32.Parse((string)value["id"]);
                string identificacion = value["identificacion"];
                string userName = value["userName"];
                string password = value["password"];
                int idHistorialCargas = Int32.Parse((string)value["idHistorialCargas"]);

                if (id != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                Users objUsers = objLUsers.Actualizar(id, identificacion, userName, password, idHistorialCargas);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, Convertir(objUsers));
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = objUsers.Id }));
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

        [HttpPatch]
        public HttpResponseMessage PatchUsers(int id, [FromBody] dynamic value)
        {
            try
            {
                // Consultar el objeto 
                Users objUsers = objLUsers.Consultar(id);
                if (objUsers.Equals(null) || value == null)
                {
                    // El objeto no se encontro
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                // Actualizar los atributos que se envian
                string identificacion = value["identificacion"];
                string userName = value["userName"];
                string password = value["password"];
                int idHistorialCargas = Int32.Parse((string)value["idHistorialCargas"]);

                Users objUsersRst = objLUsers.Actualizar(id, identificacion, userName, password, idHistorialCargas);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, Convertir(objUsersRst));

                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = objUsers.Id }));
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Error al actualizar el objeto
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteUsers(Int32 id)
        {
            var Users = objLUsers.Consultar(id);
            if (Users == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                objLUsers.Eliminar(Users.Id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, Convertir(Users));
        }

        [NonAction]
        private UsersDTO Convertir(Users objUsers)
        {
            UsersDTO objUsersDTO = new UsersDTO();
            objUsersDTO.Id = objUsers.Id;
            objUsersDTO.Identificacion = objUsers.Identificacion;
            objUsersDTO.UserName = objUsers.UserName;
            objUsersDTO.Password = objUsers.Password;
            objUsersDTO.HistorialCargasDTO = new HistorialCargasDTO()
            {
                Id = objUsers.HistorialCargas.Id,
                Nombres = objUsers.HistorialCargas.Nombres,
                ApellidoMaterno = objUsers.HistorialCargas.ApellidoMaterno,
                ApellidoPaterno = objUsers.HistorialCargas.ApellidoPaterno,
                Direccion= objUsers.HistorialCargas.Direccion,
                FechaNacimento = objUsers.HistorialCargas.FechaNacimento,
        };
            return objUsersDTO;
        }
    }
}