using System.Threading.Tasks.Dataflow;
using MessageDrivenApp.Models;

namespace MessageDrivenApp.ProducerConsumer
{
    // Extension method to register Producers and Consumers Services in DI
    public static class ProducerConsumerExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<Producer>();
            services.AddSingleton<Consumer>();
            services.AddSingleton<ITargetBlock<Message>>(new BufferBlock<Message>());
            services.AddSingleton<ISourceBlock<Message>>(provider => (ISourceBlock<Message>)provider.GetRequiredService<ITargetBlock<Message>>());
            services.AddHostedService<Consumer>(provider => provider.GetService<Consumer>());
        }
    }
}
