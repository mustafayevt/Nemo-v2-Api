﻿using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;

namespace Nemo_v2_Repo.Repositories.EFRepository
{
    public class EFBuyerRepository:EFRepository<Buyer>, IBuyerRepository
    {
        public EFBuyerRepository(ApplicationContext context) : base(context)
        {
        }
    }
}