using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
namespace Tools
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlFlexflow
    {
        /// <summary>
        /// �����ļ�·����.
        /// </summary>
        public static string configFilePath;

        private static XmlDocument xd = new XmlDocument();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NodeName"></param>
        /// <returns></returns>
        public static string ReadXmlNodeValue(string NodeName)
        {
            if (string.IsNullOrEmpty(configFilePath)) {
                MessageBox.Show("����ָ��:   configFilePath","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return "";
            }
            String NodeVale = "";
            try {
                xd.Load(configFilePath);
            }
            catch(Exception ex){
                MessageBox.Show(ex.ToString(), "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return NodeVale;             
            }
            XmlNodeList xnl = xd.SelectSingleNode("flexflow_client.exe").ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;
                XmlNodeList xnlsubs = xe.ChildNodes;
                foreach (XmlNode xnsb in xnlsubs)
                {
                    XmlElement exsub = (XmlElement)xnsb;
                    if (exsub.InnerText == NodeName)
                    {
                        NodeVale = exsub.NextSibling.InnerText.ToString();
                    }
                }
            }
            return NodeVale;
        }
    }
}
