using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Model;
using Utilities;

namespace Logic
{
    public class LHistorialCargas
    {
        DHistorialCargas objDHistorialCargas;

        public LHistorialCargas()
        {
            objDHistorialCargas = new DHistorialCargas();
        }

        public HistorialCargas Guardar(string nombres, string apellidoMaterno, string apellidoPaterno, string direccion, string fechaNacimiento)
        {
            try
            {
                objDHistorialCargas = new DHistorialCargas();
                HistorialCargas objHistorialCargas = new HistorialCargas();
                objHistorialCargas.Nombres = nombres;
                objHistorialCargas.ApellidoMaterno = apellidoMaterno;
                objHistorialCargas.ApellidoPaterno = apellidoPaterno;
                objHistorialCargas.Direccion = direccion;
                objHistorialCargas.FechaNacimento = Convert.ToDateTime(fechaNacimiento);
                objHistorialCargas.Id = objDHistorialCargas.Guardar(objHistorialCargas);
                return objHistorialCargas;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HistorialCargas Guardar(HistorialCargas historialCargas)
        {
            try
            {
                objDHistorialCargas = new DHistorialCargas();
                HistorialCargas objHistorialCargas = new HistorialCargas();
                objHistorialCargas.Nombres = historialCargas.Nombres;
                objHistorialCargas.ApellidoMaterno = historialCargas.ApellidoMaterno;
                objHistorialCargas.ApellidoPaterno = historialCargas.ApellidoPaterno;
                objHistorialCargas.Direccion = historialCargas.Direccion;
                objHistorialCargas.FechaNacimento = Convert.ToDateTime(historialCargas.FechaNacimento);
                objHistorialCargas.Id = objDHistorialCargas.Guardar(objHistorialCargas);
                return objHistorialCargas;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HistorialCargas Actualizar(int id, string nombres, string apellidoMaterno, string apellidoPaterno, string direccion, string fechaNacimiento)
        {
            try
            {

                HistorialCargas objHistorialCargas = objDHistorialCargas.Consultar(id);
                if (objHistorialCargas.Equals(null))
                {
                    throw new ApplicationException(Messages.HistorialCargasNull);
                }

                objDHistorialCargas = new DHistorialCargas();
                objHistorialCargas.Nombres = nombres;
                objHistorialCargas.ApellidoMaterno = apellidoMaterno;
                objHistorialCargas.ApellidoPaterno = apellidoPaterno;
                objHistorialCargas.Direccion = direccion;
                objHistorialCargas.FechaNacimento = Convert.ToDateTime(fechaNacimiento);
                objDHistorialCargas.Actualizar(objHistorialCargas);
                return objHistorialCargas;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HistorialCargas Actualizar(HistorialCargas historialCargas)
        {
            try
            {

                HistorialCargas objHistorialCargas = objDHistorialCargas.Consultar(historialCargas.Id);
                if (objHistorialCargas.Equals(null))
                {
                    throw new ApplicationException(Messages.HistorialCargasNull);
                }

                objDHistorialCargas = new DHistorialCargas();
                objHistorialCargas.Nombres = historialCargas.Nombres;
                objHistorialCargas.ApellidoMaterno = historialCargas.ApellidoMaterno;
                objHistorialCargas.ApellidoPaterno = historialCargas.ApellidoPaterno;
                objHistorialCargas.Direccion = historialCargas.Direccion;
                objHistorialCargas.FechaNacimento = Convert.ToDateTime(historialCargas.FechaNacimento);
                objDHistorialCargas.Actualizar(objHistorialCargas);
                return objHistorialCargas;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<HistorialCargas> Consultar()
        {
            try
            {
                return objDHistorialCargas.Consultar();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public HistorialCargas Consultar(int id)
        {
            try
            {
                return objDHistorialCargas.Consultar(id);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                HistorialCargas historialCargas = objDHistorialCargas.Consultar(id);
                if (historialCargas != null)
                {
                    objDHistorialCargas.Eliminar(historialCargas);
                }
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}