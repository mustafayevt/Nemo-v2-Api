using System.Collections.Generic;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Service.Abstraction
{
    public interface ISectionService
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Section> GetSectionsByRestaurantId(long RestId);
        Section GetSection(long id);
        Section InsertSection(Section Section);
        Section UpdateSection(Section Section);
        void DeleteSection(long id);
    }
}