#region - Imports
using PontoTelRpa.Domain.Models;
using Microsoft.Extensions.Configuration;
#pragma warning disable CS8601, CS8604, CS8618
#endregion

namespace PontoTelRpa.Domain.Rules
{
    public class DateRules
    {
        private string StartJourney { get; set; }
        private string EndJourney { get; set; }
        private string LimitToLunch { get; set; }
        private int ToleranceMinute {get;set;}

        public DateRules(IConfiguration config)
        {
            StartJourney = config["StartJourney"];
            EndJourney = config["EndJourney"];
            LimitToLunch = config["LimitToLunch"];
            ToleranceMinute = int.Parse(config["ToleranceMinute"]);
        }

        public EProcessType GetPoint(DateTime receivedDate)
        {
            DateTime dateNow = DateTime.Now;

            if (receivedDate.AddMinutes(ToleranceMinute) > dateNow)
                if (VerifyStartJourneyTime(dateNow, StartJourney))
                    return EProcessType.FreePoint;
                else if (VerifyStartJourneyTime(dateNow, LimitToLunch))
                    return EProcessType.LunchTime;
                else if (VerifyStartJourneyTime(dateNow, EndJourney))
                    return EProcessType.FreePoint;
                else
                    return EProcessType.OutOfTime;
            else
                return EProcessType.OutOfTime;
        }

        private bool VerifyStartJourneyTime(DateTime dateNow, string journeyVariant) =>
            dateNow >= Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd " + journeyVariant))
                    .AddMinutes(-ToleranceMinute)
                && dateNow <= Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd " + journeyVariant))
                    .AddMinutes(ToleranceMinute);
    }
}