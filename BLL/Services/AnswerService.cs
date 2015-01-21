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
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalAnswer> answerRepository;

        public AnswerService(IUnitOfWork uow)
        {
            this.uow = uow;
            this.answerRepository = uow.AnswerRepository;
        }

        public IEnumerable<AnswerEntity> GetAllAnswerEntities()
        {
            return answerRepository.GetAll().Select(answ => answ.ToBllAnswer());
        }

        public void CreateAnswer(AnswerEntity answer)
        {
            answerRepository.Create(answer.ToDalAnswer());
            uow.Commit();
        }

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}
