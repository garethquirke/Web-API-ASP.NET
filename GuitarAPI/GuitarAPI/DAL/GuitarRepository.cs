using GuitarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarAPI.DAL
{
    public class GuitarRepository
    {
        private ApplicationDBContext context = new ApplicationDBContext();


        public IEnumerable<Guitar> GetAllGuitars()
        {
            return context.Guitars;
        }


        public Guitar GetGuitarByName(string name)
        {
            Guitar guitar = context.Guitars.FirstOrDefault(g => g.Name.ToUpper() == name.ToUpper());
            return guitar;
        }

        public IEnumerable<Guitar> GetNewGuitars()
        {
            var guitars = context.Guitars.Where(g => g.IsNew == true);
            return guitars;
        }


        public int AddGuitar(Guitar g)
        {
            lock (context)
            {
                // check the stock see if its already there
                var stock = context.Guitars.SingleOrDefault(f => f.Name.ToUpper() == g.Name.ToUpper());
                if (stock == null)
                {
                    context.Guitars.Add(g);
                    return 1;
                }
                else
                {
                    stock.Stock++;
                    return 2;

                }
            }
        }



        public int DeleteGuitar(string name)
        {
            lock (context)
            {
                var record = context.Guitars.SingleOrDefault(g => g.Name.ToUpper() == name.ToUpper());
                if (record != null)
                {
                    context.Guitars.Remove(record);
                    return 1;
                   
                }
                else
                {
                    return 2;  
                }
            }
        }






    }
}