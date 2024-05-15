using Microsoft.OpenApi.Models;
using System.Text.Json;
using Microsoft.OpenApi.Any;
using System.Security.Cryptography;
namespace SecureProgrammingProject.Tests

{
    internal class SwaggerTest : BaseTester
    {
        protected override string testName => "Swagger Test";
        private OpenApiDocument SwaggerDoc;
        private HttpClient client;
        public SwaggerTest(String filename)
        {
            OpenApiDocument _SwaggerDoc = swaggerParser.ReadLocalSwaggerFile(filename);
            _SwaggerDoc.ResolveReferences();
            this.SwaggerDoc = _SwaggerDoc;
            this.client = new HttpClient();
        }


        public override void RunTest(string address, StringWriter resultsStream)
        {
            PositiveCases(address);
            RandomCases(address);
            FinalReport();
        }

        private void PositiveCases(string address)
        {
            foreach (var Path in SwaggerDoc.Paths)
            {
                String URL = Path.Key;
                List<Task> tasks = new List<Task>();
                foreach (var Operation in Path.Value.Operations)
                {
                    String method = Operation.Key.ToString();
                    tasks.Add(SendRequest(this, client, address + URL, method, Operation.Value.Parameters, Operation.Value.RequestBody, Operation.Value.Responses));
                }
                Task.WaitAll(tasks.ToArray());
            }
        }

        private void RandomCases(string address)
        {
            foreach (var Path in SwaggerDoc.Paths)
            {
                String URL = Path.Key;
                List<Task> tasks = new List<Task>();
                foreach (var Operation in Path.Value.Operations)
                {
                    String method = Operation.Key.ToString();
                    tasks.Add(RandomCases(this, client, address + URL, method, Operation.Value.Parameters, Operation.Value.RequestBody, Operation.Value.Responses));
                }
                Task.WaitAll(tasks.ToArray());
            }
        }

        private static async Task SendRequest(SwaggerTest tester, HttpClient client, string URL, string method, IList<OpenApiParameter> httpParams, OpenApiRequestBody body, OpenApiResponses expected)
        {
#pragma warning disable CS8629
            IList<OpenApiParameter> pathParams = httpParams.Where(p => string.Equals(p.In.Value.ToString(), "Path", StringComparison.OrdinalIgnoreCase)).ToList();
            IList<OpenApiParameter> QueryParams = httpParams.Where(p => p.In.Value.Equals("query")).ToList();
            URL = AddPathParams(URL, pathParams, QueryParams, false);
#pragma warning restore CS8629
            using var request = new HttpRequestMessage(new HttpMethod(method.ToUpper()), URL);


            foreach (var param in httpParams)
            {
#pragma warning disable CS8629
                if (param.In.Value.Equals("header"))
#pragma warning restore CS8629  
                {
                    request.Headers.Add(param.Name, param.Schema.Default.ToString());
                }
                if (param.In.Value.Equals("cookies"))
                {
                    Console.WriteLine("Cookie params not supported");
                    return;
                }
            }

            string bodyCopy = "";
            if (body != null)
            {
                bodyCopy = AddBodyContent(request, body, client, true);
            }
            using var response = await client.SendAsync(request);
            int rescode = (int)response.StatusCode;
            if (!expected.ContainsKey(rescode.ToString()))
            {
                tester.testRan(tester.failed);
                if (request.Content == null)
                {
                    tester.FailureReport("Expected status code to be: " + expected.Keys.AsEnumerable().Aggregate(((a, e) => (a + ", " + e))) + " Got: " + rescode.ToString() + " " + URL);
                }
                else
                {
                    tester.FailureReport("Expected status code to be: " + expected.Keys.AsEnumerable().Aggregate(((a, e) => (a + ", " + e))) + " Got: " + rescode.ToString() + " Request was " + URL);
                }
            }
            else
            {
                tester.testRan(tester.passed);
            }
            //TODO ADD CHECKS FOR BODY AND HEADERS? MAYBe impossible to do without examples
        }

        static private async Task RandomCases(SwaggerTest tester, HttpClient client, string URL, string method, IList<OpenApiParameter> httpParams, OpenApiRequestBody body, OpenApiResponses expected)
        {
#pragma warning disable CS8629
            IList<OpenApiParameter> pathParams = httpParams.Where(p => string.Equals(p.In.Value.ToString(), "Path", StringComparison.OrdinalIgnoreCase)).ToList();
            IList<OpenApiParameter> QueryParams = httpParams.Where(p => p.In.Value.Equals("query")).ToList();
#pragma warning restore CS8629
            URL = AddPathParams(URL, pathParams, QueryParams, true);
            using var request = new HttpRequestMessage(new HttpMethod(method.ToUpper()), URL);

            foreach (var param in httpParams)
            {
#pragma warning disable CS8629
                if (param.In.Value.Equals("header"))
#pragma warning restore CS8629
                {
                    request.Headers.Add(param.Name, param.Schema.Default.ToString());
                }
                if (param.In.Value.Equals("cookies"))
                {
                    Console.WriteLine("Cookie params not supported");
                    return;
                }
            }

            string bodyCopy = "";
            if (body != null)
            {
                 bodyCopy = AddBodyContent(request, body, client, true);
            }
            using var response = await client.SendAsync(request);
            int rescode = (int)response.StatusCode;
            if (!expected.ContainsKey(rescode.ToString()))
            {
                tester.testRan(tester.failed);
                if(request.Content == null)
                {
                    tester.FailureReport("Expected status code to be: " + expected.Keys.AsEnumerable().Aggregate(((a, e) => (a+", "+ e)))  + " Got: " + rescode.ToString()+" "+ URL);
                }
                else
                {
                    tester.FailureReport("Expected status code to be: " + expected.Keys.AsEnumerable().Aggregate(((a, e) => (a + ", " + e))) + " Got: " + rescode.ToString() + " Request was " + URL+ "Body: " + bodyCopy);
                }
            }
            else
            {
                tester.testRan(tester.passed);
            }
        }

