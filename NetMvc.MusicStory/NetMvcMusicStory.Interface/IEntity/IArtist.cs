
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMvcMusicStory.Interface.IEntity
{

    /// <summary>
    /// 专辑歌手业务实体
    /// </summary>
  public interface IArtist 
    {
        object GetSingle(Guid objectId);

        //获取多行数据
        object GetAll();

        //新增一行数据
        int AddAndSave(object entiy);

        //删除一行数据
        int DeleteAndSave(object entiy);

        //修改一行以数据
        int EditAndSave(object entiy);
    }
}