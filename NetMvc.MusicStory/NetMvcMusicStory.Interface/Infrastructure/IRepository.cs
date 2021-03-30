using System;
using System.Linq;

namespace NetMvcMusicStory.Interface.Infrastructure
{
    //资源库标准，定标准
    public interface IRepository<T> where T : class, IEntity, new()
    {
        T GetSingle(Guid objectId);

        //获取多行数据
        IQueryable<T> GetAll();

        //新增一行数据
        int AddAndSave(T entiy);

        //删除一行数据
        int DeleteAndSave(T entiy);

        //修改一行以数据
        int EditAndSave(T entiy);


        IQueryable<T1> GetAllRalevance<T1>();

    }
}