        static private string AddPathParams(string URL, IList<OpenApiParameter> pathParams, IList<OpenApiParameter> QueryParams, Boolean randomize)
        {
            if (pathParams.Count() > 0)
            {
                string pattern = @"\{([^}]*)\}";
                //Path examples not supported in standard version 2.0 so we are just going to have to generate some random ones
                URL = System.Text.RegularExpressions.Regex.Replace(URL, pattern, CreateRandomValueFromSchema(pathParams.First().Schema, randomize));
            }
            if (QueryParams.Count() > 0)
            {
                URL = URL + "?";

                foreach (var param in QueryParams)
                {
                    URL = URL + param.Name;
                    if (param != QueryParams.Last())
                    {
                        URL = URL + "&";
                    }
                }
            }
            return URL;
        }

        static private string AddBodyContent(HttpRequestMessage request,OpenApiRequestBody body, HttpClient client, Boolean random)
        {
            String content = "";

            foreach (var param in body.Content)
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(param.Key.ToString()));

                try
                {
                    if (param.Value.Examples.Count() > 0)
                    {
                        foreach (var example in param.Value.Examples)
                        {
                            content += example.Key;
                            //Funny name but is correct

                            content += example.Value.Value;
                        }
                    }
                    else if (param.Value.Example != null)
                    {
                        content += param.Value.Example.ToString();
                    }
                    else if (param.Value.Schema != null && param.Value.Schema.Example != null)
                    {
                        var testing = (OpenApiObject)param.Value.Schema.Example;
                        content = parseToJson(testing.ToDictionary());


                    }
                }
                catch (NullReferenceException ex)
                {

                    Console.WriteLine(ex.ToString());
                    continue;
                }

                var strContent = new StringContent(content);
                request.Content = strContent;
                request.Content.Headers.Clear();
                request.Content.Headers.Add("Content-Type", param.Key);
                
            }
            return content;
        }



        static private object GetValue(IOpenApiAny openApiAny)
        {
            if (openApiAny is OpenApiString str)
            {
                return str.Value;
            }
            else if (openApiAny is OpenApiInteger integer)
            {
                return integer.Value;
            }
            else if (openApiAny is OpenApiBoolean boolean)
            {
                return boolean.Value;
            }
            else if (openApiAny is OpenApiLong long_)
            {

                return long_.Value;

            }
            else if (openApiAny is OpenApiArray arr_)
            {
                return arr_;
            }
            else
            {
                throw new NotSupportedException("Unsupported type of IOpenApiAny");
            }
        }

        static private string parseToJson(IDictionary<string, IOpenApiAny> items)
        {
            Dictionary<string, object> serializedDictionary = new Dictionary<string, object>();

            foreach (KeyValuePair<string, IOpenApiAny> pair in items)
            {

                object val = GetValue(pair.Value);
                
                if (val is IEnumerable<IOpenApiAny> array) //Parse array items
                {
                    List<object> arrayItems = recursiveLists(array);
                    serializedDictionary[pair.Key] = arrayItems;
                }
                else // Handle other types
                {
                    serializedDictionary[pair.Key] = val;
                }
            }

            return JsonSerializer.Serialize(serializedDictionary);
        }
        static private List<object> recursiveLists(IEnumerable<IOpenApiAny> array)
        {
            List<object> arrayItems = new List<object>();

            foreach (var item in array)
            {
                if (item is IDictionary<string, IOpenApiAny> dictItem)
                {
                    IDictionary<string, object> dict = new Dictionary<string, object>();
                    foreach (var pair in dictItem)
                    {
                        
                        var val = GetValue(pair.Value);
                        if (val is IEnumerable<IOpenApiAny> innerArray)
                        {
                            List<object> innerArrayItems = recursiveLists(innerArray);
                            dict[pair.Key] = innerArrayItems;
                        }
                        else
                        {
                            dict[pair.Key] = val;
                        }
                        
                    }
                    arrayItems.Add(dict);
                }
            }
            return arrayItems;
        }
        static private string CreateRandomValueFromSchema(OpenApiSchema schema, Boolean typeMixing)
        {
            int length = RandomNumberGenerator.GetInt32(1, 100);

            if (typeMixing)
            {
                int typeIndex = RandomNumberGenerator.GetInt32(4);
                switch (typeIndex)
                {
                    case 0:
                        schema.Type = "string";
                        break;
                    case 1:
                        schema.Type = "integer";
                        break;
                    case 2:
                        schema.Type = "boolean";
                        break;
                    case 3:
                        schema.Type = "number";
                        break;
                }
            }

            if (schema.Type == "string")
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
            }
            else if (schema.Type == "integer")
            {
                return new string(RandomNumberGenerator.GetInt32(0, int.MaxValue).ToString());
            }
            else if (schema.Type == "boolean")
            {
                return new string((RandomNumberGenerator.GetInt32(2) == 0).ToString());
            }
            else if (schema.Type == "number")
            {
                return new string((
                    GetRandomDouble() * (length)).ToString());
            }
            else
            {
                throw new ArgumentException("Unsupported type");
            }
        }
        private static Double GetRandomDouble()
        {
            var rng = RandomNumberGenerator.Create();
            var bytes = new Byte[8];
            rng.GetBytes(bytes);
            var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
            return  ul / (Double)(1UL << 53);
        }
    }


}

