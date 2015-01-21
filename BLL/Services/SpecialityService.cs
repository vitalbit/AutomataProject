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
    public class SpecialityService : ISpecialityService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalSpeciality> specialityRepository;

        public SpecialityService(IUnitOfWork uow)
        {
            this.uow = uow;
            this.specialityRepository = uow.SpecialityRepository;
        }

        public IEnumerable<SpecialityEntity> GetAllSpecialityEntities()
        {
            return specialityRepository.GetAll().Select(answ => answ.ToBllSpeciality());
        }

        public void CreateSpeciality(SpecialityEntity speciality)
        {
            specialityRepository.Create(speciality.ToDalSpeciality());
            uow.Commit();
        }

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}
