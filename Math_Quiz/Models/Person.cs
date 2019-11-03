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


        public Person(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            this.Results = new List<Result>();
        }

        public Person(string username)
        {
            this.Username = username;
        }


        public IEnumerable<Result> GetResults(XDocument doc)
        {
            //TODO: Getresults
            //var results = from query 
            //              in doc.Descendants("results")
            //              where doc.Descendants("")
            //              select new Result()
            //              {
            //                  Competency = (string)query,
            //                  Percentage = (string)query.
            //              }
            //this.Results.Add(new Result()
            //{
            //    Percentage = $"{result}%",
            //    Competency = result
            //});

            //return this.Results;

        }
    }
}
