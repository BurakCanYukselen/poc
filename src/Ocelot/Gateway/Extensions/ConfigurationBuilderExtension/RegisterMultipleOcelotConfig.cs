using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Ocelot.DependencyInjection;

namespace Gateway.Extensions.ConfigurationBuilderExtension
{
    public static partial class ConfigurationBuilderExtension
    {
        private const string OCELOT_FILE_NAME = "ocelot.merged.json";

        public static IConfigurationBuilder RegisterMultipleOcelotConfig(this IConfigurationBuilder builder, string relativePath,
            IWebHostEnvironment env)
        {
            var routes = MergeConfigs(relativePath);
            SaveMergedConfigurationFile(routes, relativePath);
            builder.AddOcelot(relativePath, env);
            return builder;
        }

        public static IEnumerable<dynamic> MergeConfigs(string relativePath)
        {
            var routes = new List<dynamic>();

            var files = GetFilesInLocation(relativePath);
            foreach (var file in files)
                if (Regex.IsMatch(file, "ocelot.([a-zA-Z0-9]*).json"))
                {
                    var route = GetJsonContent(file);
                    routes.Add(route);
                }

            var directories = GetDirectoriesInLocation(relativePath);
            foreach (var directory in directories)
            {
                var nextRelativePath = GetRelativePath(directory, relativePath);
                routes.AddRange(MergeConfigs(nextRelativePath));
            }

            return routes;
        }

        public static void SaveMergedConfigurationFile(IEnumerable<dynamic> routes, string relativePath)
        {
            var path = GetTargetPath(relativePath);
            var jsonObject = new {Routes = routes};
            File.WriteAllText(@$"{path}\{OCELOT_FILE_NAME}", Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject));
        }

        public static string GetRelativePath(string path, string relativePath)
        {
            var directory = new DirectoryInfo(path);
            var subRelativePath = Path.Combine(relativePath, directory.Name);
            return subRelativePath;
        }

        public static string[] GetDirectoriesInLocation(string relativePath)
        {
            var targetPath = GetTargetPath(relativePath);
            var directories = Directory.GetDirectories(targetPath);
            return directories;
        }

        public static string[] GetFilesInLocation(string relativePath)
        {
            var targetPath = GetTargetPath(relativePath);
            var files = Directory.GetFiles(targetPath);
            return files;
        }

        public static string GetTargetPath(string relativePath)
        {
            var rootPath = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            var targetPath = Path.Combine(rootPath.Root, relativePath);
            return targetPath;
        }

        public static dynamic GetJsonContent(string path)
        {
            var fileContent = File.ReadAllText(path);
            var dynamicObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(fileContent);
            var route = dynamicObject.Routes.First;
            return route;
        }
    }
}