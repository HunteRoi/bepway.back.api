using DAL;

namespace API.Controllers
{
    public class CompanyController : APIController
    {
        public CompanyController (BepwayContext context) 
        {
            Context = context;
        }   
    }
}