using Core.Ports.Driven;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBase;

namespace Persistence.Services
{
    public class DataBaseService : IDataBaseService
    {
        private readonly DataBaseContext _dataBaseContext;

        public DataBaseService(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public void UpdateDatabase()
        {
            _dataBaseContext.Database.Migrate();
        }
    }
}
