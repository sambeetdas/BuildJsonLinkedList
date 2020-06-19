using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BuildJson
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonBuilder();
            Console.ReadKey();
        }

        static void JsonBuilder()
        {
            JsonStructure obj = new JsonStructure("0", GetField());
            for (int i = 0; i < 5; i++)
            {
                obj.InsertChilden((i+1).ToString(), GetField(), obj);
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            Console.WriteLine(json);
                      
        }
        static List<Dictionary<string, string>> GetField()
        {
            List<Dictionary<string, string>> listDict = new List<Dictionary<string, string>>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            Random random = new Random();
            dict.Add("name", "SubId");
            dict.Add("value", random.NextDouble().ToString());
            listDict.Add(dict);

            return listDict;
        }
    }

    public class JsonStructure
    {
        public string MaterialId { get; set; }
        public List<Dictionary<string,string>> Field { get; set; }
        public JsonStructure Children { get; set; }

        public JsonStructure(string MaterialId, List<Dictionary<string, string>> Field)
        {
            this.MaterialId = MaterialId;
            this.Field = Field;
        }

        private JsonStructure head = null;
        public void InsertChilden(string MaterialId, List<Dictionary<string, string>> Field, JsonStructure obj)
        {
            JsonStructure child = new JsonStructure(MaterialId, Field);

            if (obj.Children == null)
            {
                obj.Children = child;
                return;
            }
            JsonStructure lastNode = GetLastNode(obj);
            lastNode.Children = child;

            return;

        }

        private JsonStructure GetLastNode(JsonStructure linklist)
        {
            JsonStructure temp = linklist.Children;
            while (temp.Children != null)
            {
                temp = temp.Children;
            }
            return temp;
        }


    }
}
