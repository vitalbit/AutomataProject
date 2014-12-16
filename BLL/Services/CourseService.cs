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
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork uow;
        private readonly ICourseRepository courseRepository;

        public CourseService(IUnitOfWork uow, ICourseRepository repository)
        {
            this.uow = uow;
            this.courseRepository = repository;
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
    }
}
