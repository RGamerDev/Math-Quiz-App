using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Quiz.Models
{
    public class Question
    {
        public int Id { get; set; }
        public static int id { get; set; } = 1;
        public string Description { get; set; }
        public List<string> Choices { get; set; }
        public string CorrectAnswer { get; set; }

        public Question(string desc, List<string> c, string cor)
        {
            this.Id = id++;
            this.Description = desc;
            this.Choices = c;
            this.CorrectAnswer = cor;
        }
    }
}
