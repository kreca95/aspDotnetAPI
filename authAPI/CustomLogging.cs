using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace authAPI
{
    public static class CustomLogging
    {
        public enum TracingLevel
        {
            ALL, DEBUG, INFO, WARN, ERROR, FATAL, OFF, CACHE
        }

        private static ILog _log = null;
        private static string _logFile = null;
        public static void Initialize(string ApplicationPath)
        {

            _logFile = Path.Combine(ApplicationPath, "App_Data", "AuthAPI.WebService.log");
            GlobalContext.Properties["LogFileName"] = _logFile;

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Path.Combine(ApplicationPath, "Log4Net.config")));

            _log = LogManager.GetLogger("AuthAPI");
        }
        public static string LogFile
        {
            get { return _logFile; }
        }

        public static void LogMessage(TracingLevel Level, string Message)
        {
            switch (Level)
            {
                case TracingLevel.DEBUG:
                    _log.Debug(Message);
                    break;

                case TracingLevel.INFO:
                    _log.Info(Message);
                    break;

                case TracingLevel.WARN:
                    _log.Warn(Message);
                    break;

                case TracingLevel.ERROR:
                    _log.Error(Message);
                    break;

                case TracingLevel.CACHE:
                    _log.Info(Message);
                    break;

                case TracingLevel.FATAL:
                    _log.Fatal(Message);
                    break;
            }
        }

        public static HttpResponseMessage Test()
        {
            string _mess = "Web service is working";

            CustomLogging.LogMessage(TracingLevel.INFO, _mess);

            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent(_mess, System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }
    }
}