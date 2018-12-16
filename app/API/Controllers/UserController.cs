using DAL;

namespace API.Controllers
{
    public class UserController : APIController
    {
        public UserController (BepwayContext context) {
            Context = context;
        }
    }
}