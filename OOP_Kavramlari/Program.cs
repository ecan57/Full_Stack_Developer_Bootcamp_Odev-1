using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Kavramlari
{
    /*
         OOP kavramları ve prensiplerini kullanmaya çalışarak IHesapla diye bir interface oluşturup şekiller ve cisimler üzerinden hesaplamalar yapmak istedim. 
    */
    interface IHesapla
    {
        /*
         Interfaceler sadece imza olarak tutuyor o yüzden bu projede sadece Hesapla metodunu tutmak istedim ki her bir cisimde farklı hesaplamalar olacak.
        Yani implement eden her bir sınıf için içini doldurmadığımdan rahatça kullanabileceğim.
         */
        double Hesapla();
    }
    /*
     Daire classı base class olarak kullandım çünkü hesaplanmasını istediğim cisimler Daire şeklini kullanıyor. Bu yüzden abstract keywordünü kullandım ve interface yapımızı implement etti.
     */
    abstract class Daire : IHesapla
    {
        /*
         Access modifier kullanarak hafif bir kısıtlama getridim fielda. Sonra encapsülation uygulayarak erişebilinmesi gereken yerlere erişilmesini de sağladım.
         */
        protected byte YarıCap;
        public byte r
        {
            get { return YarıCap; }
            set { YarıCap = value; }
        }
        /*
         İmplement ettiğimiz metodun içini doldurdum, temel hesaplama bunun üzerinden gidecek.
         */
        public double Hesapla()
        {
            return Math.PI * r;
        }
        //public Daire()
        //{
        //    Console.WriteLine("Daire kullanıldı.");
        //} --- Constructorı burada kullanmak istedim en azından bir anlamı olsun diye ama istediğim yerlerde çalışmasını sağlayamadığım için kullanmaktan vazgeçtim.
    }
    /*
     Küre classı Base olan Daire classını inherit etti. Bu sınıfın inherit edilmesini istemediğim için sealed keywordünü kullanarak sınıfı mühürlendi.
     */
    sealed class Kure : Daire
    {
        public double KureAlan()
        {
            return Math.Round((base.Hesapla() * r * 4), 2);
        }
        public double KureHacim()
        {
            return Math.Round(((KureAlan() * r) / 3), 2);
        }
    }
    class Silindir : Daire
    {
        protected byte Yukseklik;
        public byte h 
        { 
            get { return Yukseklik; }
            set { Yukseklik = value; }
        }
        /*
         Burada virtual metodu kullanıldı. Çünkü inherit edildiğinde miras alan sınıf kendisine göre override edip kullanabilir.
         */
        public virtual double Alan()
        {
            return Math.Round((base.Hesapla() * 2 * (h + r)), 2);
        }
        public double SilindirHacim()
        {
            return Math.Round((base.Hesapla() * r * h), 2);
        }
    }
    /*
     Bu kez Koni classı Silindir classını inherit etti.
     Aslında bu projede multiple inheritance hariç diğer türler kullanılmış oldu. 
     */
    class Koni : Silindir
    {
        protected double Kenar { get; set; }
        public double s 
        {
            get {return Kenar; }
            set {Kenar = value; } 
        }
        /*
         Miras alınan sınıfın metodu bu classta uygulayabilmek için override edildi.
         */
        public override double Alan()
        {
            return Math.Round((base.Hesapla() * (r + Kenar)), 2);
        }
        public double KoniHacim()
        {
            return Math.Round((base.SilindirHacim() / 3), 2);
        }
    }
    class Program
    {
        /*
         Bu kısımda ise polymorphism kullanımı görebiliriz.
         */
        static void Main(string[] args)
        {
            Console.WriteLine("ÜÇ BOYUTLU CİSİMLER");
            System.Threading.Thread.Sleep(1000);
            Console.Write("\nHesaplama için bir yarıçap değeri giriniz: ");
            //Burada base classımız abstract olduğundan new kullanarak nesne oluşturamazdık ama miras aldığı sınıfı kullanarak nesne üretebildik. 
            Daire daire = new Kure();
            daire.r = Convert.ToByte(Console.ReadLine());

            Kure kure = new Kure();
            kure.r = daire.r;
            Console.WriteLine($"\nKürenin Alanı: {kure.KureAlan()} \nKürenin Hacmi: {kure.KureHacim()}");

            Console.Write("\nHesaplama için bir yükseklik değeri giriniz: ");

            Silindir silindir = new Silindir();
            silindir.r = daire.r;
            silindir.h = Convert.ToByte(Console.ReadLine());
            Console.WriteLine($"\nSilindirin Alanı: {silindir.Alan()} \nSilindirin Hacmi: {silindir.SilindirHacim()}");

            Koni koni = new Koni();
            koni.r = kure.r;
            koni.h = silindir.h;
            koni.s = Math.Sqrt(koni.r * koni.r + koni.h * koni.h);
            Console.WriteLine($"\nKoninin Alanı: {koni.Alan()} \nKoninin Hacmi: {koni.KoniHacim()}");

            Console.ReadKey();
        }
    }
}
