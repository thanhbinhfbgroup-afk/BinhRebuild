using Binh.Core.ValueObjects;

namespace Binh.SharedPorts.Input
{
    public interface ICommand
    {
        CommandType Type { get; }
        BinhEntityId ControlledEntityId { get; }

    }
}