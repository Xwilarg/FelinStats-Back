﻿using Nancy;
using System.Collections.Generic;
using System.IO;

namespace FelinStats_Back.Endpoint
{
    public class Mttr : NancyModule
    {
        public Mttr() : base("/mttr.json")
        {
            // Repair time for weapons
            Get("/", x =>
            {
                if (!File.Exists("mttr.txt"))
                    return (Response.AsJson(new Response.Error()
                    {
                        Code = 503,
                        Message = "mttr.txt doesn't exist."
                    })
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
                string[] lines = File.ReadAllLines("mttr.txt");
                Dictionary<string, int> mttrDic = new Dictionary<string, int>();
                foreach (string s in lines)
                {
                    string[] datas = s.Split(';');
                    mttrDic.Add(datas[0], int.Parse(datas[1]));
                }
                return (Response.AsJson(new Response.Histogram()
                {
                    Code = 200,
                    Value = mttrDic
                })
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
            });
        }
    }
}
