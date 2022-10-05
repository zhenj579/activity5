using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

class University
{
    [JsonProperty("country")]
    public string Country { get; set; }
    
    [JsonProperty("web_pages")]
    public List<string> Web_Pages { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("state-province")]
    public string state_province {get; set;}

    [JsonProperty("alpha_two_code")]
    public string alpha_two_code { get; set; }

    [JsonProperty("domains")]
    public List<string> domains { get; set; }
}

class Program
{
    private static readonly HttpClient client = new HttpClient();
    static async Task Main(string[] args)
    {
        await ProcessRepositories();
    }

    private static async Task ProcessRepositories()
    {
        while(true)
        {
            Console.WriteLine("Enter university name. Press Enter without writing a name to quit the program.");
            var uni_name = Console.ReadLine();

            if(string.IsNullOrEmpty(uni_name))
            {
                break;
            }

            var result = await client.GetAsync("http://universities.hipolabs.com/search?name=" + uni_name);
            var resultRead = await result.Content.ReadAsStringAsync();
             
            var universities = JsonConvert.DeserializeObject<University[]>(resultRead);

            foreach (var university in universities)
            {
                Console.WriteLine("---");
                Console.WriteLine("Country: " + university.Country);
                Console.WriteLine("Name: " + university.Name);
                Console.WriteLine("Web Pages: " + string.Join(",", university.Web_Pages));
                Console.WriteLine("Domains: " + string.Join(",", university.domains));
                Console.WriteLine("State Province: " + university.state_province);
                Console.WriteLine("Alpha Two Code: " + university.alpha_two_code);
                Console.WriteLine("\n---");
            }
        }
    }
}


