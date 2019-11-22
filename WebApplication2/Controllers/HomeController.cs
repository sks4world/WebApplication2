using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        


        public string BASE_URL = "https://api.iextrading.com/1.0/";
        public string BASE_URL1 = "https://api.fda.gov/food/enforcement.json";
        HttpClient httpClient;

        public HomeController()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<Company> GetSymbols()
        {
            // String Foodrecall_API = BASE_URL + "?search=report_date:[20040101+TO+20131231]&limit=1";
            string Foodrecall_API = BASE_URL + "ref-data/symbols";
            string companyList = "";
            List<Company> companies = null;

            httpClient.BaseAddress = new Uri(Foodrecall_API);
            HttpResponseMessage response = httpClient.GetAsync(Foodrecall_API).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                companyList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            if (!companyList.Equals(""))
            {
                companies = JsonConvert.DeserializeObject<List<Company>>(companyList);
                companies = companies.GetRange(0, 50);

            }
            return companies;
        }

        public List<Recall> GetRecalls()
        {
            // String Foodrecall_API = BASE_URL + "?search=report_date:[20040101+TO+20131231]&limit=1";
            string Foodrecall_API1 = BASE_URL1 + "?search=report_date:[20040101+TO+20131231]&limit=50";
            string recallList = "";
            List<Recall> recalls = null;

            httpClient.BaseAddress = new Uri(Foodrecall_API1);
            HttpResponseMessage response1 = httpClient.GetAsync(Foodrecall_API1).GetAwaiter().GetResult();

            if (response1.IsSuccessStatusCode)
            {
                recallList = response1.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            if (!recallList.Equals(""))
            {
                string test;

                recallList=


                test = JsonConvert.ToString(recallList);
                var parsedObject = JObject.Parse(test);
                var resultsJson = parsedObject["results"].ToString();
                var resultsObj = JsonConvert.DeserializeObject<List<Recall>>(resultsJson);
                recalls = resultsObj.GetRange(0, 50);

                //recalls = JsonConvert.DeserializeObject<List<Recall>>(recallList);
                //recalls = recalls.GetRange(0, 50);
                

            }
            return recalls;
        }

        public IActionResult Index()
        {
            List<Company> companies = GetSymbols();
            return View(companies);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            List<Recall> recalls = GetRecalls();
            return View(recalls);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
