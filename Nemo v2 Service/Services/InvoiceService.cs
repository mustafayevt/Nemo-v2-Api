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
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Invoice> Get()
        {
            return _unitOfWork.InvoiceRepository.Get();
        }

        public IEnumerable<Invoice> GetInvoicesByRestaurantId(long RestId)
        {
            return _unitOfWork.InvoiceRepository.Query(x => x.RestaurantId == RestId)
                .Include(x => x.Foods)
                .ThenInclude(x=>x.Food);
        }

        public Invoice GetInvoice(long id)
        {
            return _unitOfWork.InvoiceRepository.Query(x => x.Id == id)
                .Include(x => x.Foods)
                .ThenInclude(x=>x.Food).First();
        }

        public Invoice InsertInvoice(Invoice invoice)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (invoice.Foods?.Any() ?? false)
                {
                    if (invoice.Foods.Any(x => x.Food.Id == 0))
                        throw new NullReferenceException("Food not found");

                    var foodIds = invoice.Foods.Select(y => y.Food.Id).ToList();
                    if (foodIds.Any())
                    {
                        var foods = _unitOfWork.FoodRepository.Query(x => foodIds.Contains(x.Id)).ToList();

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
            
                var result = _unitOfWork.InvoiceRepository.Insert(invoice);
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

        public Invoice UpdateInvoice(Invoice invoice)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if (invoice.Foods?.Any() ?? false)
                {
                    if (invoice.Foods.Any(x => x.Food.Id == 0))
                        throw new NullReferenceException("Food not found");

                    var foodIds = invoice.Foods.Select(y => y.Food.Id).ToList();
                    if (foodIds.Any())
                    {
                        var foods = _unitOfWork.FoodRepository.Query(x => foodIds.Contains(x.Id)).ToList();

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
            
                var result = _unitOfWork.InvoiceRepository.Update(invoice);
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

        public void DeleteInvoice(long id)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                _unitOfWork.InvoiceRepository.Delete(id);
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