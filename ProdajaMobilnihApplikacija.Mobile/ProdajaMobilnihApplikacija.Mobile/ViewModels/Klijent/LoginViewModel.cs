using ProdajaMobilnihAplikacija.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProdajaMobilnihApplikacija.Mobile.ViewModels.Klijent
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly APIService serviceKlijenti = new APIService("Klijent");

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }
        //USERNAME
        string _username = string.Empty;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        //PASSWORD
        string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }


        public static int UserID { get; set; }

        //Komand patern - sluzi za povezivanje sa view modelom
        public ICommand LoginCommand { get; set; } //icomand interface omogucava da povezujemo butone itd sa komandama

        async Task Login()
        {
            IsBusy = true;

            //odavde isto kao u win formi login

            APIService.Username = Username;
            APIService.Password = Password;



            LoginRequest search;

            try
            {
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Greška", "Obavezno unesite korisničko ime i lozinku!", "Ok");
                    return;
                }


                search = new LoginRequest()
                {
                    Username = _username,
                    Password = _password
                };

                var request = new KlijentSearchRequest()
                {
                    _Username = Username
                };
                var temp = await serviceKlijenti.Get<List<ProdajaMobilnihAplikacija.Model.Klijent>>(request);
                var obj = temp;

                for (int i = 0; i < obj.Count; i++)
                {
                    if (Username == obj[i].KorisnickoIme)
                    {
                        APIService.UserID = obj[i].KlijentId;
                    }
                }

                Application.Current.MainPage = new MainPage();

            }
            catch (Exception ex)
            {
                //throw;
            }

        }
    }
}
