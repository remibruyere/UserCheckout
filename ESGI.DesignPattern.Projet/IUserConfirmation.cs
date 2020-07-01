namespace ESGI.DesignPattern.Projet
{
    public interface IUserConfirmation
    {
        bool Accepted { get; }

        void AskConfirmation();
    }
}