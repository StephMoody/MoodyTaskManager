using LiteDB;
using MoodyTaskManager.Contract;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MoodyTaskManager.Data
{
    internal abstract class RepositoryBase<T> : IRepositoryBase where T : DataBase
    {
        protected RepositoryBase(T data)
        {
            Data = data;
        }

        public T Data { get; protected set; }

        public Task Initialize()
        {
            using (LiteDatabase db = GetDataBase())
            {
                LiteCollection<T> collection = db.GetCollection<T>();
                if (collection.Find(b => b.ID == Data.ID).Count() == 0)
                    collection.Insert(Data);
            }

            return Task.CompletedTask;
        }

        public Task Save()
        {
            using (LiteDatabase db = GetDataBase())
            {
                LiteCollection<T> collection = db.GetCollection<T>();
                T data = Data;
                collection.Update(data);
            }

            return Task.CompletedTask;
        }

        public Task Load()
        {
            using (LiteDatabase db = GetDataBase())
            {
                LiteCollection<T> collection = db.GetCollection<T>();
                T data = collection.FindOne(b => b.ID == Data.ID);
                Data = data;
            }

            return Task.CompletedTask;
        }
        
        private LiteDatabase GetDataBase()
        {
            string[] embeddedResources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            string dbName = embeddedResources.FirstOrDefault(b => b.EndsWith(".db"));
            return new LiteDatabase(dbName);
        }
    }
}
