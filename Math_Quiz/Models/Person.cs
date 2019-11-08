using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.ApplicationModel;

namespace Math_Quiz.Models
{
    public class Person
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Result> Results { get; set; }


        public Person(string username, string password, IEnumerable<XElement> results)
        {
            this.Username = username;
            this.Password = password;
            this.Results = new List<Result>();

            var elements = results.Elements();


            foreach (var element in elements)
            {
                var nodes = element.Elements();
                this.Results.Add(new Result()
                {
                    Percentage = nodes.ElementAt(0).Value.ToString() + "%",
                    Competency = nodes.ElementAt(1).Value.ToString(),
                    Grade = nodes.ElementAt(2).Value.ToString(),
                    Difficulty = nodes.ElementAt(3).Value.ToString()
                });
            }
        }

        public Person(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public Person(string username)
        {
            this.Username = username;
        }
    }
}
