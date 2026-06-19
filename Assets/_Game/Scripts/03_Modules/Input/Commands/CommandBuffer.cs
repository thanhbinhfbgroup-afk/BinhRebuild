using Binh.SharedPorts.Input;
using System.Collections.Generic;

namespace Binh.Modules.Input.Commands
{
    public sealed class CommandBuffer
    {
        private readonly Queue<ICommand> _commands;
        public CommandBuffer(int capacity)
        {
            _commands = new Queue<ICommand>(capacity);
        }
        public void Enqueue(ICommand command)
        {
            _commands.Enqueue(command);
        }
        public bool TryDequeue(out ICommand command)
        {
            if (_commands.Count == 0)
            {
                command = null;
                return false;
            }
            command = _commands.Dequeue();
            return true;
        }
        public void Clear()
        {
            _commands.Clear();
        }

    }
}
