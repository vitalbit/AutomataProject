using Ninject.Modules;
using ORM;
using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Concrete;
using BLL.Interface.Services;
using BLL.Services;
using XMLConvertation;
using Systems;
using DAL.Interface.DTO;
using RegexpressionProcess;

namespace DependencyResolver
{
    public class ResolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<EntityModel>();

            Bind<IUnitOfWork>().To<UnitOfWork>();

            Bind<IContentService>().To<ContentService>();
            Bind<IBlockService>().To<BlockService>();
            Bind<IUserService>().To<UserService>();

            Bind<ITestConvert>().To<XmlConverter>();
            Bind<IGradeSystem>().To<GradeSystem>();
            Bind<IRegExpCheck>().To<RegExpCheck>();
        }
    }
}
