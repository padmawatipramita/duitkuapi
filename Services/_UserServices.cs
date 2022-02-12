using Binus.WS.Pattern.Output;
using Binus.WS.Pattern.Service;
using DuitKu.API.Model;
using DuitKu.API.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Duitku_API.Services
{
    [ApiController]
    [Route("user")]
    public class _UserServices : BaseService
    {
        public _UserServices(ILogger<BaseService> logger) : base(logger)
        {
        }

        // GET ALL USER
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDetail), StatusCodes.Status200OK)]
        public IActionResult GetAllUser()
        {
            try
            {
                var objJSON = new _UserOutputGet();
                objJSON.Data = DuitKu.API.Helper._UserHelper.GetAllUser();
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

        // ADD NEW USER
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(_UserOutputAddAndPatch), StatusCodes.Status200OK)]
        public IActionResult AddNewItem([FromBody] _UserModel data)
        {
            try
            {
                var objJSON = new _UserOutputAddAndPatch();
                objJSON.Success = DuitKu.API.Helper._UserHelper.AddNewUser(data);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

        // UPDATE PERSONAL DATA
        [HttpPatch]
        [Produces("application/json")]
        [ProducesResponseType(typeof(_UserOutputAddAndPatch), StatusCodes.Status200OK)]
        public IActionResult ChangePersonalData([FromBody] _UserModel data)
        {
            try
            {
                var objJSON = new _UserOutputAddAndPatch();
                objJSON.Success = DuitKu.API.Helper._UserHelper.ChangePersonalData(data);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

        // GET SPECIFIC USER
        /*
        Modified by Ariel Sefrian
        Date: Minggu, 06/02/2022 - 22:41 WIB
        Purpose: fix the getSpecificUser codes in _UserService
        */

        [HttpGet]
        [Route("specificId")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SpecificUserOutput), StatusCodes.Status200OK)]
        public IActionResult GetSpecificUserById([FromQuery] _UserModel data)
        {
            try
            {
                var objJSON = new SpecificUserOutput();
                objJSON.Data = DuitKu.API.Helper._UserHelper.GetSpecificUserById(data.UserID);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Account not found!"))
                {
                    return StatusCode(404, new OutputBase(ex)
                    {
                        ResultCode = 404,
                    });
                }
                else
                {
                    return StatusCode(500, new OutputBase(ex));
                }
            }
        }

        [HttpGet]
        [Route("specificEmail")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SpecificUserOutput), StatusCodes.Status200OK)]
        public IActionResult GetSpecificUserByEmail([FromQuery] _UserModel data)
        {
            try
            {
                var objJSON = new SpecificUserOutput();
                objJSON.Data = DuitKu.API.Helper._UserHelper.GetSpecificUserByEmail(data.UserEmail);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Account not found!"))
                {
                    return StatusCode(404, new OutputBase(ex)
                    {
                        ResultCode = 404,
                    });
                }
                else
                {
                    return StatusCode(500, new OutputBase(ex));
                }
            }
        }
    }
}