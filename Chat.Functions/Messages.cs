using MauiChat.Messages;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chat.Functions
{
    public class Messages
    {
        [FunctionName("Messages")]
        public static async Task SendMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] object message,
            [SignalR(HubName = "maui-chat")] IAsyncCollector<SignalRMessage> signalrMessages)
        {
            var jsonObject = (JObject)message;
            var msg = jsonObject.ToObject<Message>();

            if(msg.TypeInfo.Name == nameof(PhotoMessage))
            {
                var photoMessage = jsonObject.ToObject<PhotoMessage>();
                var bytes = Convert.FromBase64String(photoMessage.Base64Photo);

                var stream = new MemoryStream(bytes);

                var subscriptionKey = Environment.GetEnvironmentVariable("ComputerVisionKey");
                
                var computerVision = new ComputerVisionClient(
                    new ApiKeyServiceClientCredentials(subscriptionKey), 
                    new DelegatingHandler[] { });

                computerVision.Endpoint = Environment.GetEnvironmentVariable("ComputerVisionEndpoint");

                var features = new List<VisualFeatureTypes?>() 
                { 
                    VisualFeatureTypes.Adult
                };

                var result = await computerVision.AnalyzeImageInStreamAsync(stream, features);

                if (result.Adult.IsAdultContent)
                {
                    return;
                }

                var url = await StorageHelper.Upload(bytes, photoMessage.FileEnding);

                msg = new PhotoUrlMessage(photoMessage.Username)
                {
                    Id = photoMessage.Id,
                    Timestamp = photoMessage.Timestamp,
                    Url = url
                };

                await signalrMessages.AddAsync(new SignalRMessage
                {
                    Target = "newMessage",
                    Arguments = new[] { message }
                });

                return;
            }

            await signalrMessages.AddAsync(new SignalRMessage
            {
                Target = "newMessage",
                Arguments = new[] { message }
            });
        }
    }
}
