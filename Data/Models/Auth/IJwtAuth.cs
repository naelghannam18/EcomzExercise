namespace EcomzExercise.Models.Auth
{
    public interface IJwtAuth
    {
        string Authentication(string Email, string Role);
        string UserAuthentication(string Email);
    }
}
