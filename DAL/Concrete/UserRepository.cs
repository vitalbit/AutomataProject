using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().ToList().Select(answ => answ.ToDalUser());
        }

        public DalUser GetById(int key)
        {
            return context.Set<User>().ToList().FirstOrDefault(answ => answ.UserId == key).ToDalUser();
        }

        public DalUser GetByPredicate(System.Linq.Expressions.Expression<Func<DalUser, bool>> f)
        {
            Func<DalUser, bool> func = f.Compile();
            IEnumerable<DalUser> answers = GetAll();
            return answers.FirstOrDefault(answ => func(answ));
        }

        public void Create(DalUser e)
        {
            context.Set<User>().Add(e.ToOrmUser());
        }

        public void Delete(DalUser e)
        {
            context.Set<User>().Remove(e.ToOrmUser());
        }

        public void Update(DalUser e)
        {
            User user = context.Set<User>().ToList().FirstOrDefault(answ => answ.UserId == e.Id);
            user.CourseId = e.CourseId;
            user.Email = e.Email;
            user.FacultyId = e.FacultyId;
            user.FirstName = e.FirstName;
            user.GroupId = e.GroupId;
            user.LastName = e.LastName;
            user.Nickname = e.Nickname;
            user.Password = e.Password;
            user.RoleId = e.RoleId;
            user.SpecialityId = e.SpecialityId;
            context.Set<User>().Attach(user);
            context.Entry(user).State = EntityState.Modified;
        }
    }
}
