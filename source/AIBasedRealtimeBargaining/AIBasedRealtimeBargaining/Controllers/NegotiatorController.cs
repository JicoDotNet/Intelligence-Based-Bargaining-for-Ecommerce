using AIBasedRealtimeBargaining.Logic;
using AIBasedRealtimeBargaining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AIBasedRealtimeBargaining.Controllers
{
    public class NegotiatorController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Negotiate(RequestCommand command)
        {
            try
            {
                if(command.TokenKey != GenericLogic.DefaultToken)
                {
					return Ok(new ResponseCommand { IsSuccess = false, StatusCode = HttpStatusCode.Unauthorized, Message = "You are Unauthorized.", _request = command, _response = null });
				}
                if (command == null
                        || string.IsNullOrEmpty(command.Tenant)
                        || command.OfferPrice <= 0
                        || command.ThresholdPrice <= 0
                        || command.ThresholdPrice >= command.OfferPrice
                        || command.CustomerId <= 0
                        || command.ProductId <= 0
                        || command.ProposedCost <= 0)
                {
					return Ok(new ResponseCommand { IsSuccess = false, StatusCode = HttpStatusCode.Forbidden, Message = "Param Mismatch", _request = command, _response = null });
				}
                NegotiatorLogic negotiator = new NegotiatorLogic();
                ResponseCommand response = new ResponseCommand()
                {
                    IsSuccess = true,
                    Message = "Success",
                    StatusCode = HttpStatusCode.OK,
                    _request = command,
                    _response = negotiator.NextValue(command)
                };
                return Ok(response);
            }
            catch (Exception Ex)
            {
				return Ok(new ResponseCommand { IsSuccess = false, StatusCode = HttpStatusCode.InternalServerError, Message = Ex.Message, _request = command, _response = null });
			}
        }
	}
}
