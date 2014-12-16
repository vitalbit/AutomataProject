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
    public class OptionService : IOptionService
    {
        private readonly IUnitOfWork uow;
        private readonly IOptionRepository optionRepository;

        public OptionService(IUnitOfWork uow, IOptionRepository repository)
        {
            this.uow = uow;
            this.optionRepository = repository;
        }

        public IEnumerable<OptionEntity> GetAllOptionEntities()
        {
            return optionRepository.GetAll().Select(answ => answ.ToBllOption());
        }

        public void CreateOption(OptionEntity option)
        {
            optionRepository.Create(option.ToDalOption());
            uow.Commit();
        }
    }
}
