// a StockListing i.e. ticker and price

using System.ComponentModel.DataAnnotations;

namespace StockPrice.Models
{
    // a listing for a stock on the stock market
    public class StockListing
    {
        // ticker symbol e.g. AAPL, GOOG, IBM, MSFT
        [Required]
        public string TickerSymbol
        {
            get;
            set;
        }

        // price last trade in $
        [Range(0, 1000)]
        public double Price
        {
            get;
            set;
        }

    }
}