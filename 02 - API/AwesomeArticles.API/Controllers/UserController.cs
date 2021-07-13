using AwesomeArticles.API.ApiResult;
using AwesomeArticles.Application.Services.Interfaces;
using AwesomeArticles.Application.ViewModels;
using AwesomeArticles.CrossCutting.Security;
using AwesomeArticles.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeArticles.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        
        [HttpPost("/api/user/add")]
        public async Task<IActionResult> AddUser([FromBody] UserViewModel userViewModel)
        {
            try
            {
                var user = await _userService.AddUser(userViewModel);

                return Ok(new Result("Usuário criado!", true, user));
            }
            catch(DomainException ex)
            {
                return StatusCode(400, Result.ResultBuilder.DomainError(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.ResultBuilder.InternalServerError());
            }
        }

        [HttpPost("/api/user/login")]
        public async Task<IActionResult> Login([FromBody] UserViewModel userViewModel)
        {
            try
            {
                var loginResponse = await _userService.Login(userViewModel);

                if (loginResponse is null)
                    return Unauthorized();
                
                return Ok(new Result("Usuário criado!", true, loginResponse));
            }
            catch (DomainException ex)
            {
                return StatusCode(401, Result.ResultBuilder.DomainError(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.ResultBuilder.InternalServerError());
            }
        }
    }
}
