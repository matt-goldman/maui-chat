using MauiChat.Maui.Abstractions;
using MauiChat.Messages.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MauiChat.Messages
{
    public static class DependencyInjection
    {
        public static IServiceCollection UseChatService(this IServiceCollection services)
        {
            services.AddSingleton<IChatService, ChatService>();

            return services;
        }
    }
}
