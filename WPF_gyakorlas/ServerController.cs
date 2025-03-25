using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace WPF_gyakorlas
{
    internal class ServerController
    {

        HttpClient client = new HttpClient();

        public ServerController()
        {

        }


        public async Task<List<string>> getAllNames()
        {
            List<string> list = new List<string>();
            string nameURL = "http://localhost:3000/allName";

            try
            {
                    HttpResponseMessage response = await client.GetAsync(nameURL);
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync(); 
                    list = JsonConvert.DeserializeObject<List<JsonData>>(result).Select(item => item.name).ToList();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message); 
            }

            return list;
        }



        public async Task DeletePersonAsync(string name)
        {
            string url = "http://localhost:3000/deletePerson";

            var data = new { name = name };

            string jsonContent = JsonConvert.SerializeObject(data);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            try
            {
                var response = await client.PostAsync(url, content);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(responseBody);
                }
                else
                {
                    MessageBox.Show("Hiba történt a törlés során. HTTP státuszkód: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message);
            }
        }

        public async Task CreatePerson(string name, int age)
        {
            string registerURL = "http://localhost:3000/createPerson";

            try
            {
                    var personData = new
                    {
                        name = name,
                        age = age
                    };
                    string jsonData = JsonConvert.SerializeObject(personData);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(registerURL, content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(responseBody);
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Hiba történt az emberkísérletek során:" + errorMessage);
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
            }
        }


        public async Task<List<int>> getAllages()
        {
            List<int> list = new List<int>();
            string nameURL = "http://localhost:3000/allAge";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(nameURL);
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<JsonData>>(result).Select(item => item.age).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
            }

            return list;
        }
    }
    class JsonData
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string message { get; set; }
    }
}
