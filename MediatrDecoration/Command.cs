using MediatR;

namespace MediatrDecoration
{
    public class Command :IRequest<string>
    {
        
    }

    public class CommandHandler : IRequestHandler<Command, string>
    {
        public string Handle(Command message)
        {
            return "Inner";
        }
    }
}