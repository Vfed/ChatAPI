namespace ChatAPI.Servises.Specific
{
    public interface ILocaLDataService
    {
        int GetUsersOnline();
        void SetUsersOnline(int val);
    }
}