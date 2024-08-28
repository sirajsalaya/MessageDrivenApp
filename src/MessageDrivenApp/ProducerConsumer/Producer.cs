using System.Threading.Tasks.Dataflow;
using MessageDrivenApp.Interfaces;
using MessageDrivenApp.Models;

namespace MessageDrivenApp.ProducerConsumer
{
    /** 
    * This service uses .Net Dataflow (Task Parallel Library) for writing and reading
    * messages from a dataflow block, which are synchronous and asynchronous
    * Producer's Produce method write the message to the Dataflow block.
    */

    public class Producer : IProducer
    {
        public void Produce(ITargetBlock<Message> target, Message message)
        {
            target.Post(message);
        }
    }
}
