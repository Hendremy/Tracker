using Hendricé.Rémy.Poo.Tracker.Domains;
using Newtonsoft.Json;
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
        private ISerializePlanning _planningSerializer;
        private IParseUser _userParser;
        private IDictionary<PlanningDTO,string> _plannings;

        public JSONTrackerRepository(ISerializePlanning planningParser, IParseUser userParser, string dirLocation, string usersFileName, string planningDirName)
        {
            _planningSerializer = planningParser;
            _userParser = userParser;
            _dirLocation = dirLocation;
            _usersFileName = usersFileName;
            _planningsDirName = planningDirName;
        }

        public IEnumerable<Job> GetUserJobs(string user, out string errormessage)
        {
            _plannings = new Dictionary<PlanningDTO, string>();
            errormessage = LoadPlannings();
            List<Job> jobs = new List<Job>();
            foreach(PlanningDTO planningDTO in _plannings.Keys)
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
            return failedPlannings.Count > 0 ? BuildFailedPlanningsMessage(failedPlannings," n'ont pas pû être chargés") : null;
        }

        private string BuildFailedPlanningsMessage(IEnumerable<string> failedPaths, string message)
        {
            var sb = new StringBuilder();
            sb.Append($"Erreur - {failedPaths.Count()} {message} :\n");
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
            var planningDTO = _planningSerializer.Deserialize(planningJson);
            _plannings.Add(planningDTO, filePath);
        }

        private void WritePlannings()
        {
            if (_plannings == null) return;
            var failedPaths = new List<String>();
            foreach(var entry in _plannings)
            {
                TryWritePlanning(entry.Value, entry.Key, failedPaths);
            }
            if(failedPaths.Count > 0)
            {
                throw new TrackerRepositoryException(BuildFailedPlanningsMessage(failedPaths," n'ont pas pû être sauvegardés"));
            }
        }

        private void TryWritePlanning(string filePath, PlanningDTO planning, IList<string> failedPaths)
        {
            string planningJson = _planningSerializer.Serialize(planning);
            try
            {
                using var fileStream = File.Open(filePath, FileMode.Create);
                using StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(planningJson);
            }
            catch (Exception ex)
            {
                if(ex is IOException || ex is UnauthorizedAccessException)
                failedPaths.Add(filePath);
            }
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
            string absPath = GetAbsolutePath(_usersFileName);
            string jsonText = File.ReadAllText(absPath);
            return _userParser.ParseArray(jsonText);
        }

        private string GetAbsolutePath(string dirOrFile)
        {
            string relativPath = $"{_dirLocation}/{dirOrFile}";
            return Path.GetFullPath(relativPath);
        }

        private bool JSONParseExceptionIsHandled(Exception ex)
        {
            return ex is JsonReaderException || ex is InvalidCastException 
                || ex is KeyNotFoundException || ex is NullReferenceException
                || ex is FormatException;
        }

        public void Dispose()
        {
            WritePlannings();
        }
    }
}
