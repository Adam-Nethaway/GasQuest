using Android.Gms.Extensions;
using Firebase.Auth;
using GasQuestApp.Droid;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthDroid))]
namespace GasQuestApp.Droid
{
    class AuthDroid : IAuth
    {


        public bool IsSignIn()
        {
            FirebaseUser user = FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }

        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {

                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var tokenResult = await FirebaseAuth.Instance.CurrentUser.GetIdToken(false).AsAsync<GetTokenResult>();
                return tokenResult.Token;

            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
        }

        public async Task<string> SignUpWithEmailAndPassword(string email, string password)
        {
            try
            {

                var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var tokenResult = await FirebaseAuth.Instance.CurrentUser.GetIdToken(false).AsAsync<GetTokenResult>();
                return tokenResult.Token;

            }
            catch (FirebaseAuthUserCollisionException e) 
            {
                e.PrintStackTrace();
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
        }

        public bool SignOut()
        {
            try
            {
                FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetCurrentUser()
        {
            FirebaseUser user = FirebaseAuth.Instance.CurrentUser;

            string uid = user.Uid;

            return uid;
        }
    }
}