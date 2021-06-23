using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace Chat.Functions
{
    public static class GetSignalRInfo
    {
        [FunctionName("GetSignalRInfo")]
        public static SignalRConnectionInfo Run(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "maui-chat")]
            SignalRConnectionInfo info)
        {
            return info;
        }
    }
}
