using Binh.SharedPorts.Input;
using Binh.Core.ValueObjects;

namespace Binh.Modules.Input.Commands
{
    public sealed class SwitchContextCommand
    {
        public CommandType Type => CommandType.SwitchContext;
        public SwitchContextCommand(BinhEntityId controlledEntityId, InputContext targetContext)
        {
            ControlledEntityId = controlledEntityId;
            TargetContext = targetContext;
        }
        public BinhEntityId ControlledEntityId { get; }
        public InputContext TargetContext { get; }
    }
}
