<<<<<<< HEAD
﻿using Microsoft.OpenApi.Models;
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
=======
﻿using Microsoft.OpenApi.Models;
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
>>>>>>> 534183718ef6d07849b46bbcf4c532876b779d53
