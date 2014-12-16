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
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork uow;
        private readonly IFacultyRepository facultyRepository;

        public FacultyService(IUnitOfWork uow, IFacultyRepository repository)
        {
            this.uow = uow;
            this.facultyRepository = repository;
        }

        public IEnumerable<FacultyEntity> GetAllFacultyEntities()
        {
            return facultyRepository.GetAll().Select(answ => answ.ToBllFaculty());
        }

        public void CreateFaculty(FacultyEntity faculty)
        {
            facultyRepository.Create(faculty.ToDalFaculty());
            uow.Commit();
        }
    }
}
