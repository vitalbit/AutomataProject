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
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalCourse> courseRepository;

        public CourseService(IUnitOfWork uow)
        {
            this.uow = uow;
            this.courseRepository = uow.CourseRepository;
        }

        public IEnumerable<CourseEntity> GetAllCourseEntities()
        {
            return courseRepository.GetAll().Select(answ => answ.ToBllCourse());
        }

        public void CreateCourse(CourseEntity course)
        {
            courseRepository.Create(course.ToDalCourse());
            uow.Commit();
        }

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}
