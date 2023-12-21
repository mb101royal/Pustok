namespace nov30task.ExternalServices.Interfaces
{
    public interface IEmailService
    {
        void Send(string mailTo, string header, string body, bool isHtml = true);
    }
}
