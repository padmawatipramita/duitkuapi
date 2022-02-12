using Binus.WS.Pattern.Output;
using Binus.WS.Pattern.Service;
using DuitKu.API.Model;
using DuitKu.API.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DuitKu.API.Services
{
    [ApiController]
    [Route("transaction")]
    public class _TransactionService : BaseService
    {
        public _TransactionService(ILogger<BaseService> logger) : base(logger)
        {
        }

        // ADD NEW TRANSACTIONS
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(_TransactionOutput), StatusCodes.Status200OK)]
        public IActionResult AddNewItem([FromBody] _TransactionModel data)
        {
            try
            {
                var objJSON = new _TransactionOutput();
                objJSON.Success = Helper._TransactionHelper.AddNewTransaction(data);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

        // GET ALL TRANSACTIONS
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TransOutput), StatusCodes.Status200OK)]
        public IActionResult GetAllTransaction()
        {
            try
            {
                var objJSON = new TransOutput();
                objJSON.Data = Helper._TransactionHelper.GetAllTransaction();
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

        /*
           Modified by Ariel Sefrian
           Date: Jumat, 11/02/2022 - 22:04 WIB
           Purpose: added passing parameter
        */

        [HttpGet]
        [Route("current")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TransOutput), StatusCodes.Status200OK)]
        public IActionResult GetCurrentTransaction([FromQuery] _TransactionModel data)
            {
                try
                {
                    var objJSON = new TransOutput();
                    objJSON.Data = Helper._TransactionHelper.GetCurrentMonthTransaction(data);
                    return new OkObjectResult(objJSON);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new OutputBase(ex));
                }
            }
        
        [HttpGet]
        [Route("specificTrans")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TransOutput), StatusCodes.Status200OK)]
        public IActionResult GetSpecificTrans([FromQuery] _TransactionModel data)
        {
            try
            {
                var objJSON = new TransOutput();
                objJSON.Data = Helper._TransactionHelper.GetTransactionMonth(data);
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

        /*
           Modified by Ariel Sefrian 
           Date: Sabtu, 12/02/2022 - 11:21 WIB
           Purpose: added GetAllTransactionById
        */

        [HttpGet]
        [Route("getAllById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TransOutput), StatusCodes.Status200OK)]
        public IActionResult GetAllTransactionById([FromQuery] _TransactionModel data)
        {
            try
            {
                var objJSON = new TransOutput();
                objJSON.Data = Helper._TransactionHelper.GetAllTransactionById(data.UserID);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }
    }
}