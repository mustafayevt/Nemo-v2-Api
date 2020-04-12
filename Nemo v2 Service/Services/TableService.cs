using System;
using System.Collections.Generic;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class TableService:ITableService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Table> GetTables()
        {
            return _unitOfWork.TableRepository.Get();
        }

        public IEnumerable<Table> GetTablesByRestaurantId(long RestId)
        {
            return _unitOfWork.TableRepository.Query(x => x.RestaurantId == RestId);
        }

        public IEnumerable<Table> GetTablesBySectioinId(long SectionId)
        {
            return _unitOfWork.TableRepository.Query(x => x.SectionId == SectionId);
        }

        public Table GetTable(long id)
        {
            return _unitOfWork.TableRepository.GetById(id);
        }

        public Table InsertTable(Table Table)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result =  _unitOfWork.TableRepository.Insert(Table);
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

        public Table UpdateTable(Table Table)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var result =  _unitOfWork.TableRepository.Update(Table);
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

        public void DeleteTable(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.TableRepository.Delete(id);
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