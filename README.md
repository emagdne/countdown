# Overview

This .NET solution uses NUnit, Moq, Ninject, and Selenium to run automated unit, integration, and functional tests against a simple C# / ASP.NET application. It was developed by myself, Jordan Brown, as part of a graduate-level software verification and validation class. The application, titled CountDown, uses a SQLite database to track users and their to-do items. A summary of CountDown's requirements is as follows:

1. CountDown shall allow users to register by using an email address and password.
2. CountDown shall allow users to log into the system by using a registered email and password.
3. CountDown shall allow users to create to-do items.
4. CountDown shall allow users to view their to-do items as a list.
  - Each to-do item shall have a count-down timer that shows the time left.
5. CountDown shall allow users to mark to-do items as complete.
6. CountDown shall allow users to view a to-do item.
7. CountDown shall allow users to assign existing to-do items to other registered users.
8. CountDown shall allow users to update to-do items.
9. CountDown shall allow users to delete to-do items.

# Installation

Before installing, ensure that the following technologies are available on your machine:

 - .NET Framework 4.5
 - Visual Studio 2013 (other versions may work but have not been tested)
 - Chrome (required for Selenium to run)
 - SQLite 3 (optional but recommended)

To begin, download the repository by clicking "Download ZIP" on GitHub or by using a Git client to run the following:

`git clone https://github.com/emagdne/countdown` 

Next, open `CountDown.sln` in Visual Studio. Right-click on the solution in the Solution Explorer pane and select `Enable NuGet Package Restore`. Click `Yes` on the dialog that appears. After Visual Studio configures package restore, rebuild the solution. NuGet should download the project's dependencies automatically.

# Usage

Run the `CountDown` project to launch the site in IIS. The site should become available at `http://localhost:50212/countdown/`. Register an account, then login to browse the rest of the site.

To run all test cases, launch the site, then use a test runner to run test cases within the `CountDown.UnitTests`, `CountDown.IntegrationTests`, and `CountDown.FunctionalTests` projects. Resharper's `Unit Test Sessions` window is recommended, but any test runner from NuGet capable of running NUnit tests should work.

# FAQ

> Do I need to setup CountDown's SQLite database?

For convenience, a SQLite database is included in the `CountDown` project's directory at `~/App_Data/countdown.sqlite`. This database already has the site's tables loaded. If needed, a script capable of dropping / creating the site's tables can be found in `~/App_Data/schema.sql`.

> I ran and aborted the solution's test cases. When I rerun them, several test cases fail that passed before. What happened?

If functional test cases are run and aborted, the site's SQLite database will be left in an inconsistent state. You will need to connect to the database via a SQL client and delete any test records that were not cleaned up in the aborted run.

> How can I view exceptions thrown by the application? Currently, the system error page is the only page returned.

To view traditional ASP.NET exception pages, open `Web.config` in the `CountDown` project's root folder. Find the `customErrors` tag and set the `mode` attribute to `RemoteOnly` or `Off`.

# MIT License

Copyright (c) 2015 Jordan Brown

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
