using System;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace ST_DowloadJsonFileFromAPI
{
    [Microsoft.SqlServer.Dts.Tasks.ScriptTask.SSISScriptTaskEntryPointAttribute]
    public partial class ScriptMain : Microsoft.SqlServer.Dts.Tasks.ScriptTask.VSTARTScriptObjectModelBase
    {
        public void Main()
        {
            try
            {
                string baseRatesApiUrl = Dts.Variables["User::BaseSourcePath"].Value.ToString();
                string baseFolderPath = Dts.Variables["User::BaseFolderPath"].Value.ToString();
                string guidSuffixName = Dts.Variables["User::GuidSuffixName"].Value.ToString();
                string asOfDateSuffixName = Dts.Variables["User::AsOfDateSuffixName"].Value.ToString();
                string fullFileName = baseFolderPath + guidSuffixName + asOfDateSuffixName;

                WebClient myWebClient = new WebClient();

                Directory.CreateDirectory(baseFolderPath);

                myWebClient.DownloadFile(baseRatesApiUrl, fullFileName);

                Dts.TaskResult = (int)ScriptResults.Success;
            }
            catch (Exception e)
            {
                Dts.Events.FireError(500, "", e.Message, "", 0);
                Dts.TaskResult = (int)ScriptResults.Failure;
            }
        }

        enum ScriptResults
        {
            Success = Microsoft.SqlServer.Dts.Runtime.DTSExecResult.Success,
            Failure = Microsoft.SqlServer.Dts.Runtime.DTSExecResult.Failure
        };
    }
}