using BLL.Interface.Services;
using BLL.Interface.Entities;
using BLL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;
using DAL.Interface.DTO;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalRole> roleRepository;

        public RoleService(IUnitOfWork uow)
        {
            this.uow = uow;
            this.roleRepository = uow.RoleRepository;
        }

        public IEnumerable<RoleEntity> GetAllRoleEntities()
        {
            return roleRepository.GetAll().Select(answ => answ.ToBllRole());
        }

        public void CreateRole(RoleEntity role)
        {
            roleRepository.Create(role.ToDalRole());
            uow.Commit();
        }

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}
