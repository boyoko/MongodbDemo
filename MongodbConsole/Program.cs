using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MongodbConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.WriteLine(System.Text.Encoding.GetEncoding("GB2312"));
            try
            {
                BulkWrite(10);
                var m = Find<MessageA,int>("CodeType", 58);
            }
            catch (Exception)
            {
                throw;
            }
            Console.ReadKey();
        }


        private static TDocument Find<TDocument, TValue>(string field,TValue o)
        {
            //var client = new MongoClient("mongodb://192.168.203.128:27017");
            //var database = client.GetDatabase("test");
            //var collection = database.GetCollection<MessageA>("InsuranceLog");
            //var filter = Builders<MessageA>.Filter.Eq("CodeType", 71);
            //var document = collection.Find(filter).First();
            //Console.WriteLine(document.CreateTime);
            var client = new MongoClient("mongodb://192.168.203.128:27017");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<TDocument>("InsuranceLog");
            var filter = Builders<TDocument>.Filter.Eq(field, o);
            var document = collection.Find(filter).FirstOrDefault();
            return document;
        }

        private static void BulkWrite(long count)
        {
            var client = new MongoClient("mongodb://192.168.203.128:27017");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<MessageA>("InsuranceLog");

            IList<WriteModel<MessageA>> list = new List<WriteModel<MessageA>>();
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < count; i++)
            {
                MessageA mesA = new MessageA
                {
                    Sid = Guid.NewGuid().ToString(),
                    Mid = Guid.NewGuid().ToString(),
                    CodeType = i,
                    CreateTime = DateTime.Now,
                    OrgID = Guid.NewGuid(),
                    TraceableCode = Guid.NewGuid().ToString(),
                    OrderNo = Guid.NewGuid().ToString("N")
                };

                var model = new InsertOneModel<MessageA>(mesA);
                list.Add(model);
            }

            sw.Stop();
            Console.WriteLine("循环构造{1}数据耗时{0}毫秒", sw.ElapsedMilliseconds,count);
            sw.Restart();
            collection.BulkWrite(list, new BulkWriteOptions { IsOrdered = false });
            sw.Stop();
            Console.WriteLine("BulkWrite{1}耗时{0}毫秒", sw.ElapsedMilliseconds, count);
        }



    }



    
}