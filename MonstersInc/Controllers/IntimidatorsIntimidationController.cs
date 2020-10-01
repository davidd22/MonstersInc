using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MonstersInc.Auth;
using MonstersInc.Models;
using MonstersIncLogic;
using System;
using System.Threading.Tasks;

namespace MonstersInc.Controllers
{
    [Authorize]

    [ApiController]
    [Route("api/intimidators/intimidation")]
    public class IntimidatorsIntimidationController : ControllerBase
    {
        private readonly IintimidationRepository _intimidationRepository;
        private readonly IntimidatorIntimidationsCacheChannel _intimidatorIntimidationsCacheChannel;

        private readonly IintimidatorRepository _intimidatorRepository;
        public IntimidatorsIntimidationController(IintimidationRepository _intimidationRepository
                                                 , IntimidatorIntimidationsCacheChannel _intimidatorIntimidationsCacheChannel

                                                 , IintimidatorRepository _intimidatorRepository)
        {
            this._intimidationRepository = _intimidationRepository ?? throw new ArgumentNullException(nameof(_intimidationRepository));
            this._intimidatorIntimidationsCacheChannel = _intimidatorIntimidationsCacheChannel ?? throw new ArgumentNullException(nameof(_intimidatorIntimidationsCacheChannel));
         
            this._intimidatorRepository = _intimidatorRepository ?? throw new ArgumentNullException(nameof(_intimidatorRepository));
        }

        [HttpPost]
        public async Task<IActionResult> Start(IntimidatorDoorSelectionDto intimidatorDoorSelectionDto)
        {
            var updated = await _intimidationRepository.Start(intimidatorDoorSelectionDto.DoorId, await AuthHelper.GetIntimidatorId(User, _intimidatorRepository));

            if (updated == false)
                return BadRequest("door is not available");

            await _intimidatorIntimidationsCacheChannel.WriteToIntimidationsSummeryChannel(await AuthHelper.GetIntimidatorId(User, _intimidatorRepository));

            return Created(string.Empty, updated);
        }
        [HttpPatch]
        public async Task<IActionResult> End(IntimidatorDoorSelectionDto intimidatorDoorSelectionDto)
        {
            var updated = await _intimidationRepository.End(intimidatorDoorSelectionDto.DoorId, await AuthHelper.GetIntimidatorId(User, _intimidatorRepository));

            if (updated == false)
                return BadRequest("door is not available");

            await _intimidatorIntimidationsCacheChannel.WriteToIntimidationsSummeryChannel(await AuthHelper.GetIntimidatorId(User, _intimidatorRepository));


            return Accepted(updated);
        }
    }
}
