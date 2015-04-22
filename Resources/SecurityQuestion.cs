using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Resources
{
    public class SecurityQuestion
    {
        public readonly string question { get; set; }
        public readonly string answer { get; set; }

        public SecurityQuestion(string inQuestion, string inAnswer)
        {
            question = inQuestion;
            answer = inAnswer;
        }

        public bool verifyAnswer(string providedAnswer)
        {
            bool verified = answer == providedAnswer;
            return verified;
        }
    }
}