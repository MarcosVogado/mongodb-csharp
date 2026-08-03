using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CursoMongoDB.Programs
{
    public static class Program_4_4b
    {
        public static async Task ExecutarAsync()
        {
            Console.WriteLine("Processo iniciado (Assíncrono)");
            //System.Threading.Thread.Sleep(15000);
            await Task.Delay(15000);
            Console.WriteLine("Processo Assíncrono finalizado");
            
        }
    }
}