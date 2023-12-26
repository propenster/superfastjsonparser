using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFastJSONParser
{
    public interface IOurJSONObject
    {
        T GetJSONObject<T>(string key) where T : class, new();
    }
    public class OurJSONObject : IOurJSONObject
    {
        private Dictionary<object, object> _BackingDict = new Dictionary<object, object>();
        public OurJSONObject(string jsonString)
        {
            JsonString = jsonString ?? throw new ArgumentNullException(nameof(jsonString));
            var _lexer = new Lexer(jsonString);
            var _parser = new Parser(_lexer);
            _BackingDict = _parser.Parse() as Dictionary<object, object> ?? new Dictionary<object, object>();
            if (!_BackingDict.Any()) throw new Exception("Could not parse json string");
        }

        public string JsonString { get; }

        public T GetJSONObject<T>(string key) where T : class, new()
        {
            Dictionary<object, object> value = string.IsNullOrWhiteSpace(key) ? _BackingDict : (Dictionary<object, object>)_BackingDict[key];
            T result = new T();
            ConvertDictionaryToObject<T>(result, value);

            return result;

        }
        private static void ConvertDictionaryToObject<T>(T? result, Dictionary<object, object> dictionary) where T : class, new()
        {
            if (!dictionary.Any()) return;


            foreach(var kvp in dictionary)
            {
                //get prop
                var property = result?.GetType().GetProperty(kvp.Key.ToString() ?? string.Empty);
                if(property != null && property.CanWrite)
                {
                    if(property.PropertyType.IsClass && property.PropertyType != typeof(string)) //nested object
                    {
                        if(property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            var genericArgumentType = property.PropertyType.GetGenericArguments().FirstOrDefault();
                            var listType = typeof(List<>).MakeGenericType(genericArgumentType); 
                            var nestedList = Activator.CreateInstance(listType) as IList;
                            var listData = kvp.Value as List<object>;
                            if(listData != null)
                            {
                                foreach(var item in listData)
                                {
                                    if (genericArgumentType.IsClass) //NestedObject is class else... List<int> 
                                    {
                                        var nestedObject = Activator.CreateInstance(genericArgumentType);
                                        ConvertDictionaryToObject(nestedObject, item as Dictionary<object, object>);
                                        nestedList.Add(nestedObject);
                                    }
                                    else
                                    {
                                        // int, string, anything else
                                        var convertedListItem = Convert.ChangeType(item, genericArgumentType);
                                        nestedList.Add(convertedListItem);
                                    }
                                }
                            }
                            property.SetValue(result, nestedList);
                        }
                        else
                        {
                            var nestedObject = Activator.CreateInstance(property.PropertyType);
                            var nestedData = kvp.Value as Dictionary<object, object>;
                            if(nestedData != null)
                            {
                                ConvertDictionaryToObject(nestedObject, nestedData);
                            }
                            property.SetValue(result, nestedObject);
                        }
                    }
                    else
                    {
                        var convertedValue = Convert.ChangeType(kvp.Value, property.PropertyType);
                        property.SetValue(result, convertedValue);
                    }
                    //check is property a list? 

                    //is property a class - a.k.a a nestedObject
                }
            }

        }
    }
}
