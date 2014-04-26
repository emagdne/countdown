using System.Collections.Generic;

namespace CountDown.WebTestingFramework
{
    public static partial class CountDownApp
    {
        public static class CreateToDoPage
        {
            public static class TitleField
            {
                public static string ErrorMessage
                {
                    get { return Browser.GetText("span[data-valmsg-for=Title]"); }
                }

                public static void Fill(string title)
                {
                    Browser.FillInputField("input[name=title]", title);
                }
            }

            public static class DescriptionField
            {
                public static string ErrorMessage
                {
                    get { return Browser.GetText("span[data-valmsg-for=Description]"); }
                }

                public static void Fill(string desc)
                {
                    Browser.FillInputField("textarea[name=description]", desc);
                }
            }

            public static class StartDateField
            {
                public static string Value
                {
                    get { return Browser.GetInputValue("#todo-create-start-date"); }
                }

                public static string ErrorMessage
                {
                    get { return Browser.GetText("span[data-valmsg-for=StartDate]"); }
                }

                public static void Fill(string startDate)
                {
                    Browser.FillInputField("input[name=startDate]", startDate);
                }

                public static void Clear()
                {
                    Browser.ClearInputField("input[name=startDate]");
                }
            }

            public static class StartTimeField
            {
                public static string Value
                {
                    get { return Browser.GetInputValue("#todo-create-start-time"); }
                }

                public static string ErrorMessage
                {
                    get { return Browser.GetText("span[data-valmsg-for=StartTime]"); }
                }

                public static void Fill(string startTime)
                {
                    Browser.FillInputField("input[name=startTime]", startTime);
                }

                public static void Clear()
                {
                    Browser.ClearInputField("input[name=startTime]");
                }
            }

            public static class DueDateField
            {
                public static string ErrorMessage
                {
                    get { return Browser.GetText("span[data-valmsg-for=DueDate]"); }
                }

                public static void Fill(string dueDate)
                {
                    Browser.FillInputField("input[name=dueDate]", dueDate);
                }
            }

            public static class DueTimeField
            {
                public static string ErrorMessage
                {
                    get { return Browser.GetText("span[data-valmsg-for=DueTime]"); }
                }

                public static void Fill(string dueTime)
                {
                    Browser.FillInputField("input[name=dueTime]", dueTime);
                }
            }

            public static class AssignToDropdown
            {
                public static string Value
                {
                    get { return Browser.GetSelectedDropdownOption("#todo-create-assign"); }
                }

                public static List<string> Options
                {
                    get { return Browser.GetDropdownOptions("#todo-create-assign"); }
                } 

                public static void Select(string text)
                {
                    Browser.SelectDropdownOption("#todo-create-assign", text);
                }
            }

            public static void ClickCancel()
            {
                Browser.ClickElement("#todo-create-cancel");
            }

            public static void ClickSubmit()
            {
                Browser.ClickElement("input[value=Submit]");
            }
        }
    }
}