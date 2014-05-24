
using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using ReactiveUI;




namespace LoadingSample
{
    /// <summary>
    /// Controls the Loading View
    /// </summary>
    public class LoginViewModel : ReactiveObject
    {

        #region Members
        /// <summary>
        /// Lof in time
        /// </summary>
        private string mLoginTime;

        /// <summary>
        /// ser to log in
        /// </summary>
        private string mUsername;

        /// <summary>
        /// Password to log in
        /// </summary>
        private string mPassword; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="mLoginTime"/>
        /// </summary>
        public string LogInTime
        {
            get { return mLoginTime; }

            set { this.RaiseAndSetIfChanged(ref mLoginTime, value); }
        }

        /// <summary>
        /// Gets or sets the the <see cref="mUsername"/>
        /// </summary>
        public string Username
        {
            get { return mUsername; }

            set { this.RaiseAndSetIfChanged(ref mUsername, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="mPassword"/>
        /// </summary>
        public string Password
        {
            get { return mPassword; }

            set { this.RaiseAndSetIfChanged(ref mPassword, value); }
        } 
        #endregion

        #region Commands
        /// <summary>
        /// Log in command
        /// </summary>
        public ICommand LoginCommand { get; set; }
        
        #endregion

        /// <summary>
        /// Creates new instance of <see cref="LoginViewModel"/>
        /// </summary>
        public LoginViewModel()
        {
            var AreArgsValid = this.WhenAny(vm => vm.Username,vm => vm.Password,(user,password) => !String.IsNullOrEmpty(user.Value) && !String.IsNullOrEmpty(password.Value));
            LoginCommand = new ReactiveCommand(AreArgsValid);

            var logInclient = ((ReactiveCommand)LoginCommand).Timestamp().Subscribe(

               newUser =>
                   LogInTime = newUser.Timestamp.ToString("T"));




        }

    }
}