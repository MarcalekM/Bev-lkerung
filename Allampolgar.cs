using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beolvasas
{
    internal class Allampolgar
    {
        public int Id { get; set; }
        public string Nem { get; set; }
        public int SzuletesiEv { get; set; }
        public int Suly {  get; set; }
        public int Magassag {  get; set; }
        public bool Dohanyzik { get; set; }
        public string Nemzetiseg {  get; set; }
        public string Nepcsoport { get; set; }
        public string Tartomany {  get; set; }
        public int NettoJovedelem { get; set; }
        public string IskolaiVegzettseg { get; set; }
        public string PolitikaiNezet {  get; set; }
        public bool AktivSzavazo { get; set; }
        public int SorFogyasztasEvente {  get; set; }
        public int KrumpliFogyasztasEvente { get; set; }

        public Allampolgar(string sor) {
            var temp = sor.Split(';');
            Id = int.Parse(temp[0]);
            Nem = temp[1];
            SzuletesiEv = int.Parse(temp[2]);
            Suly = int.Parse(temp[3]);
            Magassag = int.Parse(temp[4]);
            Dohanyzik = temp[5].Equals("igen") ;
            Nemzetiseg = temp[6];
            Nepcsoport = temp[6].Equals("német") ? temp[7] : null;
            Tartomany = temp[8];
            NettoJovedelem = int.Parse(temp[9]);
            IskolaiVegzettseg = temp[10] != " " ? temp[10] : null;
            PolitikaiNezet = temp[11];
            AktivSzavazo = temp[12].Equals("igen");
            SorFogyasztasEvente = temp[13] != "NA" ? int.Parse(temp[13]) : 0;
            KrumpliFogyasztasEvente = temp[14] != "NA" ? int.Parse(temp[14]) : 0;
        }

        public override string ToString()
        {
            return $"{Id} {Nem} {SzuletesiEv} {Suly} {Magassag} {(Dohanyzik ? "igen" : "nem")} {Nemzetiseg} {Nepcsoport} {Tartomany} {NettoJovedelem} {PolitikaiNezet} {(AktivSzavazo ? "igen" : "nem")} {SorFogyasztasEvente} {KrumpliFogyasztasEvente}";
        }
    }
}
