﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using GSD.Globalization;
using System.Threading;
using DataLayer;

namespace MyShop
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Application["Online"] = 0;
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var persianCulture = new PersianCulture();
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;
        }

        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(
                System.Web.SessionState.SessionStateBehavior.Required);
        }

        protected void Session_Start()
        {
            int online = int.Parse(HttpContext.Current.Application["Online"].ToString());
            online += 1;
            HttpContext.Current.Application["Online"] = online;
            string ip = Request.UserHostAddress;
            using (MyEShop_DBEntities _dbEntities = new MyEShop_DBEntities())
            {
                DateTime dt = DateTime.Now.Date;
                if (!_dbEntities.SiteVisit.Any(v => v.IP == ip & v.DateTime == dt))
                {
                    _dbEntities.SiteVisit.Add(new SiteVisit()
                    {
                        DateTime = DateTime.Now,
                        IP = ip
                    });
                }

                _dbEntities.SaveChanges();
            }
        }

        protected void Session_End()
        {
            int online = int.Parse(HttpContext.Current.Application["Online"].ToString());
            online -= 1;
            HttpContext.Current.Application["Online"] = online;
        }
    }
}