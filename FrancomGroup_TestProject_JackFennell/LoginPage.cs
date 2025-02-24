namespace FrancomGroup_TestProject_JackFennell
{
    public class MultifactorCredentials
    {
        public int ReferenceNumber { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int Postcode { get; set; }
    }
    public class LoginService
    {
        private Dictionary<int, string> dobLoginData = new Dictionary<int, string>
        {
            {98159735, "03/12/1998"}
        };

        private List<MultifactorCredentials>multiFactorLoginData = new List<MultifactorCredentials>
        {
            new MultifactorCredentials 
            { 
                ReferenceNumber = 98159733, 
                Phone = "0431633652", 
                Email = "selmer.ketler@pantherafinance.test", 
                Postcode = 3910
            }
        };

        public bool LoginWithDOB(int referenceNumber, string dob)
        {
            return dobLoginData.ContainsKey(referenceNumber) && dobLoginData[referenceNumber] == dob;
        }

        public bool LoginWithMultiFactor(int referenceNumber, string phone, string email, int postcode)
        {
            var credentials = multiFactorLoginData.FirstOrDefault(x => x.ReferenceNumber == referenceNumber);
            if (credentials == null)
            {
                return false;
            }

            return new[] 
            { 
                credentials.Phone == phone, 
                credentials.Email == email, 
                credentials.Postcode == postcode 
            }
            .Count(match => match)>=2;
            
        }
    }
}

       

        

       