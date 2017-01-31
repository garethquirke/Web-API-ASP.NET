// a RESTful services which provides stock market prices for stocks

using StockPrice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace StockPrice.Controllers
{
    public class StockController : ApiController
    {
       /* 
       * GET /api/stock/            get all stock listings               GetAllListings
       * GET /api/stock/IBM         get price last trade for IBM         GetStockPrice   
       */

        // the listings on this stock market
        private List<StockListing> listings;

        // initialise the listings collection, stateless
        public StockController()
        {
            listings = new List<StockListing>() 
                { 
                    new StockListing { TickerSymbol = "AAPL", Price = 464.90 }, 
                    new StockListing { TickerSymbol = "IBM", Price = 192.50  }, 
                    new StockListing { TickerSymbol = "GOOG", Price = 890.20  },
                    new StockListing { TickerSymbol = "MSFT", Price = 33.03  } 
                };
        }

        // todo: use repository pattern

        // GET api/stock
        public IHttpActionResult GetAllListings()
        {
            return Ok(listings.OrderBy(s => s.TickerSymbol).ToList());                                                   // 200 OK, listings serialized in response body
        }
      
        // GET api/stock/GOOG or api/stock?ticker=GOOG
        // default route template changed to api/{controller}/{id} rather than api/{controller}/{ticker} in WebApiConfig.cs
        public IHttpActionResult GetStockPrice(String ticker)
        {
            // LINQ query, find matching ticker (case-insensitive) or default value (null) if none matching
            StockListing listing = listings.SingleOrDefault(l => l.TickerSymbol.ToUpper() == ticker.ToUpper());
            if (listing == null)
            {
                return NotFound();
            }
            return Ok(listing.Price);                                               
        }

    }
}
