using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IAnswerService : IDisposable
    {
        IEnumerable<AnswerEntity> GetAllAnswerEntities();
        void CreateAnswer(AnswerEntity answer);
    }
}
