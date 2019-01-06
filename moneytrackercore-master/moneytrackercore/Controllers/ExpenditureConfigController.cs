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
    public class ExpenditureConfigController : ControllerBase
    {
        private IExpenditureConfigRepository _expenditureConfigRepository;
        private ILogger<ExpenditureConfigController> _logger;
        private IMapper _mapper;

        public ExpenditureConfigController(IExpenditureConfigRepository expenditureConfigRepository, ILogger<ExpenditureConfigController> logger, IMapper mapper)
        {
            _expenditureConfigRepository = expenditureConfigRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ExpenditureConfigModel>>(_expenditureConfigRepository.GetAllExpenditureConfig()));
        }

        [HttpGet("{id}", Name = "ExpenditureConfigGet")]
        public IActionResult GetUser(int id)
        {
            return Ok(_mapper.Map<UsersModel>(_expenditureConfigRepository.GetExpenditureConfig(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExpenditureConfig model)
        {
            try
            {
                _logger.LogInformation("Creating a new Expenditure Config");
                _expenditureConfigRepository.Add(model);
                if (await _expenditureConfigRepository.SaveAllAsync())
                {
                    var newUri = Url.Link("expenditureConfigGet", new { id = model.Id });
                    return Created(newUri, model);
                }
                else
                {
                    _logger.LogWarning("Could not save Expenditure Config to the database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception while saving Expenditure Config: {ex}");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ExpenditureConfig model)
        {
            try
            {
                var oldexpenditureConfig = _expenditureConfigRepository.GetExpenditureConfig(id);
                if (oldexpenditureConfig == null) return NotFound($"Could not find a Expenditure Config with an ID of: {id}");

                oldexpenditureConfig.ExpenditureCategory = model.ExpenditureCategory ?? model.ExpenditureCategory;


                if (await _expenditureConfigRepository.SaveAllAsync())
                {
                    return Ok(oldexpenditureConfig);
                }

            }
            catch (Exception ex)
            {

            }

            return BadRequest("Couldn't update Expenditure Config");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldexpenditureConfig = _expenditureConfigRepository.GetExpenditureConfig(id);
                if (oldexpenditureConfig == null) return NotFound($"Could not find a Expenditure Config with an ID of: {id}");

                _expenditureConfigRepository.Delete(oldexpenditureConfig);


                if (await _expenditureConfigRepository.SaveAllAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest("Could not delete Expenditure Config");
        }
    }
}