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
    public class LUsers
    {
        DUsers objDUsers;

        public LUsers()
        {
            objDUsers = new DUsers();
        }

        public Users Guardar(string identificacion, string userName, string password, int idHistorialCargas)
        {
            try
            {
                objDUsers = new DUsers();
                Users objUsers = new Users();
                objUsers.Identificacion = identificacion;
                objUsers.UserName = userName;
                objUsers.Password = password;
                objUsers.HistorialCargas = new HistorialCargas { Id = idHistorialCargas };
                objUsers.Id = objDUsers.Guardar(objUsers);
                return objUsers;
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

        public Users Guardar(Users users)
        {
            try
            {
                objDUsers = new DUsers();
                Users objUsers = new Users();
                objUsers.Identificacion = users.Identificacion;
                objUsers.UserName = users.UserName;
                objUsers.Password = users.Password;
                objUsers.HistorialCargas = new HistorialCargas { Id = users.HistorialCargas.Id };
                objUsers.Id = objDUsers.Guardar(objUsers);
                return objUsers;
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

        public Users Actualizar(int id, string identificacion, string userName, string password, int idHistorialCargas)
        {
            try
            {

                Users objUsers = objDUsers.Consultar(id);
                if (objUsers.Equals(null))
                {
                    throw new ApplicationException(Messages.UsersNull);
                }

                objDUsers = new DUsers();
                objUsers.Identificacion = identificacion;
                objUsers.UserName = userName;
                objUsers.Password = password;
                objUsers.HistorialCargas = new HistorialCargas { Id = idHistorialCargas };
                objDUsers.Actualizar(objUsers);
                return objUsers;
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

        public Users Actualizar(Users users)
        {
            try
            {

                Users objUsers = objDUsers.Consultar(users.Id);
                if (objUsers.Equals(null))
                {
                    throw new ApplicationException(Messages.UsersNull);
                }

                objDUsers = new DUsers();
                objUsers.Identificacion = users.Identificacion;
                objUsers.UserName = users.UserName;
                objUsers.Password = users.Password;
                objUsers.HistorialCargas = new HistorialCargas { Id = users.HistorialCargas.Id};
                objDUsers.Actualizar(objUsers);
                return objUsers;
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


        public List<Users> Consultar()
        {
            try
            {
                return objDUsers.Consultar();
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

        public Users Consultar(int id)
        {
            try
            {
                return objDUsers.Consultar(id);
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
                Users historialCargas = objDUsers.Consultar(id);
                if (historialCargas != null)
                {
                    objDUsers.Eliminar(historialCargas);
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