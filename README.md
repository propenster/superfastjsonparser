# SuperfastJSONparser

**A superfast JSON parser written from scratch in C# for my subscribers on [YouTube](https://youtube.com/c/FaithOlusegun)**

- Lexing for normal people
- Parsing for normal people
- Reflection in C# for normal people
- Deserialization 

## References 
- [Wikipedia Parsing](https://en.wikipedia.org/wiki/Parsing)
- [Writing an interpreter in Go by Thorsten Ball](https://interpreterbook.com/)
- Watch my [Create a JSON parser from scratch in C# video](https://www.youtube.com/watch?v=mAYgIPCc1vs&list=PL0DHMcUfPntZ9yLUJ7vi9H6jGz0dJyGJU)

## Features
- Fast JSON parsing
- Low-cost JSON deserialization to plain old CLR objects

## Examples

```csharp

var jsonString = "{\"name\": \"Jason Bourne\", \"isActive\":true, \"nestedObject\": {\"id\":1, \"itemNumber\":\"123AB\"}, \"items\": [1,2,3,4,5], \"nestedObjects\":[{\"id\":1, \"itemNumber\":\"123AB\"}, {\"id\":2, \"itemNumber\":\"149AB\"}], \"age\":43}";

var jsonObject = new OurJSONObject(jsonString);

var sampleObject = jsonObject.GetJSONObject<SampleJSONObject>(string.Empty);

```




### Our sample object type
```csharp

 public class SampleJSONObject
    {
        public string name {  get; set; } = string.Empty; 
        public bool isActive { get; set; }
        public NestedObject nestedObject { get; set; } = new();
        public List<int> items { get; set; } = new List<int>();
        public List<NestedObject> nestedObjects { get; set; } = new List<NestedObject>();
        public int age { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}, isActive: {1}, nestedObject: {2}, items: [ {3} ] age: {4}", name, isActive, nestedObject,items, age);
        }
    }
    public class NestedObject
    {
        public int id { get; set; }
        public string itemNumber { get; set; } = string.Empty;

        public override string ToString()
        {
            return string.Format("{{ id: {0}, itemNumber: {1} }}", id, itemNumber);
        }
    }

```
