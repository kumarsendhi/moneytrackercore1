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
    public class PaymentModeController : ControllerBase
    {
        private IPaymentModeRepository _paymentModeRepository;
        private ILogger<PaymentModeController> _logger;
        private IMapper _mapper;

        public PaymentModeController(IPaymentModeRepository paymentModeRepository, ILogger<PaymentModeController> logger, IMapper mapper)
        {
            _paymentModeRepository = paymentModeRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<PaymentModeModel>>(_paymentModeRepository.GetAllPaymentMode()));
        }

        [HttpGet("{id}", Name = "PaymentModeGet")]
        public IActionResult GetUser(int id)
        {
            return Ok(_mapper.Map<UsersModel>(_paymentModeRepository.GetPaymentMode(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentMode model)
        {
            try
            {
                _logger.LogInformation("Creating a new Payment Mode");
                _paymentModeRepository.Add(model);
                if (await _paymentModeRepository.SaveAllAsync())
                {
                    var newUri = Url.Link("PaymentModeGet", new { id = model.Id });
                    return Created(newUri, model);
                }
                else
                {
                    _logger.LogWarning("Could not save Payment Mode to the database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception while saving Payment Mode: {ex}");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PaymentMode model)
        {
            try
            {
                var oldPaymentMode = _paymentModeRepository.GetPaymentMode(id);
                if (oldPaymentMode == null) return NotFound($"Could not find a Payment Mode with an ID of: {id}");

                oldPaymentMode.PaymentConfig = model.PaymentConfig ?? model.PaymentConfig;


                if (await _paymentModeRepository.SaveAllAsync())
                {
                    return Ok(oldPaymentMode);
                }

            }
            catch (Exception ex)
            {

            }

            return BadRequest("Couldn't update Payment Mode");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldPaymentMode = _paymentModeRepository.GetPaymentMode(id);
                if (oldPaymentMode == null) return NotFound($"Could not find a Payment Mode with an ID of: {id}");

                _paymentModeRepository.Delete(oldPaymentMode);


                if (await _paymentModeRepository.SaveAllAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest("Could not delete Payment Mode");
        }

    }
}