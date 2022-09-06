using Hendricé.Rémy.Poo.Tracker.Domains;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Datas
{
    public class JSONUserParser : IParseUser
    {

        public IEnumerable<UserCredentials> ParseArray(string jsonString)
        {
            ISet<UserCredentials> userCreds = new HashSet<UserCredentials>();
            JArray usersJson = JArray.Parse(jsonString);
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
    }
}
