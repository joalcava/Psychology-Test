using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PsychologyTest.Models
{
    public class PsyTestSeedData
    {
        private PsyTestContext _context;
        private UserManager<PsyTestUser> _userManager;

        public PsyTestSeedData(PsyTestContext context, UserManager<PsyTestUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task SeedDataTask()
        {
            if (await _userManager.FindByEmailAsync("psytest@psytest.com") == null) {
                var user = new PsyTestUser() {
                    UserName = "psytest_userseed",
                    Email = "psytest@psytest.com"
                };
                await _userManager.CreateAsync(user, "PsyTest_Admin123");
            }

            if (!_context.Instituciones.Any()) {
                
            }

            await _context.SaveChangesAsync();
        }
    }
}
