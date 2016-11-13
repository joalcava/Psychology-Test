using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PsychologyTest.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace PsychologyTest.Models
{
    public class PsyTestRepository : IPsyTestRepository
    {
        private PsyTestContext _context;

        public PsyTestRepository(PsyTestContext context)
        {
            _context = context;
        }

        public IEnumerable<PsyTestUser> GetAllUnconfirmedUsers()
        {
            return _context.Users.Where(u => u.EmailConfirmed == false).ToList();
        }

        public IEnumerable<PsyTestUser> GetAllConfirmedUsers()
        {
            return _context.Users.Where(u => u.EmailConfirmed == true).ToList();
        }

        public IEnumerable<PsyTestUser> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void ConfirmEmail(PsyTestUser user)
        {
            var t = _context.Users.First(u => u.Email == user.Email);
            t.EmailConfirmed = true;
            _context.SaveChanges();
        }

        public void DeleteUser(PsyTestUser user)
        {
            var t = _context.Users.First(u => u.Email == user.Email);
            DeletedUsers tReversed = new DeletedUsers {
                Nombres = t.Nombres,
                Apellidos = t.Apellidos,
                TipoDocId = t.TipoDocId,
                DocId = t.DocId,
                Genero = t.Genero,
                Direccion = t.Direccion,
                FechaRegistro = t.FechaRegistro,
                RolSolicitado = t.RolSolicitado,
                Email = t.Email,
                UserName = t.UserName,
                PhoneNumber = t.PhoneNumber,
                EmailConfirmed = t.EmailConfirmed
            };
            _context.UsuariosViejos.Add(tReversed);
            _context.Remove<PsyTestUser>(t);
            _context.SaveChanges();
        }

        public void DeleteUserPerm(PsyTestUser user)
        {
            var t = _context.Users.First(u => u.Email == user.Email);
            _context.Remove<PsyTestUser>(t);
            _context.SaveChanges();
        }

        public void UpdateUser(string email, PsyTestUser newData)
        {
            var user = _context.Users.First(u => u.Email == email);
            _context.Update<PsyTestUser>(user);
            user.Nombres = newData.Nombres;
            user.Apellidos = newData.Apellidos;
            user.TipoDocId = newData.TipoDocId;
            user.DocId = newData.DocId;
            user.Direccion = newData.Direccion;
            user.PhoneNumber = newData.PhoneNumber;
            user.Genero = newData.Genero;
            //TODO: Cambiar de rol realmente
            user.RolSolicitado = newData.RolSolicitado;
            _context.SaveChanges();
        }

        public IEnumerable<Grupo> GetAllGrupos()
        {
            return _context.Grupos
                .Include(grupo => grupo.Institucion)
                .ToList();
        }

        public IEnumerable<Institucion> GetAllInstituciones()
        {
            return _context.Instituciones
                .Include(institucion => institucion.Grupos)
                .ToList();
        }

        public bool AddGrupo(GrupoViewModel grupo)
        {
            try {
                var inst = _context.Instituciones.First(i => i.Nombre == grupo.Inst);
                var grup = Mapper.Map<Grupo>(grupo);
                grup.Institucion = inst;
                _context.Grupos.Add(grup);
                _context.Update<Institucion>(inst);
                inst.Grupos.Add(grup);
                _context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public bool AddInstitucion(Institucion institucion)
        {
            try {
                institucion.Grupos = new List<Grupo>();
                _context.Instituciones.Add(institucion);
                _context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public List<string> GetAllInstitucionNames()
        {
            return _context.Instituciones.Select(n => n.Nombre).ToList();
        }

        public List<string> GetAllGrupoNames()
        {
            return _context.Grupos.Select(n => n.Nombre).ToList();
        }

        public bool DeleteGrupo(string grupoId)
        {
            try {
                var g = _context.Grupos.First(i => i.Id == Convert.ToInt32(grupoId));
                _context.Remove<Grupo>(g);
                _context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public bool DeleteInstitucion(int instId)
        {
            try {
                var inst = _context.Instituciones.
                    Include(x => x.Grupos)
                    .First(i => i.Nit == instId);
                foreach (var grupo in inst.Grupos) {
                    _context.Remove(grupo);
                }
                _context.SaveChanges();
                _context.Remove(inst);
                _context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public Grupo GetGrupoById(int grupoId)
        {
            return _context.Grupos.Include(x => x.Institucion).First(i => i.Id == grupoId);
        }

        public Institucion GetInstitucionById(int instId)
        {
            return _context.Instituciones.First(i => i.Nit == instId);
        }

        public void UpdateInstitucion(Institucion newInstitucionData)
        {
            var inst = _context.Instituciones.First(i => i.Nit == newInstitucionData.Nit);
            _context.Update(inst);
            inst.Nit = newInstitucionData.Nit;
            inst.Nombre = newInstitucionData.Nombre;
            inst.SitioWeb = newInstitucionData.SitioWeb;
            inst.Telefono = newInstitucionData.Telefono;
            inst.Ciudad = newInstitucionData.Ciudad;
            inst.Direccion = newInstitucionData.Direccion;
            _context.SaveChanges();
        }

        public void UpdateGrupo(Grupo newGrupoData)
        {
            var grupo = _context.Grupos.First(g => g.Id == newGrupoData.Id);
            _context.Update(grupo);
            grupo.Grado = newGrupoData.Grado;
            grupo.Institucion = newGrupoData.Institucion;
            grupo.Jornada = newGrupoData.Jornada;
            grupo.Nombre = newGrupoData.Nombre;
            _context.SaveChanges();
        }

        public IEnumerable<PruebaPsicologica> GetAllTests(bool include = false)
        {
            if (!include)
                return _context.PruebasPsicologicas.ToList();
            return _context.PruebasPsicologicas.Include(x => x.Preguntas).ToList();
        }
    }
}
