using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface ITableService
    {
        IEnumerable<Table> GetTables();
        IEnumerable<Table> GetTablesByRestaurantId(long RestId);
        IEnumerable<Table> GetTablesBySectioinId(long SectionId);
        Table GetTable(long id);
        Table InsertTable(Table Table);
        Table UpdateTable(Table Table);
        void DeleteTable(long id);
    }
}