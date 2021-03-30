using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.AdminControllers
{
    public class FileUploadController : Controller
    {
        Hashtable tab;
        public FileUploadController()
        {
             tab = new Hashtable();
        }

        /*
        *  name:hcw
        *  time: 2020/12/20
        *  content：lrc歌词文件制作
        *  return：string
        */
        public  string  CreateLrc(string path,string content)
        {
            //title = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(file.FileName);
            //string RelativePath = "../Content/UploaFile/Photo/" + title;//相对路径
                FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(content);//开始写入歌词
                sw.Close();
                fs1.Close();
                return "../Content/UploaFile/Lrc/";            
        }



        /*
        *  name:hcw
        *  time: 2020/12/20
        *  content：编辑歌词文件内容
        *  return：json
        */
        public string EditLrc(string path,string lrcdata)
        {
            //title = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(file.FileName);
            //string RelativePath = "../Content/UploaFile/Photo/" + title;//相对路径
            if (!System.IO.File.Exists(path)) //判断文件是否存在
            {
                FileStream fs1 = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);//创建或编辑lrc文件 
                //清空歌词文件内容
                fs1.Seek(0, SeekOrigin.Begin);
                fs1.SetLength(0);


                //重新写入
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(lrcdata);//开始写入歌词
                fs1.Close();
         

                tab.Add("code", 0);
                tab.Add("msg", "编辑成功");
                return JsonConvert.SerializeObject(tab);
            }

            tab.Add("code", 1);
            tab.Add("msg", "编辑失败");
            return JsonConvert.SerializeObject(tab);
        }



                /*
                *  name:hcw
                *  time: 2020/12/17
                *  content：图片上传
                *  return：string
                */
                [HttpPost]
            public string Upload()
            {
                if (Request.Files.Count > 0)
                {
                    if (Request.Files.Count == 1)
                    {
                        HttpPostedFileBase file = Request.Files[0];
                    
                        if (file.ContentLength > 0)
                        {
                            string title = string.Empty;
                            title = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(file.FileName);
                            string RelativePath = "../Content/UploaFile/Photo/" + title;//相对路径
                            string path = System.Web.HttpContext.Current.Server.MapPath(RelativePath);//绝对路径
                            file.SaveAs(path);
                     
                            tab.Add("code", 0);
                            tab.Add("msg", "添加成功");
                            tab.Add("urlstring", RelativePath);
                            return JsonConvert.SerializeObject(tab);                   
                        }
                    }          
                }
                            tab.Add("msg","操作失败");
                            return JsonConvert.SerializeObject(tab);
            }



                /*
                *  name:hcw
                *  time: 2020/12/17
                *  content：文件上传
                *  return：string
                */
                [HttpPost]
                public string UploadMusic()
                {
                    if (Request.Files.Count > 0)
                    {
                        if (Request.Files.Count == 1)
                        {
                            HttpPostedFileBase file = Request.Files[0];

                            if (file.ContentLength > 0)
                            {
                                string route = "../Content/UploaFile/Music/";
                                if (!System.IO.Directory.Exists(Server.MapPath(route)))
                                {
                                    System.IO.Directory.CreateDirectory(Server.MapPath(route));//不存在就创建目录
                                }
                                string title = string.Empty;
                                title = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(file.FileName);
                                string RelativePath = route + title;//相对路径
                                string path = System.Web.HttpContext.Current.Server.MapPath(RelativePath);//绝对路径
                                file.SaveAs(path);

                                tab.Add("code", 0);
                                tab.Add("msg", "添加成功");
                                tab.Add("musicurl", RelativePath);
                                return JsonConvert.SerializeObject(tab);
                            }
                        }
                    }
                                tab.Add("msg", "操作失败");
                                return JsonConvert.SerializeObject(tab);
            }



            /*
            *  name:hcw
            *  time: 2020/12/15
            *  content：文件删除
            *  return：json
            */
            [HttpPost]
            public string DeleteFiles(string FilePath= null) 
            {
                if (System.IO.File.Exists(FilePath))//判断源文件是否存在
                {
                    System.IO.File.Delete(FilePath);//执行IO文件删除,需引入命名空间System.IO;
                    tab.Add("code", 0);
                    tab.Add("msg", "删除成功");
                    return JsonConvert.SerializeObject(tab);
                }
                tab.Add("code", 1);
                tab.Add("msg", "操作失败");
                return JsonConvert.SerializeObject(tab);
            }

    }
}