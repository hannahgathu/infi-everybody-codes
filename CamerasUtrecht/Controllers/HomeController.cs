using CamerasUtrecht.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using CsvHelper.Configuration;
using static System.Net.WebRequestMethods;
using System.Reflection.PortableExecutable;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CamerasUtrecht.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //get cameras from csv
            string path = "https://raw.githubusercontent.com/infi-nl/everybody-codes/master/data/cameras-defb.csv";
            List<CameraRecord> cameras = readCsv(path);


            //initialize specific camera lists
            var divByThreeAndFive = new List<CameraRecord>(); 
            var divByThree = new List<CameraRecord>();
            var divByFive = new List<CameraRecord>();
            var divByNone = new List<CameraRecord>();

            //assign each camera to correct list
            foreach (var cam in cameras)
            {
                if (cam.Number % 3 == 0 && cam.Number % 5 == 0)
                {
                    divByThreeAndFive.Add(cam);
                }
                else if (cam.Number % 3 == 0)
                {
                    divByThree.Add(cam);
                }
                else if (cam.Number % 5 == 0)
                {
                    divByFive.Add(cam);
                }
                else
                {
                    divByNone.Add(cam);
                }
            }

            //populate viewmodel
            var viewModel = new CameraViewModel();
            viewModel.DivByThreeAndFive = divByThreeAndFive;
            viewModel.DivByFive = divByFive;
            viewModel.DivByNone = divByNone;
            viewModel.DivByThree = divByThree;

            return View(viewModel);
        }

        public List<CameraRecord> readCsv(string path)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //get csv from url
                httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                var requestUri = path;
                var stream = httpClient.GetStreamAsync(requestUri).Result;
                var sr = new StreamReader(stream);

                List<string> csv = new List<string>();

                while (!sr.EndOfStream)
                {
                    csv.Add(sr.ReadLine());
                }

                //loop through csv rows to get camera records
                List<CameraRecord> records = new List<CameraRecord>();
                for (int i = 1; i < csv.Count; i++)
                {
                    string[] fields = csv[i].Split(';');

                    //skip rows with not enough fields
                    if (fields.Length < 3)
                    {
                        continue;
                    }

                    else
                    {
                        CameraRecord record = new CameraRecord
                        {
                            Number = int.Parse(fields[0].Split(" ")[0].Split("-")[2]),
                            Name = fields[0],
                            Latitude = fields[1],
                            Longitude = fields[2]

                        };
                        records.Add(record);
                    }
                }
                return records;
            }
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