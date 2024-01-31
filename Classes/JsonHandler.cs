using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Classes
{
    internal class JsonHandler<T>
    {
        private List<T> data;
        private string jsonFileName;

        public JsonHandler(string fileName)
        {
            data = new List<T>();
            jsonFileName = fileName;

            if (System.IO.File.Exists(jsonFileName) == false)
                System.IO.File.Create(jsonFileName).Close();

            ReadJsonFile();
        }

        public List<T> Data { get { return data; } }

        public void AddItem(T item)
        {
            data.Add(item);
            SaveChanges();
        }

        public void ReadJsonFile()
        {
            try
            {
                if (data == null)
                    return;

                string json = System.IO.File.ReadAllText(jsonFileName);
                data = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning : {ex.Message}");
            }
        }

        public void SaveChanges()
        {
            string updatedJson = System.Text.Json.JsonSerializer.Serialize<List<T>>(data);
            System.IO.File.WriteAllText(jsonFileName, updatedJson);
        }

        
    }

}
