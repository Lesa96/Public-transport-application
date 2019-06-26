using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public class PricelistRepository : Repository<Pricelist, int>, IPricelistRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }

        public PricelistRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<PricelistItem> GetPricelistItemsFromActivePricelist(DateTime currentTime)
        {
            return AppDbContext.Pricelists.Where(p => p.ValidFrom < currentTime && p.ValidUntil > currentTime).FirstOrDefault().PricelistItems;
        }

        public PricelistItem GetPricelistItemForSelectedTypes(TicketType ticketType, PassengerType passengerType, DateTime currentTime)
        { 
            var activePricelist = GetPricelistItemsFromActivePricelist(currentTime);
            return activePricelist.Where(item => item.TicketType == ticketType
                                    && item.PassengerType == passengerType)
                                    .FirstOrDefault();
        }

        public float GetTicketPrice(TicketType ticketType, PassengerType passengerType, DateTime currentTime)
        {
            return GetPricelistItemForSelectedTypes(ticketType, passengerType, currentTime).Price;
        }

        public PricelistItem GetPricelistItemByIds(int pricelistId, int pricelistItemId)
        {
            var pricelist = AppDbContext.Pricelists.Where(p => p.PricelistId == pricelistId).FirstOrDefault();

            return pricelist.PricelistItems.Where(pi => pi.PricelistItemId == pricelistItemId).FirstOrDefault();
        }

        public HttpStatusCode UpdatePricelist(UpdatePricelistBindingModel bindingModel)
        {         
            try
            {
                var pricelist = AppDbContext.Pricelists.Where(p => p.PricelistId == bindingModel.Id).FirstOrDefault();
                if (pricelist != null)
                {
                    for (int i = 0; i < pricelist.RowVersion.Count(); i++)
                    {
                        if (pricelist.RowVersion[i] != bindingModel.RowVersion[i])
                            return HttpStatusCode.Conflict;
                    }

                    pricelist.ValidFrom = bindingModel.ValidFrom;
                    pricelist.ValidUntil = bindingModel.ValidUntil;
                        
                    foreach (var item in bindingModel.PricelistItems)
                    {
                        var pricelistItem = AppDbContext.PricelistItems.Where(pi => pi.PricelistItemId == item.PricelistItemId).FirstOrDefault();
                        pricelistItem.Price = item.Price;
                    }

                    AppDbContext.SaveChanges();
                    return HttpStatusCode.OK;
                }
                return HttpStatusCode.NotFound;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Trace.WriteLine("DbUpdateConcurrencyException Message: {0}", ex.Message);
                return HttpStatusCode.Conflict;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("NormalException Message: {0}", ex.Message);
                return HttpStatusCode.Conflict;
            }


        }

        public HttpStatusCode DeletePricelist(int id)
        {
            
                try
                {
                    var pricelist = AppDbContext.Pricelists.Where(p => p.PricelistId == id).FirstOrDefault();
                    if (pricelist != null)
                    {
                        AppDbContext.Pricelists.Remove(pricelist);
                        AppDbContext.SaveChanges();

                        return HttpStatusCode.OK;
                    }
                    return HttpStatusCode.NotFound;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Trace.WriteLine("DbUpdateConcurrencyException Message: {0}", ex.Message);
                    return HttpStatusCode.Conflict;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("NormalException Message: {0}", ex.Message);
                    return HttpStatusCode.Conflict;
                }


        }
    }
}