using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MonstersInc.Auth;
using MonstersInc.Models;
using MonstersIncDomain;
using MonstersIncLogic;
using MonstersIncLogic.Helper;
using System;
using System.Threading.Tasks;

namespace MonstersInc.Controllers
{
 
    [ApiController]

    [Route("api/intimidators")]
    public class IntimidatorsController : ControllerBase
    {
        private readonly IintimidatorRepository _intimidatorRepository;
     

        private readonly IMapper _mapper;

        public IntimidatorsController(IintimidatorRepository _intimidatorRepository, IMapper _mapper)                         
        {
            this._intimidatorRepository = _intimidatorRepository ?? throw new ArgumentNullException(nameof(_intimidatorRepository));
            this._mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            var intimidator = await _intimidatorRepository.SelectAsync(await AuthHelper.GetIntimidatorId(User, _intimidatorRepository)
                                                                    , Environment.GetEnvironmentVariable(AuthHelper.CRYPTO_PASSWORD_VAR_NAME)
                                                                );

            if (intimidator == null)
                return NotFound();

            return Ok(_mapper.Map<IntimidatorDto>(intimidator));
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(IntimidatorCreationDto dto)
        {
            if (await _intimidatorRepository.IsPhoneExistsAsync(dto.PhoneNumber))
            {
                return BadRequest(nameof(dto.PhoneNumber) + " already exists");
            }

            var new_intimidator = await _intimidatorRepository.InsertAsync(_mapper.Map<Intimidator>(dto)
                                                                         , Environment.GetEnvironmentVariable(AuthHelper.CRYPTO_PASSWORD_VAR_NAME)
                                                                         );



            if (new_intimidator != null)
            {
                new_intimidator.ClientSecret= new PasswordCrypto(new_intimidator.ClientSecret, Environment.GetEnvironmentVariable(AuthHelper.CRYPTO_PASSWORD_VAR_NAME)).Decrypt();
                return CreatedAtAction(nameof(Get), new { id = new_intimidator.Id }, new_intimidator);
            }

            return BadRequest();
        }
    }
}