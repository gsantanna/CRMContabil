using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bie.focuscrm.ui.mvc.Models;
using bie.focuscrm.ui.viewmodel.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin;

namespace bie.focuscrm.ui.mvc.Helpers
{
    public static class IdentityHelper
    {
        public static ApplicationUser GetCurrentUser()
        {
            return
                HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }





        public static ItemMenuViewmodel[] GetMenuConfig()
        {
            var menus = new ItemMenuViewmodel[]
            {

                new ItemMenuViewmodel
                {
                    LinkText = "Home",
                    ActionName = "Index",
                    ControllerName = "Home",
                    Roles = "All",
                    Ordem = 0 ,
                },
                new ItemMenuViewmodel
                {
                    LinkText = "Atendimentos",
                    ActionName = "Index",
                    ControllerName = "Atendimentos",
                    Roles = "All",
                    Ordem = 1

                },
                new ItemMenuViewmodel
                {
                    Group= "Cadastros",
                    LinkText = "Empresas",
                    ActionName = "Index",
                    ControllerName = "Empresas",
                    Roles = "Administrador,Focus",
                    Ordem= 3
                },
                new ItemMenuViewmodel
                {
                    Group = "Cadastros",
                    LinkText = "Setores",
                    ActionName = "Index",
                    ControllerName = "Setor",
                    Roles = "Administrador,Focus",
                    Ordem = 4

                },

                 new ItemMenuViewmodel
                {
                     Group = "Cadastros",
                    LinkText = "Alertas",
                    ActionName = "Index",
                    ControllerName = "Alertas",
                    Roles = "Administrador,Focus",
                    Ordem = 5

                },
                 new ItemMenuViewmodel
                 {
                     Group = "Cadastros",
                     LinkText = "Pendências",
                     ActionName = "Index" ,
                     ControllerName = "Pendencias" ,
                     Roles = "Administrador,Focus,Setor",
                     Ordem=6
                 },
                  new ItemMenuViewmodel
                {
                    Group = "Administração",
                    LinkText = "Usuarios",
                    ActionName = "Index",
                    ControllerName = "Usuarios",
                    Roles = "Administrador" ,
                    Ordem= 2
                },
                  new ItemMenuViewmodel
                {
                    Group = "Administração",
                    LinkText = "Pesquisas",
                    ActionName = "Index",
                    ControllerName = "Pesquisa",
                    Roles = "Administrador,Focus",
                    Ordem = 5

                }



            };


            return menus;
        }
    }
}