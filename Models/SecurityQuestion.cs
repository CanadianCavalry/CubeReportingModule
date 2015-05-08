using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CubeReportingModule.Models
{
    [Table("SecurityQuestions")]
    public class SecurityQuestion
    {
        [Key]
        public string Question { get; set; }
        public string Answer { get; set; }

        //public SecurityQuestion(string inQuestion, string inAnswer)
        //{
        //    question = inQuestion;
        //    answer = inAnswer;
        //}

        //public string Question
        //{
        //    get { return question; }
        //}

        public bool verifyAnswer(string providedAnswer)
        {
            bool verified = Answer == providedAnswer;
            return verified;
        }
    }
}