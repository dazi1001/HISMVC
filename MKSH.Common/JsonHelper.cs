using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace MKSH.Common
{
    public static class JsonHelper
    {
        /// <summary>
        /// 将对象序列化为JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeJson(this Object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }

        /// <summary>
        /// 装DataTable数据转为JSON数据的扩展方法
        /// </summary>
        /// <param name="dt">DataTable数据</param>
        /// <returns></returns>
        public static string ToJson(this DataTable dt)
        {
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented,
              new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            return json;
        }


        public static string ToJson(this Object obj)
        {
            var json = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented,
              new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            return json;
        }
        /// <summary>
        /// 不需要分页
        /// </summary>
        /// <param name="dt"></param> 
        /// <returns></returns>
        public static string SerializeJson(this DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            if (dt == null)
            {
                return string.Empty;
            }
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();

                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }

                list.Add(result);
            }

            return serializer.Serialize(list); ;
        }

        /// <summary>
        /// 不需要分页
        /// </summary>
        /// <param name="dt"></param> 
        /// <returns></returns>
        public static string SerializeJson(this DataRow[] drc)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in drc)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dr.Table.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }

                list.Add(result);
            }

            return serializer.Serialize(list); ;
        }

        /// <summary>
        /// 将JSON字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeJson<T>(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }

        /// <summary>
        /// 将JSON字符串反序列化为对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static List<T> JsonStringToList<T>(this string jsonStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            List<T> objs = Serializer.Deserialize<List<T>>(jsonStr);
            return objs;
        }


        /// <summary>
        /// 将对象序列化为XML文档
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeXml(this Object obj)
        {
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            TextWriter tw = new StringWriter();
            xs.Serialize(tw, obj);
            return tw.ToString();
        }
    }
}
