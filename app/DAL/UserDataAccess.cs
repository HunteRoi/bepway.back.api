using System.Collections.Generic;
using Model;

namespace DAL
{
    public class UserDataAccess
    {
        private readonly BepwayContext _context;

        public UserDataAccess (BepwayContext context)
        {
            _context = context;
        }
        
    }
}