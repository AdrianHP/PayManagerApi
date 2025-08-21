using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayManager.Presentation.Infrastructure;

namespace PayManager.Presentation.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMapper Mapper;

        public BaseController()
        {
            Mapper = AutoMapping.Mapper;
        }
    }
}
 
