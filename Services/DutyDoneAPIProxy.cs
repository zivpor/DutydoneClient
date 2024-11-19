using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutydoneClient.Services
{
    public class DutyDoneAPIProxy
    {
        private static string serverIP = "v8tc37xc-5132.euw.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://v8tc37xc-5132.euw.devtunnels.ms/api/";
        private static string ImageBaseAddress = "https://v8tc37xc-5132.euw.devtunnels.ms/";

        public DutyDoneAPIProxy()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }


    }
}
