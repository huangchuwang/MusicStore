using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace AuthorityManagementApi.ServiceInstance.Unity.Util
{
    public static class Encryption
    {
        /*
        *  name:hcw
        *  time: 2020/12/28
        *  content：MD5加密
        *  return：json
        */
        public static string MD5(string str)
        {
            //就是比string往后一直加要好的优化容器
            //StringBuilder可以进行的处理仅限于替换和追加或删除字符串中的文本，但它的工作方式非常高效。字符串生成器
            StringBuilder result = new StringBuilder();

            //MD5CryptoServiceProvider   是 加密服务提供程序   用来 计算字符串的 MD5 哈希值
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                //将输入字符串转换为字节数组并计算哈希。
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

                //X为     十六进制 X都是大写 x都为小写
                //2为 每次都是两位数
                //假设有两个数10和26，正常情况十六进制显示0xA、0x1A，这样看起来不整齐，为了好看，可以指定"X2"，这样显示出来就是：0x0A、0x1A。 
                //遍历哈希数据的每个字节
                //并将每个字符串格式化为十六进制字符串。
                int length = data.Length;
                for (int i = 0; i < length; i++)
                    result.Append(data[i].ToString("x2"));
            }
            return result.ToString();
        }

    }
}