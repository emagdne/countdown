using CountDownWebTestingFramework;
using NUnit.Framework;

namespace CountDownFunctionalTests
{
    [TestFixture]
    public class The_Registration_Page
    {
        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Display_The_Login_Page_When_The_Login_Link_Is_Clicked_From_The_Register_Page()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.LoginLink.Click();
            Assert.That(CountDown.IsOnLoginPage, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Use_Password_Field_On_The_Register_Page_For_Password()
        {
            CountDown.GoToRegistrationPage();
            Assert.That(CountDown.RegistrationPage.PasswordField.IsPasswordField, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Use_Password_Field_On_The_Register_Page_For_ReenterPassword()
        {
            CountDown.GoToRegistrationPage();
            Assert.That(CountDown.RegistrationPage.PasswordConfirmField.IsPasswordField, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Clear_All_Fields_When_Clear_Is_Clicked()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.FillFieldsWithRandomValues();
            CountDown.RegistrationPage.ClickClear();
            Assert.That(CountDown.RegistrationPage.AreFieldsBlank(), Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Missing_FirstName()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.ClickSubmit();
            Assert.That(CountDown.RegistrationPage.FirstNameField.ErrorMessage,
                Is.EqualTo("You must provide a first name."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Long_FirstName()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.FirstNameField.Fill(WebTestHelper.RandomString(51));
            CountDown.RegistrationPage.ClickSubmit();
            Assert.That(CountDown.RegistrationPage.FirstNameField.ErrorMessage,
                Is.EqualTo("The first name must be from 1 to 50 characters in length."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Long_LastName()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.LastNameField.Fill(WebTestHelper.RandomString(51));
            CountDown.RegistrationPage.ClickSubmit();
            Assert.That(CountDown.RegistrationPage.LastNameField.ErrorMessage,
                Is.EqualTo("The last name must be from 0 to 50 characters in length."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Missing_Email()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.ClickSubmit();
            Assert.That(CountDown.RegistrationPage.EmailField.ErrorMessage,
                Is.EqualTo("You must provide an email address."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Badly_Formatted_Email()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.EmailField.Fill(WebTestHelper.RandomString());
            CountDown.RegistrationPage.ClickSubmit();
            Assert.That(CountDown.RegistrationPage.EmailField.ErrorMessage,
                Is.EqualTo("Please enter a valid email address."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Missing_Email_Confirm()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.ClickSubmit();
            Assert.That(CountDown.RegistrationPage.EmailConfirmField.ErrorMessage,
                Is.EqualTo("You must confirm your email address."));
        }


        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Bad_Email_Confirm()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.EmailField.Fill("xy@email.com");
            CountDown.RegistrationPage.EmailConfirmField.Fill("xyz@email.com");
            CountDown.RegistrationPage.ClickSubmit();
            Assert.That(CountDown.RegistrationPage.EmailConfirmField.ErrorMessage,
                Is.EqualTo("Emails do not match."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Missing_Password()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.ClickSubmit();
            Assert.That(CountDown.RegistrationPage.PasswordField.ErrorMessage,
                Is.EqualTo("You must provide a password."));
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Short_Password(int length)
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.PasswordField.Fill(WebTestHelper.RandomString(length));
            CountDown.RegistrationPage.ClickSubmit();
            Assert.That(CountDown.RegistrationPage.PasswordField.ErrorMessage,
                Is.EqualTo("The password must be from 4 to 50 characters in length."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Long_Password()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.PasswordField.Fill(WebTestHelper.RandomString(51));
            CountDown.RegistrationPage.ClickSubmit();
            Assert.That(CountDown.RegistrationPage.PasswordField.ErrorMessage,
                Is.EqualTo("The password must be from 4 to 50 characters in length."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Missing_Password_Confirm()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.ClickSubmit();
            Assert.That(CountDown.RegistrationPage.PasswordConfirmField.ErrorMessage,
                Is.EqualTo("You must confirm your password."));
        }

        [Test]
        [TestCase("1234")]
        [TestCase("123456")]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Unconfirmed_Password(string confirm)
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.PasswordField.Fill("12345");
            CountDown.RegistrationPage.PasswordConfirmField.Fill(confirm);
            CountDown.RegistrationPage.ClickSubmit();
            Assert.That(CountDown.RegistrationPage.PasswordConfirmField.ErrorMessage,
                Is.EqualTo("Passwords do not match."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Return_To_Login_Page_After_Valid_Registration()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.FirstNameField.Fill(WebTestHelper.RandomString());
            CountDown.RegistrationPage.LastNameField.Fill(WebTestHelper.RandomString());
            CountDown.RegistrationPage.EmailField.Fill("xyz@gmail.com");
            CountDown.RegistrationPage.EmailConfirmField.Fill("xyz@gmail.com");
            CountDown.RegistrationPage.PasswordField.Fill("12345");
            CountDown.RegistrationPage.PasswordConfirmField.Fill("12345");
            CountDown.RegistrationPage.ClickSubmit();
            CountDownDatabase.DeleteUser("xyz@gmail.com");
            Assert.That(CountDown.IsOnLoginPage, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Have_Email_In_Email_Field_And_Registration_successful_On_Login_Page_After_Valid_Registration()
        {
            CountDown.GoToRegistrationPage();
            CountDown.RegistrationPage.FirstNameField.Fill(WebTestHelper.RandomString());
            CountDown.RegistrationPage.LastNameField.Fill(WebTestHelper.RandomString());
            CountDown.RegistrationPage.EmailField.Fill("xyz@gmail.com");
            CountDown.RegistrationPage.EmailConfirmField.Fill("xyz@gmail.com");
            CountDown.RegistrationPage.PasswordField.Fill("12345");
            CountDown.RegistrationPage.PasswordConfirmField.Fill("12345");
            CountDown.RegistrationPage.ClickSubmit();
            CountDownDatabase.DeleteUser("xyz@gmail.com");
            Assert.That(CountDown.LoginPage.EmailField.Value == "xyz@gmail.com"
                        &&
                        CountDown.LoginPage.MessageArea.Text ==
                        "You have successfully registered for the application. Use the form below to login.", Is.True);
        }
    }
}