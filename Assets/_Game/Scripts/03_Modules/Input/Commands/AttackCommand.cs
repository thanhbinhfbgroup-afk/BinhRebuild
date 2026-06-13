using Binh.SharedPorts.Input;
using Binh.Core.ValueObjects;

namespace Binh.Modules.Input.Commands
{
    public sealed class AttackCommand
    {
        public CommandType Type => CommandType.Attack;
        public AttackCommand(BinhEntityId controlledEntityId, bool isHeld, float heldDuration)
        {
            ControlledEntityId = controlledEntityId;
            IsHeld = isHeld;
            HeldDuration = heldDuration;
        }
        public BinhEntityId ControlledEntityId { get; }
        public bool IsHeld { get; }
        public float HeldDuration { get; }
    }
}
