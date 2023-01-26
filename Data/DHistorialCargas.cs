using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data
{
    public class DHistorialCargas
    {
        private dbMedismartContainer DbContext;

        public DHistorialCargas()
        {
            DbContext = new dbMedismartContainer();

        }

        public int Guardar(HistorialCargas objHistorialCargas)
        {
            try
            {
                tblHistorialCargas objHistorialCargasContext = new tblHistorialCargas();
                objHistorialCargasContext.Id = objHistorialCargas.Id;
                objHistorialCargasContext.Nombres = objHistorialCargas.Nombres;
                objHistorialCargasContext.ApellidoMaterno = objHistorialCargas.ApellidoMaterno;
                objHistorialCargasContext.ApellidoPaterno = objHistorialCargas.ApellidoPaterno;
                objHistorialCargasContext.Direccion = objHistorialCargas.Direccion;
                objHistorialCargasContext.FechaNacimiento = objHistorialCargas.FechaNacimento;
                DbContext.HistorialCargas.Add(objHistorialCargasContext);
                DbContext.SaveChanges();
                return objHistorialCargas.Id;
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

        public int Actualizar(HistorialCargas objHistorialCargas)
        {
            try
            {
                tblHistorialCargas objHistorialCargasContext = DbContext.HistorialCargas.First(v => v.Id == objHistorialCargas.Id);
                objHistorialCargasContext.Id = objHistorialCargas.Id;
                objHistorialCargasContext.Nombres = objHistorialCargas.Nombres;
                objHistorialCargasContext.ApellidoMaterno = objHistorialCargas.ApellidoMaterno;
                objHistorialCargasContext.ApellidoPaterno = objHistorialCargas.ApellidoPaterno;
                objHistorialCargasContext.Direccion = objHistorialCargas.Direccion;
                objHistorialCargasContext.FechaNacimiento = objHistorialCargas.FechaNacimento;
                DbContext.SaveChanges();
                return objHistorialCargas.Id;
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

        public void Eliminar(HistorialCargas objHistorialCargas)
        {
            try
            {
                var query = (from p in DbContext.HistorialCargas
                             where p.Id == objHistorialCargas.Id
                             select p).First();

                DbContext.HistorialCargas.Remove(query);
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

        public List<HistorialCargas> Consultar()
        {
            try
            {
                List<HistorialCargas> lstHistorialCargas = new List<HistorialCargas>();

                var query = from i in DbContext.HistorialCargas
                            select i;

                foreach (var item in query)
                {
                    lstHistorialCargas.Add(Convertir(item));
                }

                return lstHistorialCargas;

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

        public HistorialCargas Consultar(int Id)
        {
            try
            {
                HistorialCargas objHistorialCargas = new HistorialCargas();

                var query = (from i in DbContext.HistorialCargas
                             where i.Id == Id
                             select i).FirstOrDefault();

                objHistorialCargas = Convertir(query);
                return objHistorialCargas;

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

        public HistorialCargas Convertir(tblHistorialCargas HistorialCargasContext)
        {
            HistorialCargas objHistorialCargas = new HistorialCargas();
            objHistorialCargas.Id = HistorialCargasContext.Id;
            objHistorialCargas.Nombres = HistorialCargasContext.Nombres.ToUpper();
            objHistorialCargas.ApellidoMaterno = HistorialCargasContext.ApellidoMaterno.ToUpper();
            objHistorialCargas.ApellidoPaterno = HistorialCargasContext.ApellidoPaterno.ToUpper();
            objHistorialCargas.Direccion = HistorialCargasContext.Direccion.ToUpper();
            objHistorialCargas.FechaNacimento = HistorialCargasContext.FechaNacimiento;
            return objHistorialCargas;
        }
    }
}
