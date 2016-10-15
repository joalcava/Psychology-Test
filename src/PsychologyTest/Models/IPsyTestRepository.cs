using System.Collections.Generic;

namespace PsychologyTest.Models
{
    public interface IPsyTestRepository
    {
        IEnumerable<PsyTestUser> GetAllConfirmedUsers();
        IEnumerable<PsyTestUser> GetAllUnconfirmedUsers();
        IEnumerable<PsyTestUser> GetAllUsers();
        void ConfirmEmail(PsyTestUser user);
        void DeleteUser(PsyTestUser user);
        void DeleteUserPerm(PsyTestUser user);
        void UpdateUser(string email, PsyTestUser vm);
        IEnumerable<Grupo> GetAllGrupos();
        IEnumerable<Institucion> GetAllInstituciones();
        List<string> GetAllInstitucionNames();
        bool AddInstitucion(Institucion institucion);
        bool AddGrupo(ViewModels.GrupoViewModel grupo);
        List<string> GetAllGrupoNames();
        bool DeleteGrupo(string grupoId);
        bool DeleteInstitucion(string instId);
        Grupo GetGrupoById(int grupoId);
        Institucion GetInstitucionById(int v);
    }
}