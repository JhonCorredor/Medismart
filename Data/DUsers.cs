using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data
{
    public class DUsers
    {
        private dbMedismartContainer DbContext;

        public DUsers()
        {
            DbContext = new dbMedismartContainer();
        }

        public int Guardar(Users objUsers)
        {
            try
            {
                tblUsers objUsersContext = new tblUsers();
                objUsersContext.Id = objUsers.Id;
                objUsersContext.Identificacion = objUsers.Identificacion;
                objUsersContext.Username = objUsers.UserName;
                objUsersContext.Password = objUsers.Password;
                objUsersContext.HistorialCargas = DbContext.HistorialCargas.Single(i => i.Id == objUsers.HistorialCargas.Id);
                DbContext.Users.Add(objUsersContext);
                DbContext.SaveChanges();
                return objUsersContext.Id;

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Actualizar(Users objUsers)
        {
            try
            {
                tblUsers objUsersContext = DbContext.Users.First(v => v.Id == objUsers.Id);
                objUsersContext.Id = objUsers.Id;
                objUsersContext.Identificacion = objUsers.Identificacion;
                objUsersContext.Username = objUsers.UserName;
                objUsersContext.Password = objUsers.Password;
                objUsersContext.HistorialCargas = DbContext.HistorialCargas.Single(i => i.Id == objUsers.HistorialCargas.Id);
                DbContext.SaveChanges();
                return objUsersContext.Id;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Eliminar(Users objUsers)
        {
            try
            {
                var Accion = (from p in DbContext.Users
                              where p.Id == objUsers.Id
                              select p).First();

                DbContext.Users.Remove(Accion);
                DbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
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
                List<Users> lstUsers = new List<Users>();

                var query = (from i in DbContext.Users
                             select i).OrderBy(i => i.HistorialCargas.Nombres);

                foreach (var item in query)
                {
                    lstUsers.Add(Convertir(item));
                }

                return lstUsers;

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
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
                Users objUsers = new Users();

                var query = (from i in DbContext.Users
                             where i.Id == id
                             select i).FirstOrDefault();

                objUsers = Convertir(query);
                return objUsers;

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Users ConsultarPorHistorial(int idHistorial)
        {
            try
            {
                Users users = new Users();
                var query = (from i in DbContext.Users
                             where i.HistorialCargas.Id == idHistorial
                             select i);

                foreach (var item in query)
                {
                    users = (Convertir(item));
                }

                return users;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Users Convertir(tblUsers UsersContext)
        {
            Users objUsers = new Users();
            objUsers.Id = UsersContext.Id;
            objUsers.Identificacion = UsersContext.Identificacion;
            objUsers.Identificacion = UsersContext.Identificacion;
            objUsers.HistorialCargas = new HistorialCargas()
            {
                Id = UsersContext.HistorialCargas.Id,
                Nombres = UsersContext.HistorialCargas.Nombres,
                ApellidoMaterno = UsersContext.HistorialCargas.ApellidoMaterno,
                ApellidoPaterno = UsersContext.HistorialCargas.ApellidoPaterno,
                Direccion = UsersContext.HistorialCargas.Direccion,
                FechaNacimento = UsersContext.HistorialCargas.FechaNacimiento,

            };
            return objUsers;
        }
    }
}
