﻿using System;

namespace CountDown.FunctionalTests.Data
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public class TestToDoItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime Due { get; set; }
        public long AssigneeId { get; set; }
        public TestUser Assignee { get; set; }
        public long OwnerId { get; set; }
        public TestUser Owner { get; set; }
        public bool Completed { get; set; }
    }

    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public class TestUser
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Hash { get; set; }
    }
}
