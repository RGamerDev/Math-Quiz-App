using Math_Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Math_Quiz.Data
{
    public class QuestionsRepository
    {
        public List<Question> questions { get; set; }
        public string oper;
        public int range;

        public QuestionsRepository(Options options)
        {
            range = options.Difficulty * 25;
            if (options.NumberOfQuestions != -1)
            {
                questions = new List<Question>();
                CreateQuestionList(options);
                questions = ShuffleList<Question>(questions);
            }
        }

        private void CreateQuestionList(Options opt)
        {

            for (int i = 0; i < opt.NumberOfQuestions; i++)
            {
                questions.Add(CreateQuestion(opt));
            }
        }

        public Question CreateQuestion(Options opt)
        {
            Random random = new Random();
            int leftOperand = random.Next(0, range);
            int rightOperand = random.Next(0, range);
            int correctAnswer;

            switch (opt.Grade)
            {
                case 1:
                    oper = "+";
                    correctAnswer = leftOperand + rightOperand;
                    break;
                case 2:
                    oper = "-";
                    correctAnswer = leftOperand - rightOperand;
                    break;
                case 3:
                    oper = "*";
                    correctAnswer = leftOperand * rightOperand;
                    break;
                case 4:
                    oper = "/";
                    correctAnswer = leftOperand / rightOperand;
                    break;
                default:
                    oper = "error";
                    correctAnswer = 0;
                    break;
            }

            return new Question($"What is {leftOperand} {oper} {rightOperand}?", ShuffleList<string>(new List<string>()
                {
                    random.Next(correctAnswer - 5, correctAnswer + 5).ToString(),
                    random.Next(correctAnswer - 5, correctAnswer + 5).ToString(),
                    correctAnswer.ToString(),
                    random.Next(correctAnswer - 5, correctAnswer + 5).ToString(),
                }),
                    correctAnswer.ToString()
                );;
        }

        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }
    }
}
