using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class PrinterService : IPrinterService
    {
        private IRepository<Printer> _printerRepository;

        public PrinterService(IRepository<Printer> printerRepository)
        {
            _printerRepository = printerRepository;
        }

        public IEnumerable<Printer> Get()
        {
            return _printerRepository.Get();
        }

        public IEnumerable<Printer> GetPrinterByRestaurantId(long RestId)
        {
            return _printerRepository.Query(x => x.RestaurantId == RestId);
        }

        public Printer GetPrinter(long id)
        {
            return _printerRepository.GetById(id);
        }

        public Printer InsertPrinter(Printer Printer)
        {
            return _printerRepository.Insert(Printer);
        }

        public Printer UpdatePrinter(Printer Printer)
        {
            return _printerRepository.Update(Printer);
        }

        public void DeletePrinter(long id)
        {
            _printerRepository.Delete(id);
        }
    }
}