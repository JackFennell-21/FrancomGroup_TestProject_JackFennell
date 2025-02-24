namespace FrancomGroup_TestProject_JackFennell
{
    [TestFixture]
    public class WorkFlowTests
    {
        private LoginService loginService;

        public static IEnumerable<TestCaseData> DOBTestCases
        {
            get
            {
                yield return new TestCaseData(98159735, "03/12/1998", true).SetName("Valid Reference & DOB");
                yield return new TestCaseData(98159735, "01/01/1990", false).SetName("Valid Reference & Invalid DOB");
                yield return new TestCaseData(12345678, "03/12/1998", false).SetName("Invalid Reference & Valid DOB");
                yield return new TestCaseData(12345678, "01/01/1991", false).SetName("Invalid Reference & DOB");
            }
        }

        public static IEnumerable<TestCaseData> MultiFactorTestCases
        {
            get
            {
                yield return new TestCaseData(98159733, "0431633652", "selmer.ketler@pantherafinance.test", 3910, true).SetName("Valid Reference & All Correct Credentials");
                yield return new TestCaseData(98159733, "0123456789", "selmer.ketler@pantherafinance.test", 3910, true).SetName("Valid Reference, Incorrect Phone");
                yield return new TestCaseData(98159733, "0431633652", "invalid_email@test.com", 3910, true).SetName("Valid Reference, Incorrect Email");
                yield return new TestCaseData(98159733, "0431633652", "selmer.ketler@pantherafinance.test", 1111, true).SetName("Valid Reference, Incorrect Postcode");
                yield return new TestCaseData(98159733, "0431633652", "invalid_email@test.com", 1111, false).SetName("Valid Reference, Only 1 Correct Credential");
                yield return new TestCaseData(98159733, "0123456789", "invalid_email@test.com", 1111, false).SetName("Valid Reference, All Incorrect Credentials");
                yield return new TestCaseData(12345678, "0431633652", "selmer.ketler@pantherafinance.test", 3910, false).SetName("Invalid Reference, Correct Credentials");
                yield return new TestCaseData(12345678, "0123456789", "invalid_email@test.com", 1111, false).SetName("Invalid Reference, All Incorrect Credentials");
            }
        }

        [SetUp]
        public void SetUp()
        {
            loginService = new LoginService();
        }

        [Test, TestCaseSource(nameof(DOBTestCases))]
        public void Login_With_DOB(int referenceNumber, string dob, bool expectedResult)
        {
            bool result = loginService.LoginWithDOB(referenceNumber, dob);

            Assert.That(result, Is.EqualTo(expectedResult), 
            $"Expected {expectedResult} for reference {referenceNumber} and DOB {dob}");
            
            Console.WriteLine(
            $"[{(result? "Passed":"Failed")}] Reference {referenceNumber}, DOB {dob}");  
        }

        [Test, TestCaseSource(nameof(MultiFactorTestCases))]
        public void Login_With_MultiFactor(int referenceNumber, string phone, string email, int postcode, bool expectedResult)
        {
            bool result = loginService.LoginWithMultiFactor(referenceNumber, phone, email, postcode);

            Assert.That(result, Is.EqualTo(expectedResult), 
            $"Expected {expectedResult} for reference {referenceNumber}, phone {phone}, email {email}, postcode {postcode}");

            Console.WriteLine(
            $"[{(result? "Passed":"Failed")}] Reference {referenceNumber}, Phone {phone}, Email {email}, Postcode {postcode}");
        }

    }
}

