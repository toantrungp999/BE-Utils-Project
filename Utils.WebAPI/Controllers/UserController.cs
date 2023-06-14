﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utils.Application.Dto.User;
using Utils.Application.Dto;
using Utils.Application.Services.Interfaces;
using Utils.CrossCuttingConcerns.Constants;

namespace Utils.WebAPI.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = RoleConstant.Admin)]
        [HttpGet]
        public IActionResult GetUsers([FromQuery] PaginationRequestDto paginationRequest)
        {
            var result = _userService.GetUsers(paginationRequest);

            return Ok(result);
        }

        [HttpPost, Route("Authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] AuthenticateRequestDto request)
        {
            var result = _userService.Authenticate(request);

            return Ok(result);
        }

        [HttpPost, Route("CreateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] InsertUserRequestDto request)
        {
            var userId = await _userService.InsertUser(request);

            return Created(HttpContext.Request.Path, new
            {
                UserId = userId
            });
        }

        [HttpGet("UerInfo")]
        public IActionResult GetUserById()
        {
            var result = _userService.GetUserById((Guid)UserId);

            return Ok(result);
        }

        [HttpPut("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserRequestDto request)
        {
            await _userService.UpdateUserInfo((Guid)UserId, request);

            return Ok();
        }

        [Authorize(Roles = RoleConstant.Admin)]
        [Authorize(Roles = RoleConstant.SuperAdmin)]
        [HttpPut("UpdateUerRole")]
        public async Task<IActionResult> UpdateUerRole([FromBody] UpdateUserRuleRequestDto request)
        {
            await _userService.UpdateUserRule( request);

            return Ok();
        }
    }
}
