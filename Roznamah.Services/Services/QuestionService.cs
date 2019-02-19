using Roznamah.Model.Entities;
using Roznamah.Model.Questions;
using Roznamah.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace Roznamah.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IContentService _contentService;

        public QuestionService() {
            _contentService = ApplicationContext.Current.Services.ContentService; ;
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            var root = _contentService.GetRootContent().FirstOrDefault();
            var questions = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "Question");

            List<Question> QuestionList = new List<Question>();
            foreach (var item in questions)
            {
                QuestionList.Add(new Question
                {
                    Id = item.Id,
                    Name = item.Name,
                    TitleEn = Convert.ToString(item.GetValue("QuestionTitle")),
                    TitleAr = Convert.ToString(item.GetValue("QuestionTitleArabic"))
                });

            }

            return QuestionList;
        }

        public Question GetQuestion(int questionId)
        {
            var questionData = _contentService.GetById(questionId);
            Question QuestionItem = new Question
            {
                Id = questionData.Id,
                Name = questionData.Name,
                TitleEn = Convert.ToString(questionData.GetValue("QuestionTitle")),
                TitleAr = Convert.ToString(questionData.GetValue("QuestionTitleArabic"))
            };
            return QuestionItem;
        }

    }
}
