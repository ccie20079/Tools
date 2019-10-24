using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
namespace Tools
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckPattern
    {
        /// <summary>
        /// ����Ƿ�Ϊ���֡�
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool CheckNumber(string s)
        {
            string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(s);
        }
        /// <summary>
        /// ����Ƿ�ΪExcel��Ԫ��ı�ʾ����
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool checkExcelCellIndicate(string s)
        {
            string pattern = "^[A-Z]{1,2}[1-9]{1}[0-9]{0,2}$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(s);
        }
        /// <summary>
        /// ����Ƿ�ΪExcel���б�ʶ
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool checkColumnIndicate(string s)
        {
            string pattern = "^[A-Z]{1,2}$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateStr"></param>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static bool checkDateStr(string dateStr,out DateTime dateValue) {
            string pattern = "^[0-9]{6}$";
            Regex rx = new Regex(pattern);
            if (!rx.IsMatch(dateStr)) {
                dateValue = new DateTime();
                return false;
            }
            dateStr = "20" + dateStr;
            dateStr = dateStr.Substring(0, 4) + "-" + dateStr.Substring(4, 2) + "-" + dateStr.Substring(6, 2);
            if (!DateTime.TryParse(dateStr, out dateValue)) {
                return false;
            }
            return true;
        }
        /// <summary>
        /// �ж��Ƿ�Ϊʱ���ַ�����
        /// </summary>
        /// <param name="timeStr"></param>
        /// <returns></returns>
        public static bool isTimeStr(string timeStr) {
            string pattern = "^[0-9]{2}:[0-9]{2}$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(timeStr);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool CheckFloat(string s)
        {
            string pattern = @"^-?\d+$|^(-?\d+)(\.\d+)?$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        public static void RecordLog(string configFilePath,string str)
        {
            string yy = DateTime.Now.Year.ToString();
            string mm = DateTime.Now.Month.ToString();
            string dd = DateTime.Now.Day.ToString();
            if (mm.Length == 1)
                mm = "0" + mm;
            if (dd.Length == 1)
                dd = "0" + dd;
            string date = yy + mm + dd;
            string logfilename = XmlFlexflow.ReadXmlNodeValue("LogPath") +"\\" + "FOX" + date + ".txt";
            if (File.Exists(logfilename))
            {
                FileStream fsOutput = new FileStream(logfilename, FileMode.Append, FileAccess.Write);
                StreamWriter srOutput = new StreamWriter(fsOutput);
                srOutput.WriteLine("\r\n" + DateTime.Now.ToString() + ": " + str);
                srOutput.Close();
            }
            else
            {
                FileStream fsOutput = new FileStream(logfilename, FileMode.Append, FileAccess.Write);
                StreamWriter srOutput = new StreamWriter(fsOutput);
                srOutput.WriteLine("\r\n" + DateTime.Now.ToString() + ": " + str);
                srOutput.Close();
            }
        }
        /// <summary>
        /// /�������кţ���ȡ���е�һ����
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static String getSubStr(String SN,int startIndex,int length) {
            String result = "";
            result = SN.Substring(startIndex, length);
            return result;
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="str1"></param>
       /// <param name="str2"></param>
       /// <param name="resultStr"></param>
       /// <returns></returns>
        public static bool checkStr(string str1, string str2,string resultStr) {
            //  \s ��ʾ�հ��ַ�����
            string pattern = "^" + str1 + "\\s*?" + str2 + "$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(resultStr.Trim());
        }
        /// <summary>
        /// ���ڼ�鿼�ڼ�¼����A1��Ԫ�����������
        /// </summary>
        /// <param name="checkContent"></param>
        /// <returns></returns>
        public static bool isARXls(string checkContent)
        {
            string pattern = @"^���ڼ�¼��[1-9]{1}$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(checkContent);
        }
        /// <summary>
        /// ���Ƽ��ĵ������Ƿ� ����ģʽ.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool checkFileNameOfYieldsReport(string fileName) {
            // ƥ�� ���ĵ�������ʽ��Unicode����ʾʱ����ΧΪ[\u4e00-\u9fa5],[\u4e00-\u9fbb]
            if (Regex.IsMatch(fileName, "[1-9]��[1-9]��[\u4e00-\u9fa5]+")) return true;
            if (Regex.IsMatch(fileName, "[1-9]��1[0-2]��[\u4e00-\u9fa5]+")) return true;
            //�ٴ�ƥ�������ģʽ.
            if (Regex.IsMatch(fileName, "1[0-9]��[1-9]��[\u4e00-\u9fa5]+")) return true;
            if (Regex.IsMatch(fileName, "1[0-9]��1[0-2]��[\u4e00-\u9fa5]+")) return true;
            return false;
        }

        #region ����ַ������Ƿ���һ�飬���������
        /// <summary>
        /// ����Ƿ�ƥ�䲿�� һ��***
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ifOtherDept(string name) {
            return Regex.IsMatch(name, @"^[1-9]��[\u4e00-\u9fbb]+$") || Regex.IsMatch(name, @"^[1-9][0-9]��[\u4e00-\u9fbb]+$");
        }
        #endregion
        #region
        /// <summary>
        /// ����Ƿ�ƥ�䲿�� һ��***
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ifOtherDeptByDigit(string name)
        {
            string pattern = @"^[0-9]{1}$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(name);
        }
        #endregion
    }
}
