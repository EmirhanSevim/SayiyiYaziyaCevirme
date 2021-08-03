using System;

namespace SayiyiYaziyaCevirme
{
    static class Program
    {
        [STAThread]

        public static void Main(string[] args)
        {

            Console.Write("Lütfen Bir Sayı Giriniz = ");
            decimal girilenSayi = Convert.ToDecimal(Console.ReadLine());


            Console.Write("Lütfen Bir Döviz Giriniz = ");
            string girilenDoviz = Console.ReadLine();

            Console.WriteLine(yaziyacevir(girilenSayi, girilenDoviz));

            Console.ReadKey();
        }

        private static string yaziyacevir(decimal tutar, string doviz)
        {
            string sTutar = tutar.ToString("F2").Replace('.', ','); // Replace('.',',') ondalık ayracının . olma durumu için            
            string lira = sTutar.Substring(0, sTutar.IndexOf(',')); //Tutarın tam kısmı
            string kurus = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);
            string yazi = "";

            string[] birler = { "", "Bir", "İki", "Üç", "Dört", "Beş", "Altı", "Yedi", "Sekiz", "Dokuz" };
            string[] onlar = { "", "On", "Yirmi", "Otuz", "Kırk", "Elli", "Altmış", "Yetmiş", "Seksen", "Doksan" };
            string[] binler = { "Katrilyon", "Trilyon", "Milyar", "Milyon", "Bin", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.

            int grupSayisi = 6; //Sayıdaki 3'lü grup sayısı. katrilyon içi 6. (1.234,00 daki grup sayısı 2'dir.)
                                //KATRİLYON'un başına ekleyeceğiniz her değer için grup sayısını artırınız.

            lira = lira.PadLeft(grupSayisi * 3, '0'); //Sayının soluna '0' eklenerek sayı 'grup sayısı x 3' basakmaklı yapılıyor.            

            string grupDegeri;

            for (int i = 0; i < grupSayisi * 3; i += 3) //Sayı 3'erli gruplar halinde ele alınıyor.
            {
                grupDegeri = "";

                if (lira.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + "Yüz"; //Yüzler                

                if (grupDegeri == "BirYüz") //Biryüz düzeltiliyor.
                    grupDegeri = "Yüz";

                grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))]; //Onlar

                grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))]; //Birler                

                if (grupDegeri != "") //Binler
                    grupDegeri += binler[i / 3];

                if (grupDegeri == "BirBin") //Birbin düzeltiliyor.
                    grupDegeri = "Bin";

                yazi += grupDegeri;
            }
            if (doviz == "TRY")
                yazi += " TL ";

            if (doviz == "USD")
                yazi += " Dolar ";

            if (doviz == "EUR")
                yazi += " Euro ";

            if (doviz == "GBP")
                yazi += " İngilizSterlini ";

            int yaziUzunlugu = yazi.Length;

            if (kurus.Substring(0, 1) != "0") //Kuruş onlar
                yazi += onlar[Convert.ToInt32(kurus.Substring(0, 1))];

            if (kurus.Substring(1, 1) != "0") //Kuruş birler
                yazi += birler[Convert.ToInt32(kurus.Substring(1, 1))];

            if (kurus != "00")
            {

                if (doviz == "TRY")
                    yazi += " Kr.";

                if (doviz == "USD")
                    yazi += " Cent";

                if (doviz == "EUR")
                    yazi += " Cent";

                if (doviz == "GBP")
                    yazi += " Penny";
            }


            return yazi;

        }
    }
}
