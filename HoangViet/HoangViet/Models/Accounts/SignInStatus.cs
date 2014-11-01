
namespace HoangViet.Models.Accounts
{
    public enum SignInStatus
    {
        Success,
        LockedOut,
        RequiresTwoFactorAuthentication,
        Failure
    }
}