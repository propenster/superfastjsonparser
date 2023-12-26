using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFastJSONParser
{
    //var jsonString = "{\"name\": \"Jason Bourne\", \"isActive\":true, \"nestedObject\": {\"id\":1, \"itemNumber\":\"123AB\"}, \"age\":43}";

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
}
