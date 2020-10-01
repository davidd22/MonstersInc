using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonstersInc.Models;
using MonstersIncLogic;

namespace MonstersInc.Controllers
{
    [Authorize]
    [ApiController]

    [Route("api/doors")]
    public class DoorsController : ControllerBase
    {
        private readonly IDoorRepository _doorRepository;

        private readonly IMapper _mapper;

        public DoorsController(IDoorRepository _doorRepository, IMapper _mapper)
        {
            this._doorRepository = _doorRepository ?? throw new ArgumentNullException(nameof(_doorRepository)); ;
            this._mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableAsync()
        {
            var availableDoors = await _doorRepository.SelectAvailableAsync();

            if (availableDoors == null)
                return NotFound();

            return Ok(availableDoors);
        }
    }
}
