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
    public class BalanceController : ControllerBase
    {
        private IBalanceRepository _balanceRepository;
        private ILogger<BalanceController> _logger;
        private IMapper _mapper;

        public BalanceController(IBalanceRepository balanceRepository, ILogger<BalanceController> logger, IMapper mapper)
        {
            _balanceRepository = balanceRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<BalanceModel>>(_balanceRepository.GetAllBalance()));
        }

        [HttpGet("{id}", Name = "balanceGet")]
        public IActionResult GetUser(int id)
        {
            return Ok(_mapper.Map<UsersModel>(_balanceRepository.GetBalance(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Balance model)
        {
            try
            {
                _logger.LogInformation("Creating a new Balance");
                _balanceRepository.Add(model);
                if (await _balanceRepository.SaveAllAsync())
                {
                    var newUri = Url.Link("balanceGet", new { id = model.Id });
                    return Created(newUri, model);
                }
                else
                {
                    _logger.LogWarning("Could not save Balance to the database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception while saving Balance: {ex}");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Balance model)
        {
            try
            {
                var oldbalance = _balanceRepository.GetBalance(id);
                if (oldbalance == null) return NotFound($"Could not find a Balance with an ID of: {id}");

                oldbalance.BalanceAmount = model.BalanceAmount;


                if (await _balanceRepository.SaveAllAsync())
                {
                    return Ok(oldbalance);
                }

            }
            catch (Exception ex)
            {

            }

            return BadRequest("Couldn't update Balance");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldbalance = _balanceRepository.GetBalance(id);
                if (oldbalance == null) return NotFound($"Could not find a Balance with an ID of: {id}");

                _balanceRepository.Delete(oldbalance);


                if (await _balanceRepository.SaveAllAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest("Could not delete Balance");
        }
    }
}