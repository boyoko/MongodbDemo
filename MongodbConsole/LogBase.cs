using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MongodbConsole
{
    /// <summary>
    ///     日志基类
    /// </summary>
    [BsonIgnoreExtraElements]
    public class LogBase<T>
    {
        public LogBase()
        {
            CallTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            SerialNo = Guid.NewGuid().ToString("N");
            ClientType = "1";
            Message = default(T);
            var myEntry =Dns.GetHostEntryAsync(Dns.GetHostName()).GetAwaiter().GetResult();
            var address = myEntry.AddressList.FirstOrDefault(e => e.AddressFamily.ToString().Equals("InterNetwork"));
            if (address == null) return;
            var ip = address.ToString();
            HostIp = ip;
        }

        /// <summary>
        ///     调用时间，格式：yyyyMMddHH24mmss
        /// </summary>
        public string CallTime { get; private set; }

        /// <summary>
        ///     消息序列号 UUID
        /// </summary>
        public string SerialNo { get; private set; }

        /// <summary>
        ///     客户端IP地址
        /// </summary>
        public string HostIp { get; private set; }

        /// <summary>
        ///     客户端类型:1:pc 2:手机
        /// </summary>
        public string ClientType { get; private set; }

        /// <summary>
        ///     业务信息
        /// </summary>
        public T Message { get; set; }



        //IList<WriteModel<LogBase<InsuranceLog>>> list = new List<WriteModel<LogBase<InsuranceLog>>>();
        //var sw = new Stopwatch();
        //sw.Start();
        //for (var i = 0; i < 10000000; i++)
        //{
        //    var log = new LogBase<InsuranceLog>
        //    {
        //        Message = new InsuranceLog
        //        {
        //            BusinessKey = i.ToString(),
        //            BusinessName = "我就试试"+ i,
        //            BusinessParameters = ""
        //        }
        //    };

        //    var model = new InsertOneModel<LogBase<InsuranceLog>>(log);

        //    list.Add(model);
        //}

    }
}
