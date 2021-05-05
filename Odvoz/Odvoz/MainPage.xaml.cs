using Odvoz.Models;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

namespace Odvoz
{
    public partial class MainPage : ContentPage
    {
        string polotovar = string.Empty;
        string vykres = string.Empty;

        public MainPage()
        {
            InitializeComponent();
            Refresh();
        }

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "export.db3");

        public void Refresh()
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Predmet>();
            seznamOdvozu.ItemsSource = db.Table<Predmet>().ToList();
        }

        private void ImportButton(object sender, EventArgs e)
        {
            try 
            {
                if (polotovar != null || vykres != null || jmenoEntry.Text != null || zulEntry.Text != null || ksEntry.Text != null)
                {
                    DateTime date = DateTime.Now;
                    var db = new SQLiteConnection(_dbPath);
                    db.CreateTable<Predmet>();
                    var maxPk = db.Table<Predmet>().OrderByDescending(c => c.Id).FirstOrDefault();
                    Predmet predmet = new Predmet()
                    {
                        Id = (maxPk == null ? 1 : maxPk.Id + 1),
                        Date = new DateTime(date.Year, date.Month, date.Day),
                        Jmeno = jmenoEntry.Text,
                        Zul = zulEntry.Text,
                        Polotovar = polotovar,
                        Vykres = vykres,
                        Ks = int.Parse(ksEntry.Text),
                        SearchId = new DateTime(date.Year, date.Month, date.Day).ToString() + jmenoEntry.Text + zulEntry.Text + polotovar + vykres + ksEntry.Text
                    };
                    db.Insert(predmet);
                    Refresh();
                    DisplayAlert("Uloženo", "Uloženo", "OK");
                }
                else
                {
                    DisplayAlert(null, "Zadejte všechny informace!", "OK");
                }
            }
            catch
            {
                DisplayAlert("Chyba", "Něco se stalo", "Zkusit znovu");
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            var searchResult = db.Table<Predmet>().Where(c => c.SearchId.ToLower().Contains(searchBar.Text.ToLower()));
            seznamOdvozu.ItemsSource = searchResult;
        }

        private async void DeleteButton(object sender, EventArgs e)
        {
            var button = (StackLayout)sender;
            var action = await DisplayAlert("Smazat", "Chcete smazat záznam?", "Ano", "Ne");
            if (action)
            {
                var butto = sender as StackLayout;
                var plane = (Predmet)button.BindingContext;
                var db = new SQLiteConnection(_dbPath);
                db.Delete<Predmet>(plane.Id);
                Refresh();
            }
        }

        private async void polotovarNe_Tapped(object sender, EventArgs e)
        {
            await framePolotovarNe.ScaleTo(0.9, 50);
            await framePolotovarNe.ScaleTo(1, 50);
            polotovar = "Ne";
            framePolotovarNe.BackgroundColor = Color.FromHex("808080");
            labelPolotovarNe.TextColor = Color.FromHex("#ffffff");
            framePolotovarAno.BackgroundColor = Color.FromHex("ffffff");
            labelPolotovarAno.TextColor = Color.FromHex("808080");
        }

        private async void polotovarAno_Tapped_1(object sender, EventArgs e)
        {
            await framePolotovarAno.ScaleTo(0.9, 50);
            await framePolotovarAno.ScaleTo(1, 50);
            polotovar = "Ano";
            framePolotovarAno.BackgroundColor = Color.FromHex("808080");
            labelPolotovarAno.TextColor = Color.FromHex("#ffffff");
            framePolotovarNe.BackgroundColor = Color.FromHex("ffffff");
            labelPolotovarNe.TextColor = Color.FromHex("808080");
        }

        private async void vykresNe_Tapped(object sender, EventArgs e)
        {
            await frameVykresNe.ScaleTo(0.9, 50);
            await frameVykresNe.ScaleTo(1, 50);
            vykres = "Ne";
            frameVykresNe.BackgroundColor = Color.FromHex("808080");
            labelVykresNe.TextColor = Color.FromHex("#ffffff");
            frameVykresAno.BackgroundColor = Color.FromHex("ffffff");
            labelVykresAno.TextColor = Color.FromHex("808080");
        }

        private async void vykresAno_Tapped(object sender, EventArgs e)
        {
            await frameVykresAno.ScaleTo(0.9, 50);
            await frameVykresAno.ScaleTo(1, 50);
            vykres = "Ano";
            frameVykresAno.BackgroundColor = Color.FromHex("808080");
            labelVykresAno.TextColor = Color.FromHex("#ffffff");
            frameVykresNe.BackgroundColor = Color.FromHex("ffffff");
            labelVykresNe.TextColor = Color.FromHex("808080");
        }
    }
}
