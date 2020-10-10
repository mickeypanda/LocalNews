using Firebase.Auth;
using LocalNews.Helpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalNews.Droid.Dependencies.Auth))]
namespace LocalNews.Droid.Dependencies
{
    public class Auth : IAuth
    {
        public async Task<bool> AuthenticateUser(string email, string password)
        {
            try
            {

                await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidUserException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An unknown error occured. Please try again later");
            }

        }

        public string GetCurrentUserID()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
        }

        public bool IsAuthenticated()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser != null;
        }

        public async Task<bool> RegisterUser(string name, string email, string password)
        {
            try
            {
                //creating an user with email and password
                await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);

                //create a profile builder to add the name to the profile
                var profileBuilder = new Firebase.Auth.UserProfileChangeRequest.Builder();
                profileBuilder.SetDisplayName(name);
                var build = profileBuilder.Build();

                //getting the current user
                var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;

                //updating the profile of the user with the above build
                await user.UpdateProfileAsync(build);

                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(FirebaseAuthUserCollisionException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception("An unknown error occured. Please try again later");
            }
            
        }
    }
}