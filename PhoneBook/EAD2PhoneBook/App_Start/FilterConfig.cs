﻿using System.Web;
using System.Web.Mvc;

namespace EAD2PhoneBook
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
