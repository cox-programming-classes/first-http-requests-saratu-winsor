namespace CS_First_HTTP_Client;

/// <summary>
/// login object used with the Winsor Apps API
/// </summary>
/// <param name="email"></param>
/// <param name="password"></param>
/// what are the specifics of the problem (if applicable)
/// sometimes this is just a message
public readonly record struct Login(string email, string password);

public readonly record struct ErrorResponse(string type, string error);

public readonly record struct AuthResponse(string userID, string jwt, DateTime expires, string refreshToken);

public readonly record struct UserInfo(string id, string firstName, string nickname, string lastName, string email);