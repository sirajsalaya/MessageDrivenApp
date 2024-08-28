using System.Threading.Tasks.Dataflow;
using MessageDrivenApp.Models;

namespace MessageDrivenApp.ProducerConsumer
{

    /** 
    * This service uses Background service for listening the Producer write in Dataflow block. 
    * Consumer's ExecuteAsync method reads and processed the message from the Dataflow block.
    */

    public class Consumer(ILogger<Consumer> logger, ISourceBlock<Message> source) : BackgroundService
    {
        private readonly ISourceBlock<Message> _source = source;
        ILogger<Consumer> _logger = logger;
        public int SuccessCount { get; private set; }
        public int ErrorCount { get; private set; }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (await _source.OutputAvailableAsync(cancellationToken))
            {
                Message? message = null;
                try
                {
                    message = await _source.ReceiveAsync(cancellationToken);
                    SuccessCount++;
                    _logger.LogInformation("Processed: {@message}", message.Content);
                }
                catch (Exception ex)
                {
                    ErrorCount++;
                    _logger.LogError("Error processing message: {message}. Error: {ex.Message}", message?.Content, ex.Message);
                }
            }
        }
    }
}
