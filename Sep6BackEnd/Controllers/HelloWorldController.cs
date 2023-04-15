using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sep6BackEnd.BusinessLogic;

namespace Sep6BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        private HandlerFactory _handlerFactory;

        public HelloWorldController()
        {
            _handlerFactory = new HandlerFactory();
        }

        [HttpGet]
        [Route("getHelloWorld")]
        public HelloWorldJSONClass GetHelloWorld()
        {
            string test = _handlerFactory._helloWorldlogic.GetHelloWorld();

            HelloWorldJSONClass returnObject = new HelloWorldJSONClass();
            returnObject.test = test;

            return returnObject;
        }
        
        [HttpGet]
        [Route("getHelloWorldWithName/{name}")]
        public HelloWorldJSONClass GetHelloWorldWithName( [FromRoute] string name)
        {
            string test = _handlerFactory._helloWorldlogic.GetHelloWorldWithName(name);
            
            HelloWorldJSONClass returnObject = new HelloWorldJSONClass();
            returnObject.test = test;

            return returnObject;
        }
        
        
    }
}