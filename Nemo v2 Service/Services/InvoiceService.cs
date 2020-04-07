using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Service.Services
{
    public class InvoiceService:IInvoiceService
    {
        private IRepository<Invoice> _invoiceRepository;
        private IRepository<Food> _foodRepository;

        public InvoiceService(IRepository<Invoice> invoiceRepository,
            IRepository<Food> foodRepository)
        {
            _invoiceRepository = invoiceRepository;
            _foodRepository = foodRepository;
        }

        public IEnumerable<Invoice> Get()
        {
            return _invoiceRepository.Get();
        }

        public IEnumerable<Invoice> GetInvoicesByRestaurantId(long RestId)
        {
            return _invoiceRepository.Query(x => x.RestaurantId == RestId)
                .Include(x => x.Foods)
                .ThenInclude(x=>x.Food);
        }

        public Invoice GetInvoice(long id)
        {
            return _invoiceRepository.Query(x => x.Id == id)
                .Include(x => x.Foods)
                .ThenInclude(x=>x.Food).First();
        }

        public Invoice InsertInvoice(Invoice invoice)
        {
            if (invoice.Foods?.Any() ?? false)
            {
                if (invoice.Foods.Any(x => x.Food.Id == 0))
                    throw new NullReferenceException("Food not found");

                var foodIds = invoice.Foods.Select(y => y.Food.Id).ToList();
                if (foodIds.Any())
                {
                    var foods = _foodRepository.Query(x => foodIds.Contains(x.Id)).ToList();

                    if (foods.Count() != invoice.Foods.Count())
                        throw new NullReferenceException("Food Not Found");
                    //invoice.Ingredients.Clear();
                    var foodInvoiceRels = new List<FoodInvoiceRel>();
                    for (int i = 0; i < foods.Count(); i++)
                    {
                        foodInvoiceRels.Add(new FoodInvoiceRel()
                        {
                            InvoiceId = invoice.Id,
                            FoodId = foods[i].Id,
                        });
                    }

                    invoice.Foods = foodInvoiceRels;
                }
            }
            
            return _invoiceRepository.Insert(invoice);
        }

        public Invoice UpdateInvoice(Invoice invoice)
        {
            if (invoice.Foods?.Any() ?? false)
            {
                if (invoice.Foods.Any(x => x.Food.Id == 0))
                    throw new NullReferenceException("Food not found");

                var foodIds = invoice.Foods.Select(y => y.Food.Id).ToList();
                if (foodIds.Any())
                {
                    var foods = _foodRepository.Query(x => foodIds.Contains(x.Id)).ToList();

                    if (foods.Count() != invoice.Foods.Count())
                        throw new NullReferenceException("Food Not Found");
                    //invoice.Ingredients.Clear();
                    var foodInvoiceRels = new List<FoodInvoiceRel>();
                    for (int i = 0; i < foods.Count(); i++)
                    {
                        foodInvoiceRels.Add(new FoodInvoiceRel()
                        {
                            InvoiceId = invoice.Id,
                            FoodId = foods[i].Id,
                        });
                    }

                    invoice.Foods = foodInvoiceRels;
                }
            }

            return _invoiceRepository.Update(invoice);
        }

        public void DeleteInvoice(long id)
        {
            _invoiceRepository.Delete(id);
        }
    }
}