using Math_Quiz.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.ApplicationModel;

namespace Math_Quiz.Data
{
    class PeopleRepository
    {
        string Xmlfilepath = Path.Combine(Package.Current.InstalledLocation.Path, @"Data\Accounts.xml");
        private XDocument xdoc;
        public IEnumerable<Person> People;

        public PeopleRepository()
        {
            //TODO: Get linq working
            xdoc = XDocument.Load(Xmlfilepath);

            People =
                from account
                in xdoc.Descendants("account")
                select new Person(
                    (string)account.Element("username"),
                    (string)account.Element("password"),
                    account.Descendants("results")
                    );
                    
        }

        public Person GetPerson(Person p)
        {
            return People.FirstOrDefault(x => x.Username == p.Username);
        }

        public bool canLogin(Person p)
        {
            return People.FirstOrDefault(x => x.Username == p.Username && x.Password == p.Password) == null ? false : true;
        }

        public bool canRegister(Person p)
        {
            return People.FirstOrDefault(x => x.Username == p.Username && x.Password == p.Password) == null ? true : false;
        }

        public void Register(Person p)
        {
            xdoc.Root.Add(
                new XElement("account",
                    new XElement("username", p.Username),
                    new XElement("password", p.Password)
                    ));

            xdoc.Save(Xmlfilepath);
        }
    }
}
