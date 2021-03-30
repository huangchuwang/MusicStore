using NetMvcMusicStory.Models.MusicStoryDbConnection;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
namespace NetMvcMusicStory.Interface.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        public MusicStoreDbContext _context { get; private set; }//定义数据库上下文对象
        public Repository(MusicStoreDbContext context)
        {
            _context = context;//数据库上下文初始化
            _context.Database.Log = sql =>//获取拼接的sql
            {
                Console.WriteLine(sql);
            };
        }


        /*
         *  name:hcw
         *  time: 2020/12/10
         *  content：查询单条数据
         *  param:objectId
         *  return：T
         */
        public T GetSingle(Guid objectId)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == objectId);
        }

        /// <summary>
        /// 获取实体数据集
        /// </summary>
        /// <returns>实体数据集</returns>
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }


        /// <summary>
        /// 新增一个实体对象/新增一行数据
        /// </summary>
        /// <param name="entiy">实体对象/具体一行数据</param>
        public int AddAndSave(T entiy)
        {
            _context.Set<T>().Add(entiy);
           return _context.SaveChanges();//持久化/入库保存
        }

        //删除一行数据
        public int DeleteAndSave(T entiy)
        {
            _context.Set<T>().Remove(entiy);
            return  _context.SaveChanges();//持久化/入库保存
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">一行具体数据</param>
        public int EditAndSave(T entity)
        {
            if (entity == null)
                throw new ArgumentException();
            ///由于EF编辑和上次时会次执行context，导致context中保留当前entity副本
            /// 通过RemoveHoldingEntityInContext监测context中修改键值context中修改键值冲突
            DbEntityEntry dbEntityEntry = _context.Entry<T>(entity);
            RemoveHoldingEntityInContext(entity);
            _context.Set<T>().Attach(entity);
            dbEntityEntry.State = EntityState.Modified;
            return   _context.SaveChanges();//持久化/入库保存
        }



        Boolean RemoveHoldingEntityInContext(T entity)
        {
            var objContext = ((IObjectContextAdapter)_context).ObjectContext;//新建当前DB上下文对象
            var objSet = objContext.CreateObjectSet<T>();//获取实体集合
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);
            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);//找副本
            if (exists)
                objContext.Detach(foundEntity);// 清除
            return (exists);
        }


        /// <summary>
        /// 获取关联类数据集
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>

        public IQueryable<T1> GetAllRalevance<T1>()
        {
            var dbSet = _context.Set(typeof(T1));
            var query = dbSet as IQueryable<T1>;
            return query;
        }


    }
}