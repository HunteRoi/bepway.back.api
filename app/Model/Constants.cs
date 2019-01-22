using System;

namespace Model {
    public static class Constants {
        public static class AuthorizationRoles {
            public const string ALL = "Admin, Gestionnary, Premium, Guest";
            public const string ADMIN_AND_GESTIONNARY = "Admin, Gestionnary";
        }
        
        public static class Roles {
            public const string ADMIN = "Admin";
            public const string GESTIONNARY = "Gestionnary";
            public const string PREMIUM = "Premium";
            public const string GUEST = "Guest";
        }

        public static class Status {
            public const string DRAFT = "Draft";
            public const string EXISTING = "Existing";
            public const string EXPIRED = "Expired";

        }

        public static class Page {
            public const int Index = 0;
            public const int Size = 15;
        }
    }
}