using Microsoft.Extensions.Configuration;

namespace TodoList.Shared
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _configuration;

        public static void CarregarConfiguracoes(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Application Configuration
        public static string PrivateKey => _configuration["PrivateKey"];
        #endregion
    }
}
