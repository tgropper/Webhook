namespace Webhook.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            log4net.ILog log;
            log4net.Config.BasicConfigurator.Configure();
            log = log4net.LogManager.GetLogger("Webhook");

            IHook hook = new Hook(onError: ex => log.Error(ex.Message));
            hook.NotifyAsync("notes-es", queryString: new { ids = new string[] { "asd", "qwe" } });

            System.Console.ReadKey();
        }
    }
}