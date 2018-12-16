using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class ConnectionHelper
    {
        private readonly string _configFileName = "secrets.json";
        private readonly IConfiguration _config;
        private readonly string CONNECTION_STRING_KEY;
        public ConnectionHelper(string key)
        {
            CONNECTION_STRING_KEY = key;

            string expectedConfigurationFilePath = Path.Combine(Directory.GetCurrentDirectory(), _configFileName);
            if (!File.Exists(expectedConfigurationFilePath))
            {
                throw new FileNotFoundException($"Le fichier de configuration secrète n'a pas pu être trouvé. Il doit s'agir d'un fichier json nommé {_configFileName} contenant les informations sensibles.", expectedConfigurationFilePath);
            }
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(_configFileName)
                .Build();
        }

        public string GetConnectionString ()
        {
            string connectionString = _config.GetConnectionString(CONNECTION_STRING_KEY);
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException($"Le fichier de configuration ne contient pas de clé nommée {CONNECTION_STRING_KEY}.");
            }
            return connectionString;
        }
    }
}
