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

namespace WebMedismart.Controllers
{
    public class HistorialCargasController : ApiController
    {
        LHistorialCargas objLHistorialCargas = new LHistorialCargas();

        [NonAction]
        private List<HistorialCargasDTO> Consultar()
        {
            LHistorialCargas objLHistorialCargas = new LHistorialCargas();
            List<HistorialCargasDTO> HistorialCargassDTO = new List<HistorialCargasDTO>();
            List<HistorialCargas> HistorialCargass = objLHistorialCargas.Consultar();
            foreach (var item in HistorialCargass)
            {
                HistorialCargassDTO.Add(Convertir(item));
            }
            return HistorialCargassDTO;
        }

        [HttpGet]
        public IEnumerable<HistorialCargasDTO> GetAllHistorialCargas()
        {
            return Consultar();
        }

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            var HistorialCargas = objLHistorialCargas.Consultar(id);

            if (HistorialCargas != null)
            {
                return Ok(HistorialCargas);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public HttpResponseMessage PostHistorialCargas([FromBody] dynamic value)
        {
            string nombres = value["nombres"];
            string apellidoMaterno = value["apellidoMaterno"];
            string apellidoPaterno = value["apellidoPaterno"];
            string direccion = value["direccion"];
            string fechaNacimiento = value["fechaNacimiento"];

            HistorialCargas objHistorialCargas = objLHistorialCargas.Guardar(nombres, apellidoMaterno, apellidoPaterno, direccion, fechaNacimiento);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, Convertir(objHistorialCargas));
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = objHistorialCargas.Id }));
            return response;
        }

        [HttpPut]
        public HttpResponseMessage PutHistorialCargas([FromBody] dynamic value)
        {
            try
            {

                int id = Int32.Parse((string)value["id"]);
                string nombres = value["nombres"];
                string apellidoMaterno = value["apellidoMaterno"];
                string apellidoPaterno = value["apellidoPaterno"];
                string direccion = value["direccion"];
                string fechaNacimiento = value["fechaNacimiento"];

                if (id != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                HistorialCargas objHistorialCargas = objLHistorialCargas.Actualizar(id, nombres, apellidoMaterno, apellidoPaterno, direccion, fechaNacimiento);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, Convertir(objHistorialCargas));
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = objHistorialCargas.Id }));
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

        [HttpPatch]
        public HttpResponseMessage PatchHistorialCargas(int id, [FromBody] dynamic value)
        {
            try
            {
                // Consultar el objeto 
                HistorialCargas objHistorialCargas = objLHistorialCargas.Consultar(id);
                if (objHistorialCargas.Equals(null) || value == null)
                {
                    // El objeto no se encontro
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                // Actualizar los atributos que se envian
                string nombres = value["nombres"];
                string apellidoMaterno = value["apellidoMaterno"];
                string apellidoPaterno = value["apellidoPaterno"];
                string direccion = value["direccion"];
                string fechaNacimiento = value["fechaNacimiento"];

                HistorialCargas objHistorialCargasRst = objLHistorialCargas.Actualizar(id, nombres, apellidoMaterno, apellidoPaterno, direccion, fechaNacimiento);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, Convertir(objHistorialCargasRst));

                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = objHistorialCargas.Id }));
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Error al actualizar el objeto
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteHistorialCargas(Int32 id)
        {
            var HistorialCargas = objLHistorialCargas.Consultar(id);
            if (HistorialCargas == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                objLHistorialCargas.Eliminar(HistorialCargas.Id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, Convertir(HistorialCargas));
        }

        [NonAction]
        private HistorialCargasDTO Convertir(HistorialCargas objHistorialCargas)
        {
            HistorialCargasDTO objHistorialCargasDTO = new HistorialCargasDTO();
            objHistorialCargasDTO.Id = objHistorialCargas.Id;
            objHistorialCargasDTO.Nombres = objHistorialCargas.Nombres;
            objHistorialCargasDTO.ApellidoMaterno = objHistorialCargas.ApellidoMaterno;
            objHistorialCargasDTO.ApellidoPaterno = objHistorialCargas.ApellidoPaterno;
            objHistorialCargasDTO.Direccion= objHistorialCargas.Direccion;
            objHistorialCargasDTO.FechaNacimento= objHistorialCargas.FechaNacimento;
           
            return objHistorialCargasDTO;
        }
    }
}