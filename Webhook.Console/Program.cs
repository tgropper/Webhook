namespace Webhook.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IHook hook = new Hook();
            hook.Notify("notes-es", queryString: new { ids = new string[] { "asd", "qwe" } });
        }
    }
}