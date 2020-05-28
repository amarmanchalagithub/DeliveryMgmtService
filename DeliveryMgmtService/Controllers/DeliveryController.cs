using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DeliveryMgmtService.Controllers
{
    [RoutePrefix("api/Delivery")]
    public class DeliveryController : ApiController
    {
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        [Route("UpdatedOrder")]
        public HttpResponseMessage UpdateDeliveryStatus([FromBody]ORDER oderdata)
        {
            if (oderdata != null)
            {
                using (var context = new ordermgmtDBEntities())
                {
                    var newOrderData = new ORDER();
                    newOrderData = oderdata;
                    var resultObject = context.ORDERS.Add(newOrderData);
                    context.Entry(newOrderData).State = System.Data.Entity.EntityState.Modified;
                    int newlyinsertedOrderId = context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, newlyinsertedOrderId);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not updated");
            }
        }


        [HttpGet]
        [Route("TestDelivery")]
        public string TestDelivery()
        {
            return "test Delivery Success";
        }

    }
}
