using System;
using System.Collections.Generic;
using System.Data.CData.Twitter;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CData.AzureFunctions.KintonToTweetBot
{
    internal class CdataTwitterManager
    {
        private string twitterConnectionString;

        public CdataTwitterManager(string twitterConnectionString)
        {
            this.twitterConnectionString = twitterConnectionString;
        }

        internal void Tweet(string message)
        {
            using (TwitterConnection connection = new TwitterConnection(this.twitterConnectionString))
            {
                int rowsAffected;
                TwitterCommand cmd = new TwitterCommand($"INSERT INTO Tweets (Text) VALUES ('{message}')", connection);
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
    }
