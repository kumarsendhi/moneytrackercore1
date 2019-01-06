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
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;
        private ILogger<UsersController> _logger;
        private IMapper _mapper;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<UsersModel>>(_userRepository.GetAllUsers()));
        }

        [HttpGet("{id}", Name = "UserGet")]
        public IActionResult GetUser(int id)
        {
            return Ok(_mapper.Map<UsersModel>(_userRepository.GetUser(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Users model)
        {
            try
            {
                _logger.LogInformation("Creating a new User");
                _userRepository.Add(model);
                if (await _userRepository.SaveAllAsync())
                {
                    var newUri = Url.Link("UserGet", new { id = model.Id });
                    return Created(newUri,model);
                }
                else
                {
                    _logger.LogWarning("Could not save user to the database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception while saving User: {ex}");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody] Users model)
        {
            try
            {
                var oldUser = _userRepository.GetUser(id);
                if (oldUser == null) return NotFound($"Could not find a user with an ID of: {id}");

                oldUser.FirstName = model.FirstName ?? model.FirstName;
                oldUser.LastName = model.LastName ?? model.LastName;
                oldUser.Email = model.Email ?? model.Email;
                oldUser.Password = model.Password ?? model.Password;

                if (await _userRepository.SaveAllAsync())
                {
                    return Ok(oldUser);
                }

            }
            catch(Exception ex)
            {

            }

            return BadRequest("Couldn't update User");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldUser = _userRepository.GetUser(id);
                if (oldUser == null) return NotFound($"Could not find a user with an ID of: {id}");

                _userRepository.Delete(oldUser);


                if (await _userRepository.SaveAllAsync())
                {
                    return Ok();
                }
            }
            catch(Exception ex)
            {

            }
            return BadRequest("Could not delete user");
        }



    }
}