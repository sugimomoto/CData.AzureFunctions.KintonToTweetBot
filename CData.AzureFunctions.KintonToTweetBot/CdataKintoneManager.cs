using System;
using System.Collections.Generic;
using System.Data;
using System.Data.CData.Kintone;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CData.AzureFunctions.KintonToTweetBot
{
    internal class CdataKintoneManager
    {
        /// <summary>
        /// 
        /// </summary>
        private string kintoneConnectionString;

        /// <summary>
        /// 
        /// </summary>
        private string twitterMessageFieldName = "TwitterMessage";

        /// <summary>
        /// 
        /// </summary>
        private string twitterTableName = "CDataTweetList";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="kintoneConnectionString"></param>
        public CdataKintoneManager(string kintoneConnectionString)
        {
            this.kintoneConnectionString = kintoneConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal int GetRandomId()
        {
            var result = GetDataTable($"SELECT RecordId FROM {this.twitterTableName}");

            var target = new Random().Next(0, result.Rows.Count - 1);

            return (int)result.Rows[target]["RecordId"];
        }

        /// <summary>
        /// 対象アプリのレコード数をカウント
        /// </summary>
        /// <returns></returns>
        internal int GerRecordsCount()
        {
            var result = GetDataTable($"SELECT COUNT(RecordId) as COUNT FROM {this.twitterTableName}");
            var count = (int)result.Rows[0]["COUNT"];

            return count;
        }

        /// <summary>
        /// 対象IDのレコードを取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal string GetMessage(int id)
        {
            var result = GetDataTable($"SELECT {this.twitterMessageFieldName} FROM {this.twitterTableName} Where RecordId = {id}");
            return result.Rows[0][this.twitterMessageFieldName] as String;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataTable GetDataTable(string sql)
        {
            var datatable = new DataTable();

            using (var connection = new KintoneConnection(this.kintoneConnectionString))
            {
                var dataAdapter = new KintoneDataAdapter(sql, connection);
                dataAdapter.Fill(datatable);
            }

            return datatable;
        }
    }
}
