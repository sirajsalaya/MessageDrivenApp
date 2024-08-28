using System.Threading.Tasks.Dataflow;
using MessageDrivenApp.Models;

namespace MessageDrivenApp.Interfaces
{
    public interface IProducer
    {
        void Produce(ITargetBlock<Message> target, Message message);
    }
}