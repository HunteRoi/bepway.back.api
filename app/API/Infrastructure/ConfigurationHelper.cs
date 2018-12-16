using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace API.Infrastructure
{
    public class ConfigurationHelper
    {
        private readonly string _configFileName = "secrets.json";
        private readonly IConfiguration _config;
        public ConfigurationHelper()
        {
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

        public string Get (string key)
        {
            string value = _config.GetValue<string>(key);
            if (String.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"Le fichier de configuration ne contient pas de clé nommée {key}.");
            }
            return value;
        }
    }
}
