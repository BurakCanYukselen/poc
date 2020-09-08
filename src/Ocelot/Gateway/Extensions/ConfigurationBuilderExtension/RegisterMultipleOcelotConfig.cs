using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Gateway.Extensions.ConfigurationBuilderExtension
{
    public static partial class ConfigurationBuilderExtension
    {
        public static IConfigurationBuilder RegisterMultipleOcelotConfig(this IConfigurationBuilder builder, string filePath)
        {
            var path = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            var configFilesDirectory = Path.Combine(path.Root, filePath);
            var files = Directory.GetFiles(configFilesDirectory);

            foreach (var file in files)
                if (Regex.IsMatch(file, "ocelot.([a-zA-Z0-9]*).json"))
                {
                    var provider = new PhysicalFileProvider(configFilesDirectory);
                    var fileName = file.Replace(provider.Root, string.Empty);
                    builder.AddJsonFile(provider, fileName, optional: false, reloadOnChange: true);
                }
            
            var directories = Directory.GetDirectories(configFilesDirectory);
            foreach (var directory in directories)
            {
                var relativePath = directory.Replace(path.Root, string.Empty);
                builder.RegisterMultipleOcelotConfig(relativePath);
            }

            return builder;
        }
    }
}