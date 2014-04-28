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

            public static bool HasItemsPendingLink()
            {
                return Browser.HasElement("#index-items-pending-link");
            }

            public static bool ColorsItemsOwnedByAndAssignedToOtherUsersWhite()
            {
                Filters.OwnedByMe.UnCheck();
                Filters.OwnedByOthers.Check();
                Filters.AssignedToOthers.Check();
                Filters.Completed.UnCheck();
                ApplyFilters();

                do
                {
                    if (
                        !Browser.ElementsHaveCssByXpath(
                            "//tr[@class='index-table-row' and " +
                            "td[@class='index-owner-cell' and not(contains(text(), 'Me'))] and " +
                            "td[@class='index-assignee-cell' and not(contains(text(), 'Me'))]]",
                            "background-color",
                            "rgba(0, 0, 0, 0)"))
                    {
                        return false;
                    }
                } while (PaginationControl.TryNextPage());

                return true;
            }

            public static bool ColorsItemsOwnedByAndAssignedToTheCurrentUserOrange()
            {
                Filters.OwnedByMe.Check();
                Filters.OwnedByOthers.UnCheck();
                Filters.AssignedToOthers.UnCheck();
                Filters.Completed.UnCheck();
                ApplyFilters();

                do
                {
                    if (
                        !Browser.ElementsHaveCssByXpath(
                            "//tr[@class='index-table-row' and " +
                            "td[@class='index-owner-cell' and contains(text(), 'Me')] and " +
                            "td[@class='index-assignee-cell' and contains(text(), 'Me')]]",
                            "background-color",
                            "rgba(255, 226, 174, 1)"))
                    {
                        return false;
                    }
                } while (PaginationControl.TryNextPage());

                return true;
            }

            public static bool ColorsItemsOwnedByTheCurrentUserAndAssignedToAnotherUserYellow()
            {
                Filters.OwnedByMe.Check();
                Filters.OwnedByOthers.UnCheck();
                Filters.AssignedToOthers.Check();
                Filters.Completed.UnCheck();
                ApplyFilters();

                do
                {
                    if (
                        !Browser.ElementsHaveCssByXpath(
                            "//tr[@class='index-table-row' and " +
                            "td[@class='index-owner-cell' and contains(text(), 'Me')] and " +
                            "td[@class='index-assignee-cell' and not(contains(text(), 'Me'))]]",
                            "background-color",
                            "rgba(255, 255, 187, 1)"))
                    {
                        return false;
                    }
                } while (PaginationControl.TryNextPage());

                return true;
            }

            public static bool ColorsCompletedItemsGray()
            {
                Filters.OwnedByMe.Check();
                Filters.OwnedByOthers.Check();
                Filters.AssignedToOthers.Check();
                Filters.Completed.Check();
                ApplyFilters();

                do
                {
                    if (
                        !Browser.ElementsHaveCssByXpath(
                            "//tr[@class='index-table-row' and " +
                            "td[@class='index-completed-cell' and input[@checked='checked']]]",
                            "background-color",
                            "rgba(223, 223, 223, 1)"))
                    {
                        return false;
                    }
                } while (PaginationControl.TryNextPage());

                return true;
            }

            public static bool EnablesTheCompletedCheckboxForUncompletedItemsAssignedToTheCurrentUser()
            {
                Filters.OwnedByMe.Check();
                Filters.OwnedByOthers.Check();
                Filters.AssignedToOthers.UnCheck();
                Filters.Completed.UnCheck();
                ApplyFilters();

                do
                {
                    if (
                        !Browser.ElementsDoNotHaveAttributeByXpath(
                            "//tr[@class='index-table-row' and td[@class='index-assignee-cell' and contains(text(), 'Me')]]" +
                            "/td[@class='index-completed-cell']" +
                            "/input[@type='checkbox']",
                            "disabled"))
                    {
                        return false;
                    }
                } while (PaginationControl.TryNextPage());

                return true;
            }

            public static bool DisablesTheCompletedCheckboxForUncompletedItemsAssignedToOtherUsers()
            {
                Filters.OwnedByMe.Check();
                Filters.OwnedByOthers.Check();
                Filters.AssignedToOthers.Check();
                Filters.Completed.UnCheck();
                ApplyFilters();

                do
                {
                    if (
                        !Browser.ElementsHaveAttributeWithValueByXpath(
                            "//tr[@class='index-table-row' and td[@class='index-assignee-cell' and not(contains(text(), 'Me'))]]" +
                            "/td[@class='index-completed-cell']" +
                            "/input[@type='checkbox']",
                            "disabled", "true"))
                    {
                        return false;
                    }
                } while (PaginationControl.TryNextPage());

                return true;
            }

            public static bool DisablesTheCompletedCheckboxForCompletedItems()
            {
                Filters.OwnedByMe.Check();
                Filters.OwnedByOthers.Check();
                Filters.AssignedToOthers.Check();
                Filters.Completed.Check();
                ApplyFilters();

                do
                {
                    if (
                        !Browser.ElementsHaveAttributeWithValueByXpath(
                            "//tr[@class='index-table-row']" +
                            "/td[@class='index-completed-cell']" +
                            "/input[@type='checkbox' and @checked='checked']",
                            "disabled", "true"))
                    {
                        return false;
                    }
                } while (PaginationControl.TryNextPage());

                return true;
            }

            public static bool OnlyShowsItemsOwnedByTheCurrentUserWhenTheOwnedByMeFilterIsApplied()
            {
                Filters.OwnedByMe.Check();
                Filters.OwnedByOthers.UnCheck();
                Filters.AssignedToOthers.UnCheck();
                Filters.Completed.UnCheck();
                ApplyFilters();

                do
                {
                    if (!Browser.ElementsHaveText(".index-owner-cell", "Me"))
                    {
                        return false;
                    }
                } while (PaginationControl.TryNextPage());

                return true;
            }

            public static bool OnlyShowsItemsOwnedByOthersWhenTheOwnedByOthersFilterIsApplied()
            {
                Filters.OwnedByMe.UnCheck();
                Filters.OwnedByOthers.Check();
                Filters.AssignedToOthers.UnCheck();
                Filters.Completed.UnCheck();
                ApplyFilters();

                do
                {
                    if (Browser.ElementsHaveText(".index-owner-cell", "Me"))
                    {
                        return false;
                    }
                } while (PaginationControl.TryNextPage());

                return true;
            }

            public static bool ShowsItemsAssignedToOthersWhenTheAssignedToOthersFilterIsApplied()
            {
                Filters.OwnedByMe.Check();
                Filters.OwnedByOthers.Check();
                Filters.AssignedToOthers.Check();
                Filters.Completed.UnCheck();
                ApplyFilters();

                do
                {
                    // Find at least ONE assignee cell not assigned to the current user
                    if (!Browser.ElementsHaveText(".index-assignee-cell", "Me"))
                    {
                        return true;
                    }
                } while (PaginationControl.TryNextPage());

                return false;
            }

            public static bool ShowsCompletedItemsWhenTheCompletedFilterIsApplied()
            {
                Filters.OwnedByMe.Check();
                Filters.OwnedByOthers.Check();
                Filters.AssignedToOthers.Check();
                Filters.Completed.Check();
                ApplyFilters();

                do
                {
                    // If there exists one checked checkbox in the completed column, return true
                    if (!Browser.ElementsDoNotHaveAttributeByXpath("//input[@class = 'index-completed-checkbox']", "checked"))
                    {
                        return true;
                    }
                } while (PaginationControl.TryNextPage());

                return false;
            }

            public static void ClickCreateToDoLink()
            {
                Browser.ClickElement("#index-create-link");
            }

            public static class PaginationControl
            {
                public static bool IsVisible
                {
                    get { return Browser.HasElement(".mvc-pager"); }
                }

                public static bool TryNextPage()
                {
                    return Browser.ClickElementByXpath("/html/body/div[2]/div/div[2]/div/a[contains(text(), '»')]");
                }
            }

            public static class ToDoItemTable
            {
                public static int ItemCount
                {
                    get { return Browser.ElementCount(".index-table-row"); }
                }
            }

            public static class Filters
            {
                public static class OwnedByMe
                {
                    public static bool IsChecked
                    {
                        get
                        {
                            return Browser.ElementHasAttributeWithValue("#index-owned-by-me-checkbox", "checked", "true");
                        }
                    }

                    public static void Check()
                    {
                        Browser.CheckInputCheckbox("#index-owned-by-me-checkbox");
                    }

                    public static void UnCheck()
                    {
                        Browser.UnCheckInputCheckbox("#index-owned-by-me-checkbox");
                    }
                }

                public static class OwnedByOthers
                {
                    public static bool IsChecked
                    {
                        get
                        {
                            return Browser.ElementHasAttributeWithValue("#index-owned-by-others-checkbox", "checked",
                                "true");
                        }
                    }

                    public static void Check()
                    {
                        Browser.CheckInputCheckbox("#index-owned-by-others-checkbox");
                    }

                    public static void UnCheck()
                    {
                        Browser.UnCheckInputCheckbox("#index-owned-by-others-checkbox");
                    }
                }

                public static class AssignedToOthers
                {
                    public static bool IsChecked
                    {
                        get
                        {
                            return Browser.ElementHasAttributeWithValue("#index-assigned-to-others-checkbox", "checked",
                                "true");
                        }
                    }

                    public static void Check()
                    {
                        Browser.CheckInputCheckbox("#index-assigned-to-others-checkbox");
                    }

                    public static void UnCheck()
                    {
                        Browser.UnCheckInputCheckbox("#index-assigned-to-others-checkbox");
                    }
                }

                public static class Completed
                {
                    public static bool IsChecked
                    {
                        get
                        {
                            return Browser.ElementHasAttributeWithValue("#index-filter-completed-checkbox", "checked",
                                "true");
                        }
                    }

                    public static void Check()
                    {
                        Browser.CheckInputCheckbox("#index-filter-completed-checkbox");
                    }

                    public static void UnCheck()
                    {
                        Browser.UnCheckInputCheckbox("#index-filter-completed-checkbox");
                    }
                }
            }

            public static void ApplyFilters()
            {
                Browser.ClickElement("input[value=Apply]");
            }
        }
    }
}