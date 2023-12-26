// See https://aka.ms/new-console-template for more information
using SuperFastJSONParser;

Console.WriteLine("Hello, World!");

var jsonString = "{\"name\": \"Jason Bourne\", \"isActive\":true, \"nestedObject\": {\"id\":1, \"itemNumber\":\"123AB\"}, \"items\": [1,2,3,4,5], \"nestedObjects\":[{\"id\":1, \"itemNumber\":\"123AB\"}, {\"id\":2, \"itemNumber\":\"149AB\"}], \"age\":43}";

var jsonObject = new OurJSONObject(jsonString);

var sampleObject = jsonObject.GetJSONObject<SampleJSONObject>(string.Empty);
Console.WriteLine("JSON Parsed into C# Object => {0}", sampleObject);


Console.ReadKey();

