﻿using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;

namespace Nemo_v2_Repo.Repositories.EFRepository
{
    public class EFSectionRepository:EFRepository<Section>,ISectionRepository
    {
        public EFSectionRepository(ApplicationContext context) : base(context)
        {
        }
    }
}