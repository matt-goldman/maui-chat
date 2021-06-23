using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Chat.Functions
{
    public static class ClearPhotos
    {
        [FunctionName("ClearPhotos")]
        public static async Task Run([TimerTrigger("0 */60 * * * *")]TimerInfo myTimer, ILogger log)
        {
            await StorageHelper.Clear();
        }
    }
}
