using System.Collections.Generic;

namespace PsychologyTest.Models
{
    public interface IPsyTestRepository
    {
        IEnumerable<PsyTestUser> GetAllConfirmedUsers();
        IEnumerable<PsyTestUser> GetAllUnconfirmedUsers();
        IEnumerable<PsyTestUser> GetAllUsers();
        IEnumerable<Grupo> GetAllGrupos();
        IEnumerable<Institucion> GetAllInstituciones();

        List<string> GetAllInstitucionNames();
        List<string> GetAllGrupoNames();

        Grupo GetGrupoById(int grupoId);
        Institucion GetInstitucionById(int v);

        void ConfirmEmail(PsyTestUser user);
        
        bool AddInstitucion(Institucion institucion);
        bool AddGrupo(ViewModels.GrupoViewModel grupo);
        
        bool DeleteGrupo(string grupoId);
        bool DeleteInstitucion(int instId);
        void DeleteUser(PsyTestUser user);
        void DeleteUserPerm(PsyTestUser user);

        void UpdateUser(string email, PsyTestUser vm);
        void UpdateInstitucion(Institucion newInstitucionData);
        void UpdateGrupo(Grupo newGrupoData);
        IEnumerable<PruebaPsicologica> GetAllTests(bool include);
        PruebaPsicologica AddTest(PruebaPsicologica prueba);
        void DeleteTest(int testId);
    }
}