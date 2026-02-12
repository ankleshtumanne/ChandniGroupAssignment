namespace B2BManagement.Constant
{
    public class AppConstant
    {
        public const string Success = "Operation completed successfully.";
        public const string AlreadyExist = "Emai is Already Exist.";
        public const string InvalidEmailPass = "Invalid Email and Password.";
        public const string Failed = "Something went wrong. Please try again.";

        
        public const string AgentCreated = "Agent registered successfully.";
        public const string AgentNotFound = "Agent not found.";
        public const string AgentAlreadyExists = "Agent already exists with this email.";
        public const string InvalidAgentCredentials = "Invalid agent credentials.";
        
        public const string BookingCreated = "Hotel booking created successfully.";
        public const string CheckingOut = "Check-out must be after check-in.";
        public const string CheckingIn = "Check-out must be after check-in.";
        public const string GuestRequired = "At least one guest is required.";
        public const string BookingNotFound = "Booking not found.";
        public const string InvalidBookingRequest = "Invalid booking request.";
      
        public const string AuditLogCreated = "Audit log recorded successfully.";
       
        public const string Unauthorized = "Unauthorized access.";
        public const string Forbidden = "You do not have permission to perform this action.";
        public const string TokenExpired = "Session expired. Please login again.";
        
        public const string RequiredFieldsMissing = "Required fields are missing.";
        public const string InvalidRequest = "Invalid request data.";
        public const string InvalidTotalPrice = "Invalid total price.";
        public const string Hotel1 = "Grand Plaza Hotel";
        public const string Hotel2 = "City View Inn";
        public const string Hotel3 = "Central Suites";

        public const string DefaultSignInKey = "DefaultSigningKeyAtLeast32CharactersLong!";
        public const string Default = "default";
        public const string Title = "B2B Management API";
        public const string Authorization = "Authorization";
        public const string Bearer = "Bearer";
        public const string EnterJWt = "Enter JWT token only (without Bearer)";


        public const string ErrorOcured = "An error occurred while searching hotels.";
        public const string HotelConfigurationMissing = "HotelBeds configuration missing.";
        



    }
}
