using System;

namespace Model
{
    public class Constants
    {
        public static class Roles
        {
            public const string ADMIN = "Admin";
            public const string GESTIONNARY = "Gestionnary";
            public const string USER = "User";
            public enum Status { Draft, Existing, Expired };
        }
    }
}