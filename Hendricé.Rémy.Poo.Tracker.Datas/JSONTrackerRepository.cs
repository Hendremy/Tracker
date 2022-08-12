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
        private readonly string _dirLocation = "../../../../../json";
        private readonly string _usersFileName = "users.json";
        private readonly string _planningsDirName = "plannings";

        public IEnumerable<Job> GetUserJobs(string code)
        {
            throw new TrackerRepositoryException("Can't find users");
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
