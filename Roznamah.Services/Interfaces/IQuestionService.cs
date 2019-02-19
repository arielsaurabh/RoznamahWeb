using Roznamah.Model.Entities;
using Roznamah.Model.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roznamah.Services.Interfaces
{
    public interface IQuestionService
    {
        IEnumerable<Question> GetAllQuestions();
        Question GetQuestion(int questionId);
    }
}
