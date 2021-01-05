using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestService.Models;

namespace RestService.Controllers
{
    public class UserController : ApiController
    {
        private static List<User> users { get; set; } = new List<User>()
                    {
                        new User(1,"admin","password"),
                    };

        // GET api/<controller>
        [Route("api/user")]
        public HttpResponseMessage Get()
        {
            if (Request.Headers.Contains("Authorization"))
            {
                var token = Request.Headers.GetValues("Authorization").FirstOrDefault()?.Split(' ').Last();
                if (JWTAuth.ValidateJwtToken(token))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, users);
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid token");
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized, "No authorization header");
        }

        // POST api/<controller>
        [Route("api/user")]
        public HttpResponseMessage Post([FromBody] AuthenticateRequest authenticateRequest)
        {
            var response = Authenticate(authenticateRequest);

            if (response == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Username or password is incorrect");

            return Request.CreateResponse(HttpStatusCode.Created, response);
        }

        private AuthenticateResponse Authenticate(AuthenticateRequest authenticateRequest)
        {
            var user = users.SingleOrDefault(x => x.Username == authenticateRequest.Username && x.Password == authenticateRequest.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = JWTAuth.generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }
    }
}