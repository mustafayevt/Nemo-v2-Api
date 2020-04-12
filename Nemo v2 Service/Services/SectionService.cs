using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class SectionService:ISectionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Section> GetSections()
        {
            return _unitOfWork.SectionRepository.Get();
        }

        public IEnumerable<Section> GetSectionsByRestaurantId(long RestId)
        {
            return _unitOfWork.SectionRepository.Query(x => x.RestaurantId == RestId)
                .Include(x=>x.Tables);
        }

        public Section GetSection(long id)
        {
            return _unitOfWork.SectionRepository.Query(x => x.Id == id)
                .Include(x => x.Tables).First();
        }

        public Section InsertSection(Section Section)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if(_unitOfWork.RestaurantRepository.GetById(Section.RestaurantId).BranchId==null)
                    throw new NullReferenceException("Cant add Section to the main restaurant");
                Section newSection=_unitOfWork.SectionRepository.Insert(Section);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                // if (Section.Tables?.Any() ?? false)
                // {
                //     if (Section.Tables.Any(x => x.Id == 0))
                //     {
                //         IEnumerable<Table> newTables;
                //         newTables = _tableRepository
                //             .InsertMany(Section.Tables
                //                 .Where(x => x.Id == 0));
                //
                //         Section.Tables.RemoveAll(x => x.Id == 0);
                //     }
                // }

                return newSection;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw ;
            }
        }

        public Section UpdateSection(Section Section)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                var oldSection = _unitOfWork.SectionRepository.Query(x=>x.Id==Section.Id).Include(x=>x.Tables).First();
                var deleteTables = oldSection.Tables.Where(x => Section.Tables.All(y => y.Id != x.Id)).ToList();
                var newTables = Section.Tables.Where(x => x.Id == 0);
                _unitOfWork.TableRepository.InsertMany(newTables);
                deleteTables.ForEach(x =>
                {
                    _unitOfWork.TableRepository.Delete(x.Id);
                });
            
                var result =  _unitOfWork.SectionRepository.Update(Section);
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

        public void DeleteSection(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.SectionRepository.Delete(id);
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