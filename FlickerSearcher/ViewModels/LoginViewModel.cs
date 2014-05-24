using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ReactiveUI;

namespace FlickerSearcher.ViewModels
{
    public class LoginViewModel : ReactiveObject
    {


        public ICommand LogInCommand
        {
            get; 
            set;
        }


        private string mUserName;
        
        private string mPassword;

        private string mLogInTime;

        
        public string UserName
        {
            get { return mUserName; }
            set { this.RaiseAndSetIfChanged(ref mUserName, value); }
        }

        
        public string Password
        {
            get { return mPassword; }
           set{ this.RaiseAndSetIfChanged(ref mPassword, value); }
        }


        public string LogInTime {
            get { return mLogInTime; }
            set { this.RaiseAndSetIfChanged(ref mLogInTime, value); }}


        public LoginViewModel()
        {

            var AreArgsValid = this.WhenAny(viewModel => viewModel.UserName, vm => vm.Password, (userName, password) =>
                String.IsNullOrEmpty(userName.Value) &&
                                                                                                                        String.IsNullOrEmpty(password.Value));


         

            LogInCommand = new ReactiveCommand(AreArgsValid);

            var logInclient = ((ReactiveCommand)LogInCommand).Timestamp().Subscribe(

               newUser =>
                   LogInTime = newUser.Timestamp.ToString("t"));

        }

        
    }
}
