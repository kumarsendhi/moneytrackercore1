using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using moneytrackercore.data.Entities;
using moneytrackercore.Models;
using moneytrackercore.Services;

namespace moneytrackercore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeTypeController : ControllerBase
    {
        private IIncomeTypeRepository _incomeTypeRepository;
        private ILogger<IncomeTypeController> _logger;
        private IMapper _mapper;

        public IncomeTypeController(IIncomeTypeRepository incomeTypeRepository, ILogger<IncomeTypeController> logger, IMapper mapper)
        {
            _incomeTypeRepository = incomeTypeRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<IncomeTypeModel>>(_incomeTypeRepository.GetAllIncomeType()));
        }

        [HttpGet("{id}", Name = "IncomeTypeGet")]
        public IActionResult GetUser(int id)
        {
            return Ok(_mapper.Map<UsersModel>(_incomeTypeRepository.GetIncomeType(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]IncomeType model)
        {
            try
            {
                _logger.LogInformation("Creating a new Income Type");
                _incomeTypeRepository.Add(model);
                if (await _incomeTypeRepository.SaveAllAsync())
                {
                    var newUri = Url.Link("IncomeTypeGet", new { id = model.Id });
                    return Created(newUri, model);
                }
                else
                {
                    _logger.LogWarning("Could not save Income Type to the database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception while saving User: {ex}");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] IncomeType model)
        {
            try
            {
                var oldIncomeType = _incomeTypeRepository.GetIncomeType(id);
                if (oldIncomeType == null) return NotFound($"Could not find a Income Type with an ID of: {id}");

                oldIncomeType.IncomeTypeConfig = model.IncomeTypeConfig ?? model.IncomeTypeConfig;


                if (await _incomeTypeRepository.SaveAllAsync())
                {
                    return Ok(oldIncomeType);
                }

            }
            catch (Exception ex)
            {

            }

            return BadRequest("Couldn't update Income Type");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldIncomeType = _incomeTypeRepository.GetIncomeType(id);
                if (oldIncomeType == null) return NotFound($"Could not find a Income Type with an ID of: {id}");

                _incomeTypeRepository.Delete(oldIncomeType);


                if (await _incomeTypeRepository.SaveAllAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest("Could not delete Income Type");
        }
    }
}