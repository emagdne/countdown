using CountDown.WebTestingFramework;
using NUnit.Framework;

namespace CountDown.FunctionalTests.Tests
{
    [TestFixture]
    public class The_Registration_Page
    {
        [SetUp]
        public void SetUp()
        {
            CountDownApp.GoToRegistrationPage();            
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Display_The_Login_Page_When_The_Login_Link_Is_Clicked_From_The_Register_Page()
        {
            CountDownApp.RegistrationPage.ClickLoginLink();
            Assert.That(CountDownApp.IsOnLoginPage(), Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Use_Password_Field_On_The_Register_Page_For_Password()
        {
            Assert.That(CountDownApp.RegistrationPage.PasswordField.IsPasswordField, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Use_Password_Field_On_The_Register_Page_For_ReenterPassword()
        {
            Assert.That(CountDownApp.RegistrationPage.PasswordConfirmField.IsPasswordField, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Clear_All_Fields_When_Clear_Is_Clicked()
        {
            CountDownApp.RegistrationPage.FirstNameField.Fill(FunctionalTestHelper.RandomString());
            CountDownApp.RegistrationPage.LastNameField.Fill(FunctionalTestHelper.RandomString());
            CountDownApp.RegistrationPage.EmailField.Fill(FunctionalTestHelper.RandomString());
            CountDownApp.RegistrationPage.EmailConfirmField.Fill(FunctionalTestHelper.RandomString());
            CountDownApp.RegistrationPage.PasswordField.Fill(FunctionalTestHelper.RandomString());
            CountDownApp.RegistrationPage.PasswordConfirmField.Fill(FunctionalTestHelper.RandomString());
            CountDownApp.RegistrationPage.ClickClear();
            Assert.That(CountDownApp.RegistrationPage.AreFieldsBlank(), Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Missing_FirstName()
        {
            CountDownApp.RegistrationPage.ClickSubmit();
            Assert.That(CountDownApp.RegistrationPage.FirstNameField.ErrorMessage,
                Is.EqualTo("You must provide a first name."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Long_FirstName()
        {
            CountDownApp.RegistrationPage.FirstNameField.Fill(FunctionalTestHelper.RandomString(51));
            CountDownApp.RegistrationPage.ClickSubmit();
            Assert.That(CountDownApp.RegistrationPage.FirstNameField.ErrorMessage,
                Is.EqualTo("The first name must be from 1 to 50 characters in length."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Long_LastName()
        {
            CountDownApp.RegistrationPage.LastNameField.Fill(FunctionalTestHelper.RandomString(51));
            CountDownApp.RegistrationPage.ClickSubmit();
            Assert.That(CountDownApp.RegistrationPage.LastNameField.ErrorMessage,
                Is.EqualTo("The last name must be from 0 to 50 characters in length."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Missing_Email()
        {
            CountDownApp.RegistrationPage.ClickSubmit();
            Assert.That(CountDownApp.RegistrationPage.EmailField.ErrorMessage,
                Is.EqualTo("You must provide an email address."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Badly_Formatted_Email()
        {
            CountDownApp.RegistrationPage.EmailField.Fill(FunctionalTestHelper.RandomString());
            CountDownApp.RegistrationPage.ClickSubmit();
            Assert.That(CountDownApp.RegistrationPage.EmailField.ErrorMessage,
                Is.EqualTo("Please enter a valid email address."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Missing_Email_Confirm()
        {
            CountDownApp.RegistrationPage.ClickSubmit();
            Assert.That(CountDownApp.RegistrationPage.EmailConfirmField.ErrorMessage,
                Is.EqualTo("You must confirm your email address."));
        }


        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Bad_Email_Confirm()
        {
            CountDownApp.RegistrationPage.EmailField.Fill("xy@email.com");
            CountDownApp.RegistrationPage.EmailConfirmField.Fill("xyz@email.com");
            CountDownApp.RegistrationPage.ClickSubmit();
            Assert.That(CountDownApp.RegistrationPage.EmailConfirmField.ErrorMessage,
                Is.EqualTo("Emails do not match."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Missing_Password()
        {
            CountDownApp.RegistrationPage.ClickSubmit();
            Assert.That(CountDownApp.RegistrationPage.PasswordField.ErrorMessage,
                Is.EqualTo("You must provide a password."));
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Short_Password(int length)
        {
            CountDownApp.RegistrationPage.PasswordField.Fill(FunctionalTestHelper.RandomString(length));
            CountDownApp.RegistrationPage.ClickSubmit();
            Assert.That(CountDownApp.RegistrationPage.PasswordField.ErrorMessage,
                Is.EqualTo("The password must be from 4 to 50 characters in length."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Long_Password()
        {
            CountDownApp.RegistrationPage.PasswordField.Fill(FunctionalTestHelper.RandomString(51));
            CountDownApp.RegistrationPage.ClickSubmit();
            Assert.That(CountDownApp.RegistrationPage.PasswordField.ErrorMessage,
                Is.EqualTo("The password must be from 4 to 50 characters in length."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Missing_Password_Confirm()
        {
            CountDownApp.RegistrationPage.ClickSubmit();
            Assert.That(CountDownApp.RegistrationPage.PasswordConfirmField.ErrorMessage,
                Is.EqualTo("You must confirm your password."));
        }

        [Test]
        [TestCase("1234")]
        [TestCase("123456")]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Report_Error_Message_For_Unconfirmed_Password(string confirm)
        {
            CountDownApp.RegistrationPage.PasswordField.Fill("12345");
            CountDownApp.RegistrationPage.PasswordConfirmField.Fill(confirm);
            CountDownApp.RegistrationPage.ClickSubmit();
            Assert.That(CountDownApp.RegistrationPage.PasswordConfirmField.ErrorMessage,
                Is.EqualTo("Passwords do not match."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Return_To_Login_Page_After_Valid_Registration()
        {
            CountDownApp.RegistrationPage.FirstNameField.Fill(FunctionalTestHelper.RandomString());
            CountDownApp.RegistrationPage.LastNameField.Fill(FunctionalTestHelper.RandomString());
            CountDownApp.RegistrationPage.EmailField.Fill("xyz@gmail.com");
            CountDownApp.RegistrationPage.EmailConfirmField.Fill("xyz@gmail.com");
            CountDownApp.RegistrationPage.PasswordField.Fill("12345");
            CountDownApp.RegistrationPage.PasswordConfirmField.Fill("12345");
            CountDownApp.RegistrationPage.ClickSubmit();
            CountDownDatabase.DeleteUser("xyz@gmail.com");
            Assert.That(CountDownApp.IsOnLoginPage(), Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Have_Email_In_Email_Field_And_Registration_successful_On_Login_Page_After_Valid_Registration()
        {
            CountDownApp.RegistrationPage.FirstNameField.Fill(FunctionalTestHelper.RandomString());
            CountDownApp.RegistrationPage.LastNameField.Fill(FunctionalTestHelper.RandomString());
            CountDownApp.RegistrationPage.EmailField.Fill("xyz@gmail.com");
            CountDownApp.RegistrationPage.EmailConfirmField.Fill("xyz@gmail.com");
            CountDownApp.RegistrationPage.PasswordField.Fill("12345");
            CountDownApp.RegistrationPage.PasswordConfirmField.Fill("12345");
            CountDownApp.RegistrationPage.ClickSubmit();
            CountDownDatabase.DeleteUser("xyz@gmail.com");
            Assert.That(CountDownApp.LoginPage.EmailField.Value == "xyz@gmail.com"
                        &&
                        CountDownApp.LoginPage.MessageArea.Text ==
                        "You have successfully registered for the application. Use the form below to login.", Is.True);
        }
    }
}