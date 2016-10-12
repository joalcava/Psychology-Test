using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PsychologyTest.ViewModels;
using Microsoft.EntityFrameworkCore;

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
            user = Mapper.Map<PsyTestUser>(newData);
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

        public IEnumerable<string> GetAllInstitucionNames()
        {
            return _context.Instituciones.Select(n => n.Nombre).ToList();
        }
    }
}
