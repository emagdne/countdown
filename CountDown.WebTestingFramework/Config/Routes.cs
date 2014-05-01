namespace CountDown.WebTestingFramework.Config
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    internal static class Routes
    {
        private const string Server = "http://localhost";
        private const string Port = ":50212";
        private const string VirtualDirectory = "/countdown";

        internal const string Home = Server + Port + VirtualDirectory + "/";
        internal const string Login = Server + Port + VirtualDirectory + "/login";
        internal const string Register = Server + Port + VirtualDirectory + "/register";
        internal const string CreateToDo = Server + Port + VirtualDirectory + "/todo/create";
        internal const string CompleteToDo = Server + Port + VirtualDirectory + "/todo/complete";
        internal const string EditToDo = Server + Port + VirtualDirectory + "/todo/edit";
        internal const string UpdateToDo = Server + Port + VirtualDirectory + "/todo/update";
        internal const string EditToDoCancel = Server + Port + VirtualDirectory + "/todo/edit/cancel";
        internal const string DeleteToDo = Server + Port + VirtualDirectory + "/todo/delete";
    }
}
