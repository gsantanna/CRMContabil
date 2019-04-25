using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bie.focuscrm.domain.Enums;
using bie.focuscrm.ui.mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace bie.focuscrm.ui.mvc.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        //Identity
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly UserStore<ApplicationUser> _userStore;

        public BaseController()
        {

            DbContext context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        }


        public TipoUsuario TipoUsuarioAtual
        {
            get
            {
                if (User.IsInRole("Superadmin"))
                {
                    return TipoUsuario.Superadmin;

                }
                if (User.IsInRole("Administrador"))
                {
                    return TipoUsuario.Administrador;

                }
                if (User.IsInRole("Focus"))
                {
                    return TipoUsuario.Focus;

                }
                if (User.IsInRole("Setor"))
                {
                    return TipoUsuario.Setor;
                }
                return TipoUsuario.Cliente;
            }
        }

        public int IndicePermissao { get; set; }


    }
}