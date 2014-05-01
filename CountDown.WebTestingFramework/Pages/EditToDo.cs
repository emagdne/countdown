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
                    get { return Browser.GetText("//span[@id = 'todo-edit-owner']/following-sibling::span[1]", QueryMethod.Xpath).Equals("Completed!"); }
                }
            }

            public static bool AllFieldsAreDisabled()
            {
                return Browser.ElementsHaveAttributeWithValue("//input[@type = 'text'] | //textarea | //select",
                    "disabled", "true", QueryMethod.Xpath);
            }

            public static void ClickCancelButton()
            {
                Browser.ClickElement("//div[@id = 'todo-edit-buttons']/button[contains(text(), 'Cancel')]", QueryMethod.Xpath);
            }

            public static class CancelButton
            {
                public static bool IsVisible
                {
                    get { return !Browser.ElementsHaveCss("//div[@id = 'todo-edit-buttons']/button[contains(text(), 'Cancel')]", "display", "none", QueryMethod.Xpath); }
                }
            }

            public static class DeleteButton
            {
                public static bool IsVisible
                {
                    get { return !Browser.ElementsHaveCss("//div[@id = 'todo-edit-buttons']/button[contains(text(), 'Delete')]", "display", "none", QueryMethod.Xpath); }
                }
            }

            public static class EditButton
            {
                public static bool IsVisible
                {
                    get { return !Browser.ElementsHaveCss("//div[@id = 'todo-edit-buttons']/button[contains(text(), 'Edit')]", "display", "none", QueryMethod.Xpath); }
                }
            }

            public static class SubmitButton
            {
                public static bool IsVisible
                {
                    get { return !Browser.ElementsHaveCss("//div[@id = 'todo-edit-buttons']/input[@value = 'Submit']", "display", "none", QueryMethod.Xpath); }
                }
            }
        }
    }
}