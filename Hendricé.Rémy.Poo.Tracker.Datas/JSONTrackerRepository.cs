using Hendricé.Rémy.Poo.Tracker.Domains;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hendricé.Rémy.Poo.Tracker.Datas
{
    public class JSONTrackerRepository : ITrackerRepository
    {
        private readonly string _dirLocation;
        private readonly string _usersFileName;
        private readonly string _planningsDirName;

        public JSONTrackerRepository(string dirLocation, string usersFileName, string planningDirName)
        {
            _dirLocation = dirLocation;
            _usersFileName = usersFileName;
            _planningsDirName = planningDirName;
        }

        public IEnumerable<Job> GetUserJobs(string code)
        {
            var aa = new DaySpan(DateTime.Now, DateTime.Now.AddDays(5));
            var bb = new DaySpan(DateTime.Now, DateTime.Now.AddDays(5));
            var a = new Job("AAAAAAAAAAAAAAAA", "A", new Planning("A"), new TimeReport(aa, bb));
            var b = new Job("Bachibouzouk", "B", new Planning("B"), new TimeReport(aa, bb));
            var c = new Job("C", "C", new Planning("C"), new TimeReport(aa, bb));

            var ah = new HashSet<Job>();
            ah.Add(a); ah.Add(b); ah.Add(c);
            return ah;
        }

        public IEnumerable<UserCredentials> GetUsersCredentials()
        {
            try
            {
                return ParseJUserArray();
            }
            catch (IOException ex)
            {
                throw new TrackerRepositoryException($"Impossible d'accéder au fichier {_usersFileName}");
            }
            catch (Exception ex)
            {
                if(ex is JsonReaderException || ex is InvalidCastException || ex is KeyNotFoundException)
                {
                    throw new TrackerRepositoryException($"Fichier {_usersFileName} corrompu");
                }
                else
                {
                    throw;
                }
            }
        }

        private IEnumerable<UserCredentials> ParseJUserArray()
        {
            ISet<UserCredentials> userCreds = new HashSet<UserCredentials>();
            string absPath = getAbsolutePath(_usersFileName);
            string jsonText = File.ReadAllText(absPath);
            JArray usersJson = JArray.Parse(jsonText);
            foreach (JToken obj in usersJson)
            {
                userCreds.Add(ParseJUser(obj));
            }
            return userCreds;
        }

        private UserCredentials ParseJUser(JToken obj)
        {
            JObject jUser = (JObject)obj;
            var codeValue = (JValue)jUser["Code"];
            var passwordValue = (JValue)jUser["Password"];
            string code = codeValue.Value.ToString();
            string password = passwordValue.Value.ToString();
            return new UserCredentials(code, password.ToString());
        }

        private string getAbsolutePath(string dirOrFile)
        {
            string relativPath = $"{_dirLocation}/{dirOrFile}";
            return Path.GetFullPath(relativPath);
        }
    }
}
