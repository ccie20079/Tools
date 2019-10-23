﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
namespace Tools
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckVersionForApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftp_IPADDR"></param>
        /// <param name="pName"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static MSG checkVersionByPN(string ftp_IPADDR,string pName,string version) {

            TheFTPHelper ftpHelper = new TheFTPHelper(ftp_IPADDR, "Anonymous", "");
            if (!ftpHelper.CheckDirectoryExist(ftpHelper.getFtpHost(), string.Format(@"/{0}/",pName)))
            {
                MessageBox.Show(string.Format(@"FTP Server, {0} 尚未创建目录: {1}", ftp_IPADDR,pName),"提示：",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return new MSG(string.Format(@"FTP Server, {0} 尚未创建目录: {1}", ftp_IPADDR, pName),false);
            }
            string saveFileUrl = Application.StartupPath + "/version.txt";
            //下载版本文件
            try {
                ftpHelper.DownLoadFile(ftpHelper.getFtpHost() + string.Format(@"/{0}/Version.txt", pName), saveFileUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new MSG(ex.ToString(),false);
            }            //读取该文件
            string result = FileHelper.readFile(saveFileUrl);
            String[] resultArray = result.Split(Environment.NewLine.ToCharArray());
            foreach (string str in resultArray) {
                if (!str.Contains(pName)) continue;
                //判断版本是否为　制定的版本．
                string[] productsInfoArray = str.Split(':');
                string lastestReleaseVersion = productsInfoArray[1].Trim();
                if (!version.Equals(lastestReleaseVersion)) {
                    MessageBox.Show(string.Format(@"当前版本：" + version + ", 与最新版本{0} 不一致,请更新!",lastestReleaseVersion), "提示: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return new MSG("当前版本：" + version + ", 与最新版本不一致,请更新!",false);
                }
                //
                return new MSG("当前版本：" + version + ", 与最新版本一致.", true);
            }
            //未注册，不做检查．
            return new MSG("该程序未注册！",true);
        }
      /// <summary>
      /// /
      /// </summary>
      /// <param name="ftp_IP_ADDR"></param>
      /// <param name="pName"></param>
      /// <returns></returns>
        public static string getLastestReleaseVersionByPN(string ftp_IP_ADDR,string pName) {
            TheFTPHelper ftpHelper = new TheFTPHelper(ftp_IP_ADDR, "Anonymous", "");
            if (!ftpHelper.CheckDirectoryExist(ftpHelper.getFtpHost(), string.Format(@"/{0}/", pName)))
            {
                MessageBox.Show(string.Format(@"FTP Server 尚未创建目录: {0}",  pName), "提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return string.Empty;
            }
            string saveFileUrl = Application.StartupPath + "/version.txt";
            //下载目录文件。
            ftpHelper.DownLoadFile(ftpHelper.getFtpHost() +  "/" + pName+"/Version.txt", saveFileUrl);
            //读取该文件
            string result = FileHelper.readFile(saveFileUrl);
            String[] resultArray = result.Split(Environment.NewLine.ToCharArray());
            foreach (string str in resultArray)
            {
                if (!str.Contains(pName)) continue;
                //判断版本是否为　制定的版本．
                string[] productsInfoArray = str.Split(':');
                string lastestReleaseVersion = productsInfoArray[1].Trim();
                //删除版本文件
                File.Delete(saveFileUrl);
                return lastestReleaseVersion;
            }
            MessageBox.Show(string.Format(@"此应用 {0}: 尚未注册!",pName), "提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
            File.Delete(saveFileUrl);
            //未注册，返回空
            return string.Empty;
        }
    }
}
