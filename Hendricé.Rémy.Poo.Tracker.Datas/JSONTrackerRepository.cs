using Hendricé.Rémy.Poo.Tracker.Domains;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hendricé.Rémy.Poo.Tracker.Datas
{
    public class JSONTrackerRepository : ITrackerRepository
    {
        private readonly string _dirLocation;
        private readonly string _usersFileName;
        private readonly string _planningsDirName;
        private IList<PlanningDTO> _plannings;

        //Pour mise à jour des dates => Objet date dans JobDTO qui partage référence avec Job puis réécriture de tout
        //Ou bien dictionnaire <Job,JobDTO> qui récup jobdto à edit

        public JSONTrackerRepository(string dirLocation, string usersFileName, string planningDirName)
        {
            _dirLocation = dirLocation;
            _usersFileName = usersFileName;
            _planningsDirName = planningDirName;
            _plannings = new List<PlanningDTO>();
        }

        public IEnumerable<Job> GetUserJobs(string user, out string errormessage)
        {
            errormessage = LoadPlannings();
            List<Job> jobs = new List<Job>();
            foreach(PlanningDTO planningDTO in _plannings)
            {
                var planning = new Planning(planningDTO.Name);
                var userJobs = planningDTO.Jobs.Where(j => j.Technician.Equals(user));
                jobs.AddRange(ConvertJobDTOs(planning, userJobs));
            }
            return jobs;
        }

        private IEnumerable<Job> ConvertJobDTOs(Planning planning, IEnumerable<JobDTO> jobDTOs)
        {
            IList<Job> jobs = new List<Job>();
            foreach(JobDTO jobDTO in jobDTOs)
            {
                Job job = new Job(jobDTO.Name, jobDTO.Description, planning, jobDTO.TimeReport);
                jobs.Add(job);
            }
            return jobs;
        }

        private string LoadPlannings()
        {
            IList<string> failedPlannings = new List<string>();
            string planningDirPath = GetAbsolutePath(_planningsDirName);
            try
            {
                string[] filePaths = Directory.GetFiles(planningDirPath);
                foreach(string filePath in filePaths)
                {
                    string path = TryParsePlanning(filePath);
                    if (path != null) failedPlannings.Add(path);
                }
            }
            catch (IOException ex)
            {
                throw new TrackerRepositoryException($"Le dossier {planningDirPath} n'a pas été trouvé");
            }
            return failedPlannings.Count > 0 ? BuildFailedPlanningsMessage(failedPlannings) : null;
        }

        private string BuildFailedPlanningsMessage(IEnumerable<string> failedPaths)
        {
            var sb = new StringBuilder();
            sb.Append($"{failedPaths.Count()} plannings n'ont pas pû être chargés :\n");
            foreach (string path in failedPaths)
            {
                sb.Append($"{path}\n");
            }
            return sb.ToString();
        }

        private string TryParsePlanning(string filePath)
        {
            IList<String> failedPlanning = new List<String>();
            try
            {
                ParsePlanning(filePath);
            }
            catch (Exception ex)
            {
                if(JSONParseExceptionIsHandled(ex))
                {
                    return filePath;
                }
                else
                {
                    throw;
                }
            }
            return null;
        }

        private void ParsePlanning(string filePath)
        {
            string planningJson = File.ReadAllText(filePath);
            JObject jPlanning = JObject.Parse(planningJson);
            var jName = (JValue) jPlanning["Name"];
            var jJobs = (JArray) jPlanning["Jobs"];
            IEnumerable<JobDTO> jobsDTO = ParseJobs(jJobs);
            string name = jName.Value.ToString();
            _plannings.Add(new PlanningDTO(name, filePath, jobsDTO));
        }

        private IEnumerable<JobDTO> ParseJobs(JArray jJobs)
        {
            ISet<JobDTO> jobsDTO = new HashSet<JobDTO>();
            foreach(JToken jJob in jJobs)
            {
                jobsDTO.Add(ParseJob(jJob));
            }
            return jobsDTO;
        }

        private JobDTO ParseJob(JToken jJob)
        {
            JObject jobObject = (JObject)jJob;
            var name = jobObject.GetValue("Name").ToString();
            var description = jobObject.GetValue("Description").ToString();
            var technician = jobObject.GetValue("Technician").ToString();
            var expStart = jobObject.GetValue("ExpStart").ToObject<DateTime>();
            var expEnd = jobObject.GetValue("ExpEnd").ToObject<DateTime>();
            var actStart = ParseActualDate(jobObject.GetValue("ActStart"));
            var actEnd = ParseActualDate(jobObject.GetValue("ActEnd"));
            return new JobDTO(name, description, technician, expStart, expEnd, actStart, actEnd);
        }

        private DateTime ParseActualDate(JToken jDate)
        {
            string dateStr = jDate.ToString();
            DateTime date;
            if(DateTime.TryParse(dateStr, out date))
            {
                return date;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public void WritePlannings()
        {

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
                if(JSONParseExceptionIsHandled(ex))
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
            string absPath = GetAbsolutePath(_usersFileName);
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
            var codeValue = (JValue) jUser["Code"];
            var passwordValue = (JValue) jUser["Password"];
            string code = codeValue.Value.ToString();
            string password = passwordValue.Value.ToString();
            return new UserCredentials(code, password.ToString());
        }

        private string GetAbsolutePath(string dirOrFile)
        {
            string relativPath = $"{_dirLocation}/{dirOrFile}";
            return Path.GetFullPath(relativPath);
        }

        private bool JSONParseExceptionIsHandled(Exception ex)
        {
            return ex is JsonReaderException || ex is InvalidCastException || ex is KeyNotFoundException || ex is NullReferenceException;
        }
    }
}
