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
    public class SpecialityService : ISpecialityService
    {
        private readonly IUnitOfWork uow;
        private readonly ISpecialityRepository specialityRepository;

        public SpecialityService(IUnitOfWork uow, ISpecialityRepository repository)
        {
            this.uow = uow;
            this.specialityRepository = repository;
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
    }
}
