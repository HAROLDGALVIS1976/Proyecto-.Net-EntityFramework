using HUEVOSFESTIVAL.DAL;
using HUEVOSFESTIVAL.Repository;
using PagedList;
using PagedList.Mvc;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;
using HUEVOSFESTIVAL.Models;
using HUEVOSFESTIVAL.Models.Home;
using System.Web.Mvc;

namespace HUEVOSFESTIVAL.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        FESTIVALEntities context = new FESTIVALEntities();

        public IPagedList<REFERENCIAS> listOfProducts { get; set; }
       

        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {
           
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@search",search??(object)DBNull.Value)
            };
            IPagedList<REFERENCIAS> data = context.Database.SqlQuery<REFERENCIAS>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                listOfProducts = data

            };
        }


    }



}