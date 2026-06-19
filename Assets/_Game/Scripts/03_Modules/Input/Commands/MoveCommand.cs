using Binh.SharedPorts.Input;
using Binh.Core.ValueObjects;

namespace Binh.Modules.Input.Commands
{
    public sealed class MoveCommand : IMoveCommand
    {
        public CommandType Type => CommandType.Move;
        public MoveCommand(BinhEntityId controlledEntityId, float dirX, float dirY)
        {
            ControlledEntityId = controlledEntityId;
            DirX = dirX;
            DirY = dirY;
            IsMoving = DirX != 0 || DirY != 0;
        }
        public BinhEntityId ControlledEntityId { get; }
        public float DirX { get; }
        public float DirY { get; }
        public bool IsMoving { get; }
    }
}
