using Bliss.DTO;
using Bliss.Models;
using Bliss.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Bliss.Controllers
{
    public class QuestionsController : ApiController
    {
        //private const int NUM_RESULTS = 50;

        public List<Questions> GetQuestions(int limit = 10, int offset = 0, string filter = "")
        {
            List<questions> questions;
            using (var db = new RecruitmentEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                if (filter != null && filter.Length > 0)
                {
                    questions = db.questions.Where(q => q.question.Contains(filter)).Include("questions_choices.choices").OrderBy(q => q.id).Skip(offset).Take(limit).ToList();
                }
                else
                {
                    questions = db.questions.Include("questions_choices.choices").OrderBy(q=>q.id).Skip(offset).Take(limit).ToList();
                }
            }
            return QuestionsReturn(questions);
        }

        public IHttpActionResult GetQuestion(int question_id)
        {


            try
            {
                using (var db = new RecruitmentEntities())
                {
                    var question = db.questions.FirstOrDefault((p) => p.id == question_id);
                    if (question == null)
                    {
                        return NotFound();
                    }
                    return Ok(question);
                }
            } catch (Exception e)
            {
                return BadRequest();
            }
        }

        public bool PostQuestion(HttpRequestMessage values)
        {
            using (var db = new RecruitmentEntities())
            {
                List<questions> questions = db.questions.ToList();
                questions original;
                questions question = new Models.questions();
                var data = values.Content.ReadAsStringAsync().Result;
                question = JsonConvert.DeserializeObject<questions>(data);
                if (question.id>0) //UPDATE
                {
                    original = questions.FirstOrDefault((p) => p.id == question.id);
                    if (original != null)
                    {

                        db.Entry(original).CurrentValues.SetValues(question);
                        var addedChoices = Comparers.Except(question.questions_choices, original.questions_choices, c => c.choiceID);
                        var deletedChoices = Comparers.Except(original.questions_choices, question.questions_choices, c => c.choiceID);
                        var modifiedChoices = Comparers.Except(question.questions_choices, addedChoices, c => c.choiceID);
                        addedChoices.ToList<questions_choices>().ForEach(ac => ac.questionID = original.id);
                        addedChoices.ToList<questions_choices>().ForEach(ac => db.Entry(ac).State = EntityState.Added);
                        deletedChoices.ToList<questions_choices>().ForEach(dc => db.Entry(dc).State = EntityState.Deleted);
                        foreach (questions_choices qc in modifiedChoices)
                        {
                            var existingChoice = db.questions_choices.Find(qc.questionID, qc.choiceID);
                            db.Entry(existingChoice).CurrentValues.SetValues(qc);
                        }
                
                    }
                    else
                    {
                        return false;
                    }
                }
                else //INSERT
                {
                    original = new questions
                    {
                        question = question.question,
                        image_url = question.image_url,
                        thumb_url = question.thumb_url,
                        published_at = DateTime.Now,
                        questions_choices = question.questions_choices
                    };
                
                    db.questions.Add(original);
                }

                try
                {
                   

                    db.SaveChanges();
                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        private List<Questions> QuestionsReturn(List<questions> result)
        {
            List<Questions> questionsList = new List<Questions>();
            for (int idx = 0; idx < result.Count; idx++)
            {
                Questions q = new Questions
                {
                    id = result[idx].id,
                    question = result[idx].question,
                    image_url = result[idx].image_url,
                    thumb_url = result[idx].thumb_url,
                    published_at = result[idx].published_at,
                    questions_choices = new List<Choices>()

                };
                foreach(questions_choices qc in result[idx].questions_choices)
                {
                    Choices choice = new Choices
                    {
                        choices = qc.choices.choices1,
                        id = qc.choices.id
                    };
                    q.questions_choices.Add(choice);
                }
                
                questionsList.Add(q);
            }


            return questionsList;
        }
    }
}
