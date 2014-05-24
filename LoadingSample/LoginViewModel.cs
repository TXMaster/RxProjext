
using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using ReactiveUI;




namespace LoadingSample
{
    public class LoginViewModel : ReactiveObject
    {

        private string mLoginTime;

        private string mUsername;

        private string mPassword;

        


        public string LogInTime
        {
            get { return mLoginTime; }

            set { this.RaiseAndSetIfChanged(ref mLoginTime, value); }
    }

        public string Username
        {
            get { return mUsername; }

            set { this.RaiseAndSetIfChanged(ref mUsername, value); }
        }

        public string Password
        {
            get { return mPassword; }

            set { this.RaiseAndSetIfChanged(ref mPassword, value); }
        }


        
        public ICommand LoginCommand { get; set; }

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