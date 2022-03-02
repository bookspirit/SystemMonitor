using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
    public class LogUtil
    {
        /*
         * 将信息写入默认路径的.xml文件中
         * @params dc:想要存储的字典类型数据
         * @params path:存储文件路径
         */
        public static void WriteLog(Dictionary<String,String> dc , String path)
        {
            string stmp = path;
            if(path == null)
                stmp = System.AppDomain.CurrentDomain.BaseDirectory;
            //获取当前时间
            String nowTimeDayStr = DateTime.Now.ToLongDateString();
            String nowTimeStr = DateTime.Now.ToString();
            //文件路径
            String xmlFileName = stmp + nowTimeDayStr + ".xml";
            //文件不存在则创建
            if (!File.Exists(@xmlFileName)) 
            {
                CreateXmlFile(xmlFileName);
            }
            //加载xml文件
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            //选择根节点    
            XmlNode root = xmlDoc.SelectSingleNode("List");
            //添加信息节点
            XmlNode noded = xmlDoc.CreateNode(XmlNodeType.Element, "data", null);
            foreach (KeyValuePair<string, string> kvp in dc)
            {
                CreateNode(xmlDoc, noded, kvp.Key, kvp.Value);
            }
            XmlAttribute time = xmlDoc.CreateAttribute("time");
            time.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            noded.Attributes.Append(time);
            root.AppendChild(noded);
            try
            {
                xmlDoc.Save(xmlFileName);
            }
            catch (Exception e)
            {  
                throw (e);
            }
        }

        /*
         * 读取符合条件的信息
         * @params min : 向前搜索时间(分钟)
         * @path :读取文件的路径
         * @return 字典集合格式的返回结果
         */
        public static List<Dictionary<String, String>> ReadLog(int min , String path)
        {
            string stmp = path;
            if (path == null)
                stmp = System.AppDomain.CurrentDomain.BaseDirectory;
            String nowTimeDayStr = DateTime.Now.ToLongDateString();
            String validTimeStr = DateTime.Now.AddMinutes(-min).ToString("yyyyMMddHHmmss");
            String xmlFileName = stmp + nowTimeDayStr + ".xml";
            if (!File.Exists(@xmlFileName))
            {
                //System.Windows.Forms.MessageBox.Show("invalid file path!");
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            String xmlSelectStr = "//data[@time>" + long.Parse(validTimeStr) + "]";
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes(xmlSelectStr);
            List<Dictionary<String,String>> dcList= new List<Dictionary<string,string>>();
            foreach(XmlNode data in xmlNodeList){
                XmlNodeList dataList = data.ChildNodes;
                Dictionary<String, String> dc = new Dictionary<string, string>();
                dc.Add("time",data.Attributes[0].Value.ToString());
                foreach (XmlNode attr in dataList)
                {
                    dc.Add(attr.Name, attr.InnerText);
                }
                dcList.Add(dc);
            }
            return dcList;
        }

        /*
         * 清楚过期的xml文档
         * @params day : 过期自动清除时间(天)
         * @params path ： 清除文件夹的路径
         * @return 清除文件的数目
         */
        public static int ClearExpiredLog(int day,String path)
        {
            string stmp = path;
            if (path == null)
                stmp = System.AppDomain.CurrentDomain.BaseDirectory;
            DateTime validTime = DateTime.Now.AddDays(-day);
            DirectoryInfo folder = new DirectoryInfo(stmp);
            int n = 0;
            foreach (FileInfo file in folder.GetFiles("*.xml"))
            {
                if (file.CreationTime < validTime)
                { 
                    File.Delete(file.FullName);
                    n++;
                }
            }
            return n;
        }

        /*
         * 创建xml文档
         * @params xmlFileName:文件路径
         */
        private static void CreateXmlFile(String xmlFileName)    
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点    
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点    
            XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "List", null);
            xmlDoc.AppendChild(root);
            try
            {
                xmlDoc.Save(xmlFileName);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }


        /*
         * 创建节点
         * @params xmlDoc : xml文档
         * @params parentNode : 父节点
         * @params name : 属性名
         * @params value : 属性值
         */
        private static void CreateNode(XmlDocument xmlDoc,XmlNode parentNode,string name,string value)    
        {    
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);    
            node.InnerText = value;    
            parentNode.AppendChild(node);    
        }    


    }
