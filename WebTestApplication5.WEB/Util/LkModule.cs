using Ninject.Modules;
using WebTestApplication5.BLL.Services;
using WebTestApplication5.BLL.Interfaces;

namespace WebTestApplication5.WEB.Util
{
    public class LkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IActionService>().To<ActionService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<ICompanyUserRolesService>().To<CompanyUserRolesService>();
        }
    }
}
