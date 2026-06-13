using Binh.SharedPorts.Input;
using Binh.Core.ValueObjects;

namespace Binh.Modules.Input.Commands
{
    public sealed class InteractCommand
    {
        public CommandType Type => CommandType.Interact;
        public InteractCommand(BinhEntityId controlledEntityId)
        {
            ControlledEntityId = controlledEntityId;
        }
        public BinhEntityId ControlledEntityId { get; }
    }
}
