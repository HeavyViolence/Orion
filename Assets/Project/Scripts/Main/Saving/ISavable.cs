namespace Orion.Main.Saving
{
    public interface ISavable
    {
        string StateName { get; }

        string GetState();
        void SetState(string state);
    }
}