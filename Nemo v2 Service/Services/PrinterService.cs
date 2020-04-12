using System;
using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class PrinterService : IPrinterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PrinterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Printer> Get()
        {
            return _unitOfWork.PrinterRepository.Get();
        }

        public IEnumerable<Printer> GetPrinterByRestaurantId(long RestId)
        {
            return _unitOfWork.PrinterRepository.Query(x => x.RestaurantId == RestId);
        }

        public Printer GetPrinter(long id)
        {
            return _unitOfWork.PrinterRepository.GetById(id);
        }

        public Printer InsertPrinter(Printer Printer)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.PrinterRepository.Insert(Printer);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ; 
            }
        }

        public Printer UpdatePrinter(Printer Printer)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result = _unitOfWork.PrinterRepository.Update(Printer);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ; 
            }
        }

        public void DeletePrinter(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction(); 
                _unitOfWork.PrinterRepository.Delete(id);
                _unitOfWork.Save();
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ; 
            }
        }
    }
}