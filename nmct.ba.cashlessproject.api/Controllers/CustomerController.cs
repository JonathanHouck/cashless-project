﻿using nmct.ba.cashlessproject.api.Models;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace nmct.ba.cashlessproject.api.Controllers
{
    [Authorize]
    public class CustomerController : ApiController
    {
        // GET: api/Customer
        public List<Customer> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return CustomerDA.GetCustomers(p.Claims);
        }

        // GET: api/Customer/5
        public Customer GetCustomerByNationalNumber(string cNationalNumber)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return CustomerDA.GetCustomerByNationalNumber(cNationalNumber, p.Claims);
        }

        public Boolean GetCheckCustomerExists(string nationalNumber)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return CustomerDA.checkCustomerExists(nationalNumber, p.Claims);
        }

        public HttpResponseMessage Post(Customer c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int id = CustomerDA.InsertCustomer(c, p.Claims);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(id.ToString());
            return message;
        }

        public HttpResponseMessage Put(Customer c)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            CustomerDA.UpdateCustomer(c, p.Claims);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            CustomerDA.DeleteCustomer(id, p.Claims);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
