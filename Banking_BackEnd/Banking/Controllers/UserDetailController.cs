﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Banking.Models;
using System.Web.Http.Cors;

namespace Banking.Controllers
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class UserDetailController : ApiController
    {

        public HttpResponseMessage GetDetails()
        {
            using (BankingDbEntities db = new BankingDbEntities())
            {
                var data = db.UserDetails.Where(user => user.Approval_Status == "no").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }

        public HttpResponseMessage GetDetails(int id)
        {
            using (BankingDbEntities db = new BankingDbEntities())
            {
                var data = db.UserDetails.Find(id);
                if (data != null)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Userdetail with refid= " + id + " not found");
            }

        }

        public HttpResponseMessage GetRefernceid(int id)
        {
            using (BankingDbEntities db = new BankingDbEntities())
            {
                var data = db.UserDetails.Where(user => user.Aadhar_Number == id).FirstOrDefault();
               
                if (data != null)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Userdetail with Aadharnumber= " + id + " not found");
            }

        }
        public HttpResponseMessage PostDetails([FromBody] UserDetail detail)
        {
            try
            {
                using (BankingDbEntities db = new BankingDbEntities())
                {
                    detail.Approval_Status = "no";
                    db.UserDetails.Add(detail);
                    db.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.Created, detail);

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage PutDetails(int id, [FromBody] UserDetail details)
        {
            try
            {
                using (BankingDbEntities db = new BankingDbEntities())
                {
                    var data = db.UserDetails.Find(id);

                    if (data == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Dept with id= " + id + " not found");
                    }
                    else
                    {
                        data.Mobile_Number = details.Mobile_Number;
                        data.Email_Id = details.Email_Id;
                        data.Occupation_type = details.Occupation_type;
                        data.Source_of_Income = details.Source_of_Income;
                        data.PermanentAddress_Line1 = details.PermanentAddress_Line1;
                        data.PermanentAddress_Line2 = details.PermanentAddress_Line2;
                        data.PermanentCity = details.PermanentCity;
                        data.PermanentLankmark = details.PermanentLankmark;
                        data.PermanentState = details.PermanentState;
                        data.PermanentPincode = details.PermanentPincode;
                        data.Address_Line1 = details.Address_Line1;
                        data.Address_Line2 = details.Address_Line2;
                        data.City = details.City;
                        data.Lankmark = details.Lankmark;
                        data.State = details.State;
                        data.Pincode = details.Pincode;

                        data.Gross_Annual_Income = details.Gross_Annual_Income;
                        if (data.Approval_Status == "no")
                        {
                            if (details.Approval_Status == "yes")
                            {
                                data.Approval_Status = details.Approval_Status;
                            }
                        }
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

    }
}
