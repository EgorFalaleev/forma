namespace Forma.Runtime.Services.Input
{
    public abstract class BaseInputService
    {
        protected readonly InputActions InputActions;

        protected BaseInputService(InputActions inputActions)
        {
            InputActions = inputActions;
        }

        public abstract void Enable();
        public abstract void Disable();
    }
}