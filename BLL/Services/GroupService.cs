using BLL.Interface.Services;
using BLL.Interface.Entities;
using BLL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork uow;
        private readonly IGroupRepository groupRepository;

        public GroupService(IUnitOfWork uow, IGroupRepository repository)
        {
            this.uow = uow;
            this.groupRepository = repository;
        }

        public IEnumerable<GroupEntity> GetAllGroupEntities()
        {
            return groupRepository.GetAll().Select(answ => answ.ToBllGroup());
        }

        public void CreateGroup(GroupEntity group)
        {
            groupRepository.Create(group.ToDalGroup());
            uow.Commit();
        }
    }
}
