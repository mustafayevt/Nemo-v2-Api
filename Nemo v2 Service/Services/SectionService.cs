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
        private IRepository<Section> _sectionRepository;
        private IRepository<Table> _tableRepository;
        private IRepository<Restaurant> _restaurantRepository;

        public SectionService(IRepository<Section> sectionRepository,
            IRepository<Table> tableRepository,
            IRepository<Restaurant> restaurantRepository)
        {
            _sectionRepository = sectionRepository;
            _tableRepository = tableRepository;
            _restaurantRepository = restaurantRepository;
        }


        public IEnumerable<Section> GetSections()
        {
            return _sectionRepository.Get();
        }

        public IEnumerable<Section> GetSectionsByRestaurantId(long RestId)
        {
            return _sectionRepository.Query(x => x.RestaurantId == RestId)
                .Include(x=>x.Tables);
        }

        public Section GetSection(long id)
        {
            return _sectionRepository.Query(x => x.Id == id)
                .Include(x => x.Tables).First();
        }

        public Section InsertSection(Section Section)
        {
            if(_restaurantRepository.GetById(Section.RestaurantId).BranchId==null)
                throw new NullReferenceException("Cant add Section to the main restaurant");
            Section newSection=_sectionRepository.Insert(Section);
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

        public Section UpdateSection(Section Section)
        {
            var oldSection = _sectionRepository.Query(x=>x.Id==Section.Id).Include(x=>x.Tables).First();
            var deleteTables = oldSection.Tables.Where(x => Section.Tables.All(y => y.Id != x.Id)).ToList();
            var newTables = Section.Tables.Where(x => x.Id == 0);
            _tableRepository.InsertMany(newTables);
            deleteTables.ForEach(x =>
            {
                _tableRepository.Delete(x.Id);
            });
            
            return _sectionRepository.Update(Section);
        }

        public void DeleteSection(long id)
        {
            _sectionRepository.Delete(id);
        }
    }
}