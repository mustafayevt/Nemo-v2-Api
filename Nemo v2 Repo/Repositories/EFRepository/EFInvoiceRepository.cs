using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;
using Nemo_v2_Repo.Helper;

namespace Nemo_v2_Repo.Repositories.EFRepository
{
    public class EFInvoiceRepository:EFRepository<Invoice>,IInvoiceRepository
    {
        public EFInvoiceRepository(ApplicationContext context) : base(context)
        {
        }
        
        public override Invoice Update(Invoice entity)
        {
            try
            {
                if (entity.InvoiceTableRels == null) entity.InvoiceTableRels = new List<InvoiceTableRel>();

                var model = (context as ApplicationContext).Invoices
                    .AsNoTracking()
                    .Include(x => x.InvoiceTableRels)
                    .FirstOrDefault(x => x.Id == entity.Id);
                context.TryUpdateManyToMany(model.InvoiceTableRels, entity.InvoiceTableRels, x => x.InvoiceId);

                return base.Update(entity);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw;
            }
        }
    }
}