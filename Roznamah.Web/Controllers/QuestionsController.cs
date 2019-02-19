using Our.Umbraco.AuthU.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbraco.Web.WebApi;
using Umbraco.Core.Services;
using Umbraco.Web;
using Roznamah.Web.ViewModel;

namespace Roznamah.Web.Controllers
{
    [Route("api/[controller]")]
    public class QuestionsController : UmbracoApiController
    {
        private readonly IContentService _contentService;

        public QuestionsController()
        {
            _contentService = Services.ContentService;
        }


        [HttpGet]
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

        [HttpGet]
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