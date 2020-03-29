using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class TableService:ITableService
    {
        private IRepository<Table> _tableRepository;

        public TableService(IRepository<Table> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public IEnumerable<Table> GetTables()
        {
            return _tableRepository.Get();
        }

        public IEnumerable<Table> GetTablesByRestaurantId(long RestId)
        {
            return _tableRepository.Query(x => x.RestaurantId == RestId);
        }

        public IEnumerable<Table> GetTablesBySectioinId(long SectionId)
        {
            return _tableRepository.Query(x => x.SectionId == SectionId);
        }

        public Table GetTable(long id)
        {
            return _tableRepository.GetById(id);
        }

        public Table InsertTable(Table Table)
        {
            return _tableRepository.Insert(Table);
        }

        public Table UpdateTable(Table Table)
        {
            return _tableRepository.Update(Table);
        }

        public void DeleteTable(long id)
        {
            _tableRepository.Delete(id);
        }
    }
}