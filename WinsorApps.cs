namespace CS_First_HTTP_Client;

/// <summary>
/// login object used with the Winsor Apps API
/// </summary>
/// <param name="email"></param>
/// <param name="password"></param>
/// what are the specifics of the problem (if applicable)
/// sometimes this is just a message
/// <param name= "userID"> User ID of the logged in user </param>
/// <param name = "jwt"> Authorization Bearer Token that you will use to Authenticate your requests</param>
/// <param name= "refreshToken"> Token used to renew your Authorization without having to re-enter your password</param>
/// <param name = "expires"> When does your current JWT token expire?</param>

public readonly record struct Login(string email, string password);

public readonly record struct ErrorResponse(string type, string error);

public readonly record struct AuthResponse(
    string userID, string jwt, DateTime expires, string refreshToken);

public readonly record struct UserInfo(
    string id, string firstName, string nickname, string lastName, string email, StudentInfo? studentInfo = null);
    
public readonly record struct StudentInfo(
        int gradYear, int className, AdvisorInfo advisor);

public readonly record struct AdvisorInfo(
    string id, string firstName, string lastName, string email);

public readonly record struct CycleDay(DateOnly date, string cycleDay);   