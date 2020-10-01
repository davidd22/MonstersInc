using IdentityServer4.Models;
using IdentityServer4.Stores;
using MonstersIncDomain;
using MonstersIncLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MonstersInc.Auth
{
    public class AuthClientStore : IClientStore
    {
        private readonly IintimidatorRepository _intimidatorRepository;

        public AuthClientStore(IintimidatorRepository _intimidatorRepository)
        {
            this._intimidatorRepository = _intimidatorRepository ?? throw new ArgumentNullException(nameof(_intimidatorRepository));
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            Intimidator intimidator = await _intimidatorRepository.SelectByClientIdAsync(clientId, Environment.GetEnvironmentVariable(AuthHelper.CRYPTO_PASSWORD_VAR_NAME));

            if (intimidator != null)
            {
                return new Client
                {
                    ClientId = clientId,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets = {
                                        new Secret(intimidator.ClientSecret.Sha256())
                                     },
                    AllowedScopes = { "api1" } ,
                    AccessTokenLifetime = 86400
                };
            }

            return null;

        }
    }
}
