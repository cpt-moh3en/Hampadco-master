﻿using System.Net.Http.Headers;
using System.Net;
using System.Threading.Tasks.Sources;
using System.Net.WebSockets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using hampadco.Models;
using Microsoft.AspNetCore.Hosting;
using DataLayer.Entities;
using DataLayer.Context;
using ViewModelLayer.Entities;
using System.Net.Http;
using RestSharp;

namespace hampadco.Controllers
{
    public class HomeController : Controller
    {

        private readonly HampadcoContext db;

        private readonly IWebHostEnvironment _env;

        public HomeController(HampadcoContext _db, IWebHostEnvironment env)
        {
            db = _db;
            _env = env;
        }

        private readonly ILogger<HomeController> _logger;

        public IActionResult token(Vm_User vs)
        {
            var client = new RestClient("https://ref.sayancard.ir/ref-payment/RestServices/mts/generateTokenWithNoSign/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "cookiesession1=678A8C67QRSTUVWXYZABDEFGHIJK0E49");
            var body = @"{
                    " + "\n" +
                                @"    ""WSContext"":{""UserId"":""41837658"",""Password"":""313178""},
                    " + "\n" +
                                @"    ""TransType"":""EN_GOODS"",
                    " + "\n" +
                                @"    ""ReserveNum"":""123452"",
                    " + "\n" +
                                @"    ""MerchantId"":""41837658"",
                    " + "\n" +
                                @"    ""Amount"":"+ vs.Amount +" ," + "\n" +
                                @"    ""RedirectUrl"":""https://localhost:5001/Home/Verification""
                    " + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            // Console.WriteLine(response.Content);
            var token = response.Content.Split(",");
            var token1 = token[2].Split(":");
            var token2 = token1[1].Split(",");
            string token3 = token2[0].Replace("/", "").Replace("\"", "");
            
            return RedirectToAction("p2" , new {token3});
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Verification(var RefNum)
        {
            return View();
        }

        public IActionResult pardakht()
        {
            return View();
        }

        public IActionResult p2(string token3)
        {
            ViewBag.x=token3;
            return View();
        }

        public IActionResult p3()
        {
            return View();
        }

    }
}
