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
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork uow;
        private readonly IAnswerRepository answerRepository;

        public AnswerService(IUnitOfWork uow, IAnswerRepository repository)
        {
            this.uow = uow;
            this.answerRepository = repository;        
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
    }
}
