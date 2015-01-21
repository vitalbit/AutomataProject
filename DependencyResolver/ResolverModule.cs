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

namespace DependencyResolver
{
    public class ResolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<EntityModel>();

            Bind<IUnitOfWork>().To<UnitOfWork>();

            Bind<IAnswerService>().To<AnswerService>();
            Bind<IAttachmentContentService>().To<AttachmentContentService>();
            Bind<IBlockService>().To<BlockService>();
            Bind<IBlockTypeService>().To<BlockTypeService>();
            Bind<ICourseService>().To<CourseService>();
            Bind<IFacultyService>().To<FacultyService>();
            Bind<IGroupService>().To<GroupService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<ISpecialityService>().To<SpecialityService>();
            Bind<ITestService>().To<TestService>();
            Bind<IUserService>().To<UserService>();

            Bind<ITestConvert>().To<XmlConverter>();
            Bind<IGradeSystem>().To<GradeSystem>();
        }
    }
}
