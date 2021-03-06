﻿using CountDown.Models.Domain;
using Ninject;
using NUnit.Framework;

namespace CountDown.IntegrationTests.Controllers
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    [SetUpFixture]
    public class ControllerTestsSetUp
    {
        public static StandardKernel Kernel { get; private set; }

        [SetUp]
        public void RunOnceBeforeTests()
        {
            Kernel = new StandardKernel();
            SetDefaultBinding();    
        }

        [TearDown]
        public void RunOnceAfterTests()
        {
            Kernel = null;
        }

        private void SetDefaultBinding()
        {
            Kernel.Bind<LoginAttempt>().To<LoginAttempt>();
            Kernel.Bind<User>().To<User>();
            Kernel.Bind<ToDoItem>()
                .To<ToDoItem>()
                .WithPropertyValue("Assignee", Kernel.Get<User>())
                .WithPropertyValue("Owner", Kernel.Get<User>());
        }
    }
}