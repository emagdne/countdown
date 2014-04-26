namespace CountDown.WebTestingFramework
{
    public static partial class CountDownApp
    {
        public static class HomePage
        {
            public static bool HasCreateToDoLink()
            {
                return Browser.HasElement("#index-create-link");
            }

            public static void ClickCreateToDoLink()
            {
                Browser.ClickElement("#index-create-link");
            }
        }
    }
}
