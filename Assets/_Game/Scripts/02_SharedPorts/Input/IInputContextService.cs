namespace Binh.SharedPorts.Input
{
    public interface IInputContextService
    {
        void SwitchContext(InputContext targetContext);
        bool WasSubmitPressedThisFrame();
    }
}