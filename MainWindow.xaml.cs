﻿using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Beolvasas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Action> methodMap;
        List<Allampolgar> Lakossag = new();
        public MainWindow()
        {
            InitializeComponent();
            methodMap = new Dictionary<string, Action>
            {
                { "1", Feladat1},
                { "2", Feladat2 },
                { "3", Feladat3 },
                { "4", Feladat4 },
                { "5", Feladat5 },
                { "6", Feladat6 },
                { "7", Feladat7 },
                { "8", Feladat8 },
                { "9", Feladat9},
                { "10", Feladat10 },
                { "11", Feladat11 },
                { "12", Feladat12 },
                { "13", Feladat13 },
                { "14", Feladat14 },
                { "15", Feladat15 },
                { "16", Feladat16 },
                { "17", Feladat17 },
                { "18", Feladat18 },
                { "19", Feladat19 },
                { "20", Feladat20 },
                { "21", Feladat21 },
                { "22", Feladat22 },
                { "23", Feladat23 },
                { "24", Feladat24 },
                { "25", Feladat25 },
            };
            using StreamReader sr = new(
                path: @"../../../src/bevölkerung.txt",
                encoding: UTF8Encoding.UTF8);
            _ = sr.ReadLine();
            while (!sr.EndOfStream) Lakossag.Add(new(sr.ReadLine()));
            foreach(var key in methodMap) Sorszam.Items.Add(key.Key.ToString());
            foreach (var lakos in Lakossag) MegoldasTeljes.Items.Add(lakos);
        }

        private void Sorszam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearAllData();
            if (methodMap.TryGetValue(Sorszam.SelectedValue.ToString(), out var method))
            {
                method.Invoke();
            }
        }

        private void ClearAllData()
        {
            MegoldasLista.Items.Clear();
            MegoldasMondatos.Content = "";
            MegoldasTeljes.Items.Clear();
        }

        public void Feladat1()
        {
            var feladat = Lakossag.OrderByDescending(l => l.NettoJovedelem).First().NettoJovedelem;
            MegoldasMondatos.Content = $"A legmagasabb éves nettó jövedelem: {feladat}";
        }

        public void Feladat2()
        {
            var feladat = Lakossag.Average(l => l.NettoJovedelem);
            MegoldasMondatos.Content = $"A lakosok átlaeg nettő jövedelme: {feladat}";
        }

        public void Feladat3()
        {
            var feladat = Lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, g => g.Count());
            foreach (var f in feladat)
            {
                string elem = $"{f.Key} - {f.Value}";
                MegoldasLista.Items.Add(elem);
            }
        }

        public void Feladat4()
        {
            var feladat = Lakossag.Where(l => l.Nemzetiseg.Equals("angolai"));
            foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
        }
        public void Feladat5()
        {
            var feladat = Lakossag.Where(l => l.Kor.Equals(Lakossag.Min(l => l.Kor)));
            foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
        }
        public void Feladat6()
        {
            var feladat = Lakossag.Where(l => !l.Dohanyzik);
            foreach (var f in feladat)
            {
                string elem = $"{f.Id} - {f.HaviNettoJovedelem}";
                MegoldasLista.Items.Add(elem);
            }
        }

        public void Feladat7()
        {
            var feladat = Lakossag.Where(l => l.Tartomany.Equals("Bajorország") && l.NettoJovedelem > 30000).OrderBy(l => l.IskolaiVegzettseg);
            foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
        }

        public void Feladat8()
        {
            var feladat = Lakossag.Where(l => l.Nem.Equals("férfi"));
            foreach (var f in feladat) MegoldasLista.Items.Add(f.ToString(true));
        }

        public void Feladat9()
        {
            var feladat = Lakossag.Where(l => l.Nem.Equals("nő") && l.Tartomany.Equals("Bajorország"));
            foreach (var f in feladat) MegoldasLista.Items.Add(f.ToString(false));
        }
        public void Feladat10()
        {
            var feladat = Lakossag.Where(l => !l.Dohanyzik).OrderBy(l => l.NettoJovedelem).ToList();
            for (int i = 0; i < 10; i++) MegoldasLista.Items.Add(feladat[i].ToString(true));
        }
        public void Feladat11()
        {
            var feladat = Lakossag.OrderByDescending(l => l.Kor).ToList();
            for (int i = 0; i < 5; i++) MegoldasTeljes.Items.Add(feladat[i]);
        }
        public void Feladat12()
        {
            var feladat = Lakossag.Where(l => l.Nemzetiseg.Equals("német")).OrderBy(l => l.Tartomany).DistinctBy(l => l.Tartomany);
            foreach (var fel in feladat)
            {
                MegoldasLista.Items.Add(fel.Tartomany);
                var temp = feladat.Where(f => f.Tartomany.Equals(fel.Tartomany)).ToList();
                foreach (var f in temp) MegoldasLista.Items.Add(f.Id + " - " + (f.AktivSzavazo ? "aktív szavazó" : "nem aktív szavazó"));
            }
            
        }
        public void Feladat13()
        {
            var feladat = Lakossag.Where(l => l.Nem.Equals("férfi")).Average(l => l.SorFogyasztasEvente);
            MegoldasMondatos.Content = $"A férfiak átlag sörfogyasztása évente: {feladat:0} liter";
        }
        public void Feladat14()
        {
            var feladat = Lakossag.OrderBy(l => l.IskolaiVegzettseg).ToList();
            foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
        }
        public void Feladat15()
        {
            var feladat = Lakossag.OrderByDescending(l => l.NettoJovedelem).ToList();
            for (int i = 0; i < 3; i++) MegoldasLista.Items.Add(feladat[i].ToString(false));
            feladat.Reverse();
            for (int i = 0; i < 3; i++) MegoldasLista.Items.Add(feladat[i].ToString(false));
        }
        public void Feladat16()
        {
            var feladat = Lakossag.Where(l => l.AktivSzavazo).Count();
            MegoldasMondatos.Content = $"A lakosság {feladat:0.00}%-a aktív szaavazó";
        }
        public void Feladat17()
        {

        }
        public void Feladat18()
        {

        }
        public void Feladat19()
        {

        }
        public void Feladat20()
        {

        }
        public void Feladat21()
        {

        }
        public void Feladat22()
        {

        }
        public void Feladat23()
        {

        }
        public void Feladat24()
        {

        }
        public void Feladat25()
        {

        }

    }
}