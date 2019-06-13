using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public interface IPricelistRepository : IRepository<Pricelist, int>
    {
        IEnumerable<PricelistItem> GetPricelistItemsFromActivePricelist(DateTime currentTime);
        PricelistItem GetPricelistItemForSelectedTypes(TicketType ticketType, PassengerType passengerType, DateTime currentTime);
        float GetTicketPrice(TicketType ticketType, PassengerType passengerType, DateTime currentTime);
        PricelistItem GetPricelistItemByIds(int pricelistId, int pricelistItemId);
        HttpStatusCode UpdatePricelist(UpdatePricelistBindingModel bindingModel);
        HttpStatusCode DeletePricelist(int id);
    }
}
