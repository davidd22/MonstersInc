using MonstersIncDomain;
using MonstersIncLogic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MonstersInc.Auth
{
    public static class AuthHelper
    {
        public const string CRYPTO_PASSWORD_VAR_NAME = "CRYPTO_PASSWORD";

        internal async static Task<int> GetIntimidatorId(ClaimsPrincipal user, IintimidatorRepository intimidatorRepository)
        {
            Intimidator intimidator = await GetIntimidatorAsync(user, intimidatorRepository);


            if (intimidator != null)
                return intimidator.Id;

            return 0;
        }


        internal static async Task<Intimidator> GetIntimidatorAsync(ClaimsPrincipal user, IintimidatorRepository intimidatorRepository)
        {
            var claims = (user.Claims.Select(c => new { c.Type, c.Value }));
            string client_id = claims.Where(t => t.Type == "client_id").SingleOrDefault().Value;

            return await intimidatorRepository.SelectByClientIdAsync(client_id, null);
        }
    }
}
