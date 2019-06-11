using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using GWebsite.AbpZeroTemplate.Web.Core.ScanReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core
{

    public class Scan : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        
        public Scan(AbpTimer timer)
            : base(timer)
        {
            Timer.Period = 5000; //5 seconds (good for tests, but normally will be more)
            Timer.RunOnStart = true;
        }

        protected override void DoWork()
        {
            string url = "http://localhost:5000/api/ScanReport/Scan/";

            WebRequest myReq = WebRequest.Create(url);

            myReq.Headers["Authorization"] = "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjMiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJBc3BOZXQuSWRlbnRpdHkuU2VjdXJpdHlTdGFtcCI6IjVBNU9DQTU1S08zS0o2Q0pKR0lTTDRVV0dKTk00NkI3IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJodHRwOi8vd3d3LmFzcG5ldGJvaWxlcnBsYXRlLmNvbS9pZGVudGl0eS9jbGFpbXMvdGVuYW50SWQiOiIyIiwic3ViIjoiMyIsImp0aSI6IjBhYmJmZDA3LWM1NmYtNGIwOC04ZGRkLTU1OWVmYWQyMTMzYSIsImlhdCI6MTU2MDIzNzQ5OSwibmJmIjoxNTYwMjM3NDk5LCJleHAiOjE1NjAzMjM4OTksImlzcyI6IkFicFplcm9UZW1wbGF0ZSIsImF1ZCI6IkFicFplcm9UZW1wbGF0ZSJ9.PJE81FJ87YCmSxwjqx8w5mvWb5gU-PFRKcSxbMC3gs8";

            myReq.Headers["accept"] = "text/plain";

            WebResponse wr = myReq.GetResponse();

            Console.WriteLine("Scanning now !!!");
        }
    }
}
