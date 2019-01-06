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
    public class ExpenditureController : ControllerBase
    {
        private IExpenditureRepository _expenditureRepository;
        private ILogger<ExpenditureController> _logger;
        private IMapper _mapper;

        public ExpenditureController(IExpenditureRepository expenditureRepository, ILogger<ExpenditureController> logger, IMapper mapper)
        {
            _expenditureRepository = expenditureRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ExpenditureModel>>(_expenditureRepository.GetAllExpenditure()));
        }

        [HttpGet("{id}", Name = "expenditureGet")]
        public IActionResult GetUser(int id)
        {
            return Ok(_mapper.Map<UsersModel>(_expenditureRepository.GetExpenditure(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Expenditure model)
        {
            try
            {
                _logger.LogInformation("Creating a new Expenditure");
                _expenditureRepository.Add(model);
                if (await _expenditureRepository.SaveAllAsync())
                {
                    var newUri = Url.Link("expenditureGet", new { id = model.Id });
                    return Created(newUri, model);
                }
                else
                {
                    _logger.LogWarning("Could not save Expenditure to the database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception while saving Expenditure: {ex}");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Expenditure model)
        {
            try
            {
                var oldexpenditure = _expenditureRepository.GetExpenditure(id);
                if (oldexpenditure == null) return NotFound($"Could not find a Expenditure with an ID of: {id}");

                oldexpenditure.AmountSpent = model.AmountSpent;


                if (await _expenditureRepository.SaveAllAsync())
                {
                    return Ok(oldexpenditure);
                }

            }
            catch (Exception ex)
            {

            }

            return BadRequest("Couldn't update Expenditure");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldexpenditure = _expenditureRepository.GetExpenditure(id);
                if (oldexpenditure == null) return NotFound($"Could not find a Expenditure with an ID of: {id}");

                _expenditureRepository.Delete(oldexpenditure);


                if (await _expenditureRepository.SaveAllAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest("Could not delete Expenditure");
        }
    }
}