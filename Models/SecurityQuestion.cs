using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Models
{
    public class SecurityQuestion
    {
        private readonly string question;
        private readonly string answer;

        public SecurityQuestion(string inQuestion, string inAnswer)
        {
            question = inQuestion;
            answer = inAnswer;
        }

        public string Question
        {
            get { return question; }
        }

        public bool verifyAnswer(string providedAnswer)
        {
            bool verified = answer == providedAnswer;
            return verified;
        }
    }
}