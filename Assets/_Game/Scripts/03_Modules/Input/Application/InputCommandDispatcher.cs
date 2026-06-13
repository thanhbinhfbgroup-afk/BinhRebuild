using Binh.Modules.Input.Commands;
using Binh.SharedPorts.Input;

namespace Binh.Modules.Input.Application
{
    public sealed class InputCommandDispatcher : IInputCommandSource
    {
        private readonly CommandBuffer _commandBuffer;
        public InputCommandDispatcher(CommandBuffer commandBuffer)
        {
            _commandBuffer = commandBuffer;
        }
        public bool TryDequeue(out ICommand command)
        {
            return _commandBuffer.TryDequeue(out command)
;        
        }
    }
}