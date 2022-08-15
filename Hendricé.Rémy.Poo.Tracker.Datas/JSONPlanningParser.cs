using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Datas
{
    public class JSONPlanningParser : IParsePlanning
    {
        public PlanningDTO Parse(string planningJson)
        {
            return ParsePlanning(planningJson);
        }

        private PlanningDTO ParsePlanning(string planningJson)
        {
            JObject jPlanning = JObject.Parse(planningJson);
            var jName = (JValue)jPlanning["Name"];
            var jJobs = (JArray)jPlanning["Jobs"];
            string name = jName.Value.ToString();
            IEnumerable<JobDTO> jobsDTO = ParseJobs(jJobs);
            return new PlanningDTO(name, jobsDTO);
        }

        private IEnumerable<JobDTO> ParseJobs(JArray jJobs)
        {
            ISet<JobDTO> jobsDTO = new HashSet<JobDTO>();
            foreach (JToken jJob in jJobs)
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
            var expStart = ParseExpectedDate(jobObject.GetValue("ExpStart"));
            var expEnd = ParseExpectedDate(jobObject.GetValue("ExpEnd"));
            var actStart = ParseActualDate(jobObject.GetValue("ActStart"));
            var actEnd = ParseActualDate(jobObject.GetValue("ActEnd"));
            return new JobDTO(name, description, technician, expStart, expEnd, actStart, actEnd);
        }

        private bool TryParseDate(JToken jDate, out DateTime date)
        {
            string dateStr = jDate.ToString();
            if (DateTime.TryParse(dateStr, out date))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private DateTime ParseExpectedDate(JToken jDate)
        {
            DateTime outDate;
            if (TryParseDate(jDate, out outDate))
            {
                return outDate;
            }
            else
            {
                throw new FormatException("Date non conforme");
            }
        }

        private DateTime ParseActualDate(JToken jDate)
        {
            DateTime outDate;
            if(TryParseDate(jDate, out outDate))
            {
                return outDate;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
    }
}
