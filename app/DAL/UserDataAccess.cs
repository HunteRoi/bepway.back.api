using Model;

namespace DAL
{
    public class UserDataAccess : DataAccess<User>
    {
        public UserDataAccess (BepwayContext context)
        {
            Context = context;
        }
    }
}