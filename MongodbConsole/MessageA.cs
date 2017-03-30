using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongodbConsole
{
    [BsonIgnoreExtraElements]
    public class MessageA
    {
        public string Sid { get; set; }

        public string Mid { get; set; }

        public string OrderNo { get; set; }

        public int CodeType { get; set; }

        public string TraceableCode { get; set; }
        //如果不加下面的属性，会与当前时间相差8小时，这是时区不同造成的
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 分区列，企业id
        /// </summary>
        public Guid OrgID { get; set; }
    }
}
