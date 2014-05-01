namespace CountDown.WebTestingFramework
{
    public static partial class CountDownApp
    {
        public static class EditToDoPage
        {
            public static class ToDoItem
            {
                public static string Owner
                {
                    get { return Browser.GetText("#todo-edit-owner"); }
                }

                public static bool IsCompleted
                {
                    get { return Browser.GetTextByXpath("//span[@id = 'todo-edit-owner']/following-sibling::span[1]").Equals("Completed!"); }
                }
            }

            public static bool AllFieldsAreDisabled()
            {
                return Browser.ElementsHaveAttributeWithValueByXpath("//input[@type = 'text'] | //textarea | //select",
                    "disabled", "true");
            }

            public static void ClickCancelButton()
            {
                Browser.ClickElementByXpath("//div[@id = 'todo-edit-buttons']/button[contains(text(), 'Cancel')]");
            }

            public static class CancelButton
            {
                public static bool IsVisible
                {
                    get { return !Browser.ElementsHaveCssByXpath("//div[@id = 'todo-edit-buttons']/button[contains(text(), 'Cancel')]", "display", "none"); }
                }
            }

            public static class DeleteButton
            {
                public static bool IsVisible
                {
                    get { return !Browser.ElementsHaveCssByXpath("//div[@id = 'todo-edit-buttons']/button[contains(text(), 'Delete')]", "display", "none"); }
                }
            }

            public static class EditButton
            {
                public static bool IsVisible
                {
                    get { return !Browser.ElementsHaveCssByXpath("//div[@id = 'todo-edit-buttons']/button[contains(text(), 'Edit')]", "display", "none"); }
                }
            }

            public static class SubmitButton
            {
                public static bool IsVisible
                {
                    get { return !Browser.ElementsHaveCssByXpath("//div[@id = 'todo-edit-buttons']/input[@value = 'Submit']", "display", "none"); }
                }
            }
        }
    }
}