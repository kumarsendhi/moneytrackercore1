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
    public class IncomesController : ControllerBase
    {
        private IIncomeRepository _incomeRepository;
        private ILogger<IncomesController> _logger;
        private IMapper _mapper;

        public IncomesController(IIncomeRepository incomeRepository, ILogger<IncomesController> logger, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<IncomesModel>>(_incomeRepository.GetAllIncomes()));
        }

        [HttpGet("{id}", Name = "IncomeGet")]
        public IActionResult GetUser(int id)
        {
            return Ok(_mapper.Map<UsersModel>(_incomeRepository.GetIncomes(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Incomes model)
        {
            try
            {
                _logger.LogInformation("Creating a new Incomes");
                _incomeRepository.Add(model);
                if (await _incomeRepository.SaveAllAsync())
                {
                    var newUri = Url.Link("IncomeGet", new { id = model.Id });
                    return Created(newUri, model);
                }
                else
                {
                    _logger.LogWarning("Could not save Income to the database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception while saving Income: {ex}");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Incomes model)
        {
            try
            {
                var oldIncome = _incomeRepository.GetIncomes(id);
                if (oldIncome == null) return NotFound($"Could not find a Income with an ID of: {id}");

                oldIncome.Date = model.Date;
                oldIncome.AmountEarned = model.AmountEarned;
                oldIncome.Comment = model.Comment ?? model.Comment;
                oldIncome.Incometype = model.Incometype ?? model.Incometype;

                if (await _incomeRepository.SaveAllAsync())
                {
                    return Ok(oldIncome);
                }

            }
            catch (Exception ex)
            {

            }

            return BadRequest("Couldn't update Income");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldIncome = _incomeRepository.GetIncomes(id);
                if (oldIncome == null) return NotFound($"Could not find a Income with an ID of: {id}");

                _incomeRepository.Delete(oldIncome);


                if (await _incomeRepository.SaveAllAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest("Could not delete Income");
        }
    }
}