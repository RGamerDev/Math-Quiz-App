using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math_Quiz.Models;

namespace Math_Quiz.Context
{
    public static class SessionContext
    {
        public static string username { get; set; }
        public static string password { get; set; }
        public static List<Result> Results { get; set; }
    }
}
