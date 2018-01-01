using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Configuration;

namespace CData.AzureFunctions.KintonToTweetBot
{
    public static class Main
    {
        [FunctionName("Main")]
        public static void Run([TimerTrigger("0 15 10 ? * MON-FRI")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                var kintoneConnectionString = ConfigurationManager.AppSettings["kintoneConnectionString"] as String;
                var twitterConnectionString = ConfigurationManager.AppSettings["twitterConnectionString"] as String;

                var cdataKintoneManager = new CdataKintoneManager(kintoneConnectionString);
                var cdataTwitterManager = new CdataTwitterManager(twitterConnectionString);

                var targetId = cdataKintoneManager.GetRandomId();
                var message = cdataKintoneManager.GetMessage(targetId);

                cdataTwitterManager.Tweet(message);
            }
            catch (Exception ex)
            {

                log.Error(ex.Message);
            }
        }
    }
}
