using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
namespace SecureProgrammingProject
{
    internal static class swaggerParser
    {
        public static OpenApiDocument ReadLocalSwaggerFile(string swagger)
        {

            var file = File.Open(swagger, FileMode.Open);
            return  new OpenApiStreamReader().Read(file, out var diagnostic);
        }

    }
}
