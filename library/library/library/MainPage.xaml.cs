using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using library.Pages;


namespace library
{
    public partial class MainPage : ContentPage
    {
        Books books { get; set; }
        Login login { get; set; }
        Mates mates { get; set; }
        MyRentals myRentals { get; set; }
        Registration register { get; set; }
        Rented rented { get; set; }
        Settings settings { get; set; }
        UserView userView { get; set; }




        public MainPage()
        {

            InitializeComponent();
            books = new Books();
            login = new Login();
            mates = new Mates();
            myRentals = new MyRentals();
            register = new Registration();
            rented = new Rented();
            settings = new Settings();
            userView = new UserView();

            

            btnBooks.Clicked += (s, e) => Navigation.PushAsync(books);
            btnLoginPage.Clicked+=(s,e)=> Navigation.PushAsync(login);
            btnMates.Clicked += (s, e) => Navigation.PushAsync(mates);
            btnMyRentals.Clicked += (s, e) => Navigation.PushAsync(myRentals);
            btnRegister.Clicked += (s, e) => Navigation.PushAsync(register);
            btnRented.Clicked += (s, e) => Navigation.PushAsync(rented);
            btnSettings.Clicked += (s, e) => Navigation.PushAsync(settings);
            btnUserView.Clicked += (s, e) => Navigation.PushAsync(userView);


        }

   
    }
}
