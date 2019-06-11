using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;
using WebApp.Providers;
using WebApp.Results;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [HttpGet]
        [Route("GetVerificationStatus")]
        public IHttpActionResult GetVerificationStatus()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            var userId = User.Identity.GetUserId();
            var user = unitOfWork.Users.GetUserById(userId);

            return Ok(user.VerificationStatus);
        }

        // GET api/Account/GetAdminProfile
        [HttpGet]
        [Route("GetAdminProfile")]
        public IHttpActionResult GetAdminProfile()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            var userId = User.Identity.GetUserId();
            var user = unitOfWork.Users.GetUserById(userId);

            return Ok(new AdminProfileBindingModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Address = user.Address
            });
        }

        [HttpPut]
        [Route("UpdateAdminProfile")]
        public IHttpActionResult UpdateAdminProfile(AdminProfileBindingModel bindingModel)
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            var userId = User.Identity.GetUserId();
            var user = unitOfWork.Users.GetUserById(userId);

            user.Email = bindingModel.Email;
            user.FirstName = bindingModel.FirstName;
            user.LastName = bindingModel.LastName;
            user.BirthDate = bindingModel.BirthDate;
            user.Address = bindingModel.Address;

            unitOfWork.Users.Update(user);
            unitOfWork.Complete();

            return Ok();
        }

        [HttpGet]
        [Route("GetControllerProfile")]
        public IHttpActionResult GetControllerProfile()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            var userId = User.Identity.GetUserId();
            var user = unitOfWork.Users.GetUserById(userId);

            return Ok(new ControllerProfileBindingModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Address = user.Address
            });
        }

        [HttpGet]
        [Route("GetNotVerifiedUsers")]
        public IHttpActionResult GetNotVerifiedUsers()
        {
            var users = unitOfWork.Users.GetNotVerifiedUsers();
            var resultList = new List<DisplayUserBindingModel>();

            foreach (var user in users)
            {
                resultList.Add( new DisplayUserBindingModel() {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    Address = user.Address
                });  
            }

            return Ok(resultList);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("UploadDocument")]
        public async Task<HttpResponseMessage> UploadDocument(string email)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Resources/Images");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    string filename = file.LocalFileName;
                    string realName = file.Headers.ContentDisposition.FileName.Split('\"')[1];
                    realName = realName.Split('.').Last();
                    realName = filename + "." + realName;
                    File.Move(filename, realName);

                    var user = unitOfWork.Users.Find(u => u.Email.Equals(email)).FirstOrDefault();
                    var nameParts = realName.Split('\\').SkipWhile(p => !p.Equals("Resources"));
                    var relativePath = "";
                    foreach (var part in nameParts)
                        relativePath += part + "\\";
                    relativePath = relativePath.Remove(relativePath.Length - 1);
                    user.ImageDocuments += ";" + relativePath;
                    unitOfWork.Users.Update(user);
                    unitOfWork.Complete();
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetUserDocuments")]
        public IHttpActionResult GetUserDocuments(string id)
        {
            var user = unitOfWork.Users.GetUserById(id);
            List<string> documents = new List<string>();
            if (!String.IsNullOrWhiteSpace(user.ImageDocuments))
            {
                var docs = user.ImageDocuments.Split(';');

                foreach (var doc in docs)
                {
                    if (!String.IsNullOrWhiteSpace(doc))
                        documents.Add(doc);
                }
            }

            return Ok(documents);
        }

        [HttpPatch]
        [Route("VerifyUser")]
        public IHttpActionResult VerifyUser(AccountIdBindingModel bindingModel)
        {
            var user = unitOfWork.Users.GetUserById(bindingModel.Id);
            user.VerificationStatus = Models.Enums.VerificationStatus.Accepted;

            unitOfWork.Users.Update(user);
            unitOfWork.Complete();

            return Ok();
        }

        [HttpPatch]
        [Route("DenyUser")]
        public IHttpActionResult DenyUser(AccountIdBindingModel bindingModel)
        {
            var user = unitOfWork.Users.GetUserById(bindingModel.Id);
            user.VerificationStatus = Models.Enums.VerificationStatus.Denied;

            unitOfWork.Users.Update(user);
            unitOfWork.Complete();

            return Ok();
        }

        [HttpPatch]
        [Route("AddDocument")]
        public IHttpActionResult AddDocument()
        {
            //var user = unitOfWork.Users.GetUserById(id);
            //user.VerificationStatus = Models.Enums.VerificationStatus.Denied;

            //unitOfWork.Users.Update(user);
            //unitOfWork.Complete();

            return Ok();
        }

        [HttpPut]
        [Route("UpdateControllerProfile")]
        public IHttpActionResult UpdateControllerProfile(ControllerProfileBindingModel bindingModel)
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            var userId = User.Identity.GetUserId();
            var user = unitOfWork.Users.GetUserById(userId);

            user.Email = bindingModel.Email;
            user.FirstName = bindingModel.FirstName;
            user.LastName = bindingModel.LastName;
            user.BirthDate = bindingModel.BirthDate;
            user.Address = bindingModel.Address;

            unitOfWork.Users.Update(user);
            unitOfWork.Complete();
            
            return Ok();
        }

        [HttpGet]
        [Route("GetUserProfile")]
        public IHttpActionResult GetUserProfile()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            var userId = User.Identity.GetUserId();
            var user = unitOfWork.Users.GetUserById(userId);

            return Ok(new UserProfileBindingModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Address = user.Address
            });
        }

        [HttpPut]
        [Route("UpdateUserProfile")]
        public IHttpActionResult UpdateUserProfile(UserProfileBindingModel bindingModel)
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            var userId = User.Identity.GetUserId();
            var user = unitOfWork.Users.GetUserById(userId);

            user.Email = bindingModel.Email;
            user.FirstName = bindingModel.FirstName;
            user.LastName = bindingModel.LastName;
            user.BirthDate = bindingModel.BirthDate;
            user.Address = bindingModel.Address;

            unitOfWork.Users.Update(user);
            unitOfWork.Complete();

            return Ok();
        }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // POST api/Account/Logout
        [HttpPost]
        [Authorize(Roles = "Admin, Controller, AppUser")]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                
                 ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email,
                                               Email = model.Email,
                                               FirstName = model.FirstName,
                                               LastName = model.LastName,
                                               BirthDate = model.BirthDate,
                                               Address = model.Address,
                                               PassengerType = model.PassengerType };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            var createdUser = unitOfWork.Users.Find(u => u.UserName.Equals(model.Email)).ToList().FirstOrDefault();

            IdentityResult result2 = await UserManager.AddToRoleAsync(createdUser.Id, "AppUser");

            if (!result2.Succeeded)
            {
                return GetErrorResult(result2);
            }

            return Ok();
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result); 
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
