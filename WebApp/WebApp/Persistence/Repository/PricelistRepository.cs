using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    }
}