﻿using CountDown.FunctionalTests.Data.Exceptions;
using CountDown.FunctionalTests.Data.TestData;

namespace CountDown.FunctionalTests.Data
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public static class TestDataManager
    {
        public static void GenerateTestData()
        {
            try
            {
                TestUsers.SetUp();
                TestToDoItems.SetUp();
            }
            catch (MissingDependencyException)
            {
                DestroyTestData();
                throw;
            }
        }

        public static void DestroyTestData()
        {
            TestToDoItems.CleanUp();
            TestUsers.CleanUp();
        }
    }
}
