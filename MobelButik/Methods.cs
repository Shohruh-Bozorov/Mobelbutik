﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using MobelButik;


namespace MobelButik
{
    class Methods
    {
        public static void GetProducts()
        {
            using (var db = new MobelButik.Models.NewtonContext())
            {
                var product = db.Produkts;
                foreach (var item in product)
                {
                    Console.WriteLine(item.Id + "\t" + item.ProduktNamn + "\t" + item.Pris);
                }
            }
        }
        public static void GetCategories()
        {
            using (var db = new MobelButik.Models.NewtonContext())
            {
                var category = db.Kategoris;
                foreach (var item in category)
                {
                    Console.WriteLine(item.Id + "\t" + item.Namn);
                }
            }
        }

        public static void GetKundKorg()
        {
            using (var db = new MobelButik.Models.NewtonContext())
            {
                var kundKorg = db.Kundkorgs;
                foreach (var item in kundKorg)
                {
                    Console.WriteLine(item.ProduktId);
                }
            }
        }

        public static void GetKund()
        {
            using (var db = new MobelButik.Models.NewtonContext())
            {
                var kund = db.Kunds;
                foreach (var item in kund)
                {
                    Console.WriteLine(item.Id + "\t" + item.Förnamn + "\t" + item.Efternamn);
                }
            }
        }

        public static void GetBetalningAlt()
        {
            using (var db = new MobelButik.Models.NewtonContext())
            {
                var bAlt = db.BetalningsAlternativs;
                foreach (var item in bAlt )
                {
                    Console.WriteLine(item.Id + "\t" + item.Namn);
                }
            }
        }

        public static void GetLeveransAlt()
        {
            using (var db = new MobelButik.Models.NewtonContext())
            {
                var lAlt = db.LeveransAlternativs;
                foreach (var item in lAlt)
                {
                    Console.WriteLine(item.Id + "\t" + item.Namn + "\t" + item.Pris);
                }
            }
        }




        public static void GetKitchenProducts()
        {


            using (var db = new MobelButik.Models.NewtonContext())
            {
                var result = from
                             Produkts in db.Produkts
                             where Produkts.KategoriId == 1
                             select new Queries { ProduktName = Produkts.ProduktNamn, ProduktPris = (double)Produkts.Pris, ProduktId = Produkts.Id };
                //group Produkts by Produkts.ProduktNamn;

                foreach (var product in result)
                {
                    Console.WriteLine(product.ProduktId + "\t" + product.ProduktName + "\t" + product.ProduktPris);
                }


            }
        }

        public static void GetBedroomProducts()
        {


            using (var db = new MobelButik.Models.NewtonContext())
            {
                var result = from
                             Produkts in db.Produkts
                             where Produkts.KategoriId == 2
                             select new Queries { ProduktName = Produkts.ProduktNamn, ProduktPris = (double)Produkts.Pris, ProduktId = Produkts.Id };
                //group Produkts by Produkts.ProduktNamn;

                foreach (var product in result)
                {
                    Console.WriteLine(product.ProduktId + "\t" + product.ProduktName + "\t" + product.ProduktPris);
                }


            }
        }

        public static void GetLivingRoomProducts()
        {


            using (var db = new MobelButik.Models.NewtonContext())
            {
                var result = from
                             Produkts in db.Produkts
                             where Produkts.KategoriId == 3
                             select new Queries { ProduktName = Produkts.ProduktNamn, ProduktPris = (double)Produkts.Pris, ProduktId = Produkts.Id };
                //group Produkts by Produkts.ProduktNamn;

                foreach (var product in result)
                {
                    Console.WriteLine(product.ProduktId + "\t" + product.ProduktName + "\t" + product.ProduktPris);
                }


            }


        }

        public static int InsertProdukt()
        {
            var affectedRows = 0;

            var connString = "Server=tcp:mobelbutik.database.windows.net,1433;Initial Catalog=Newton;Persist Security Info=False;User ID=vidrusen;Password=troll100!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var produkt = new MobelButik.Models.Produkt();
            produkt = AddProdukt();
            var sql = $"Insert into Produkt(Id, ProduktNamn, TillverkareId, KategoriId, Färg, Material, Pris) values('{produkt.Id}', '{produkt.ProduktNamn}', '{produkt.TillverkareId}', '{produkt.KategoriId}', '{produkt.Färg}', '{produkt.Material}', '{produkt.Pris}')";


            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                try
                {
                    affectedRows = connection.Execute(sql);
                    Console.WriteLine("Din produkt blev tillagd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return affectedRows;

        }

        public static MobelButik.Models.Produkt AddProdukt()
        {



            Console.WriteLine("Skriv in id");
            int prduktId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Skriv in namn");
            string namn = Console.ReadLine();

            Console.WriteLine(
            "Skriv in tillverkarID\n" +
            "1 = Ikea \n" +
            "2 = Mio \n" +
            "3 = FCompany \n" +
            "4 = testTable");
            int tillVerkareID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(
            "Skriv in kategoriID\n" +
            "1 = kök \n" +
            "2 = Sovrum \n" +
            "3 = vardagsrum \n" +
            "4 = Badrum");
            int kategoriID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(
            "Skriv in materialID\n" +
            "1 = Trä \n" +
            "2 = Metall \n" +
            "3 = Plast \n" +
            "4 = Blandat\n" +
            "5 = Bomull");
            int materialID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(
            "Skriv in färgID\n" +
            "1 = Svart \n" +
            "2 = Brun \n" +
            "3 = Vit \n" +
            "4 = Blå\n" +
            "5 = Grön \n" +
            "6 = Gul \n" +
            "7 = Röd \n" +
            "8 = Ofärgat");
            int färgID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Skriv in pris (ex: 150.50)");
            //float pris = float.Parse(Console.ReadLine());
            bool result = float.TryParse(Console.ReadLine(), out float pris);

            var newProdukt = new MobelButik.Models.Produkt()
            {
                Id = prduktId,
                ProduktNamn = namn,
                TillverkareId = tillVerkareID,
                KategoriId = kategoriID,
                Färg = färgID,
                Material = materialID,
                Pris = pris
            };

            return newProdukt;
        }

        public static int DeleteProdukt()
        {
            Console.WriteLine("Vilken produkt vill du ta bort? Skriv in id");
            var deleteId = Convert.ToInt32(Console.ReadLine());
            var affectedRows = 0;

            //var connString = "data source=.\\SQLEXPRESS; initial catalog=MöbelButik; persist security info=true; Integrated Security=true";
            var connString = "Server=tcp:mobelbutik.database.windows.net,1433;Initial Catalog=Newton;Persist Security Info=False;User ID=vidrusen;Password=troll100!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";



            var sql = $" DELETE FROM Produkt WHERE id = {deleteId} ";

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                try
                {
                    affectedRows = connection.Execute(sql);
                    Console.WriteLine("Din produkt blev borttagen");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return affectedRows;

        }

        public static MobelButik.Models.Kategori AddCategory()
        {

            Console.WriteLine("Skriv in id");
            int kategoriId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Skriv in namn");
            string kategoriNamn = Console.ReadLine();

            var newCategory = new MobelButik.Models.Kategori()
            {
                Id = kategoriId,
                Namn = kategoriNamn

            };

            return newCategory;
        }

        public static int InsertCategory()
        {
            var affectedRows = 0;

            var connString = "Server=tcp:mobelbutik.database.windows.net,1433;Initial Catalog=Newton;Persist Security Info=False;User ID=vidrusen;Password=troll100!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var category = new MobelButik.Models.Kategori();
            category = AddCategory();
            var sql = $"Insert into Kategori(Id, Namn) values('{category.Id}', '{category.Namn}')";


            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                try
                {
                    affectedRows = connection.Execute(sql);
                    Console.WriteLine("Din kategori blev tillagd");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return affectedRows;

        }

        public static int DeleteCategory()
        {
            Console.WriteLine("Vilken kategori vill du ta bort? Skriv in id");
            var deleteId = Convert.ToInt32(Console.ReadLine());
            var affectedRows = 0;

            //var connString = "data source=.\\SQLEXPRESS; initial catalog=MöbelButik; persist security info=true; Integrated Security=true";
            var connString = "Server=tcp:mobelbutik.database.windows.net,1433;Initial Catalog=Newton;Persist Security Info=False;User ID=vidrusen;Password=troll100!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";



            var sql = $" DELETE FROM Kategori WHERE id = {deleteId} ";

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                try
                {
                    affectedRows = connection.Execute(sql);
                    Console.WriteLine("Din kategori blev borttagen");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return affectedRows;

        }


        public static void searchProduct()
        {
            Console.WriteLine("Skriv in den produkt du vill söka efter");
            var search = Console.ReadLine();

            using (var db = new MobelButik.Models.NewtonContext())
            {
                var products = db.Produkts;
                var searchProd = products.Where(find => find.ProduktNamn.Contains(search));

                Console.WriteLine("--------------------------");

                foreach (var prod in searchProd)
                {
                    Console.WriteLine($"{prod.Id,-5} {prod.ProduktNamn,-25} {prod.Pris} kr");
                }
                Console.WriteLine("--------------------------");
            }



        }

        public static int InsertToKundKorg()
        {
            var affectedRows = 0;

            Console.WriteLine("Skriv in ID på den produkt du vill lägga till");
            var idToAdd = Convert.ToInt32(Console.ReadLine());

            var connString = "Server=tcp:mobelbutik.database.windows.net,1433;Initial Catalog=Newton;Persist Security Info=False;User ID=vidrusen;Password=troll100!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var sql = $" insert into Kundkorg select id from Produkt WHERE id = {idToAdd}";


            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                try
                {
                    affectedRows = connection.Execute(sql);
                    Console.WriteLine("Din vara finns nu på kundkorgen");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return affectedRows;
        }

        public static int DeleteFromKundKorg()
        {
            var affectedRows = 0;

            Console.WriteLine("Skriv in ID på den produkt du vill ta bort från kundkorgen");
            var id = Convert.ToInt32(Console.ReadLine());

            var connString = "Server=tcp:mobelbutik.database.windows.net,1433;Initial Catalog=Newton;Persist Security Info=False;User ID=vidrusen;Password=troll100!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var sql = $"delete from kundkorg WHERE ProduktID = {id};";


            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                try
                {
                    affectedRows = connection.Execute(sql);
                    Console.WriteLine($"Din produkt med id: {id} är nu borttagen från kundkorgen");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return affectedRows;
        }

        public static MobelButik.Models.Kund AddCustomer()
        {

            Console.WriteLine("Skriv in Id");
            int kundId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Skriv in förnamn");
            string fnamn = Console.ReadLine();

            Console.WriteLine("Skriv in efternamn");
            string enamn = Console.ReadLine();

            Console.WriteLine("Skriv in gatunamn");
            string adress = Console.ReadLine();

            Console.WriteLine("Skriv in ort");
            string ort = Console.ReadLine();

            Console.WriteLine("Skriv in postnummer");
            int postnr = Convert.ToInt32(Console.ReadLine());


            var newCustomer = new MobelButik.Models.Kund()
            {
                Id = kundId,
                Förnamn = fnamn,
                Efternamn = enamn,
                Adress = adress,
                Ort = ort,
                Postnummer = postnr
            };

            return newCustomer;
        }

        public static int InsertCustomer()
        {
            var affectedRows = 0;

            var connString = "Server=tcp:mobelbutik.database.windows.net,1433;Initial Catalog=Newton;Persist Security Info=False;User ID=vidrusen;Password=troll100!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var customer = new MobelButik.Models.Kund();
            customer = AddCustomer();
            var sql = $"insert into Kund(Id, Förnamn, Efternamn, Adress, Ort, Postnummer) values({customer.Id}, '{customer.Förnamn}','{customer.Efternamn}','{customer.Adress}','{customer.Ort}',{customer.Postnummer})";


            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                try
                {
                    affectedRows = connection.Execute(sql);
                    Console.WriteLine("Du är nu kund hos oss!");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return affectedRows;



        }

        public static void EmptyCart()
        {

            var connString = "Server=tcp:mobelbutik.database.windows.net,1433;Initial Catalog=Newton;Persist Security Info=False;User ID=vidrusen;Password=troll100!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var sql = $"delete from Kundkorg";

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                try
                {

                  connection.Execute(sql);
                  Console.WriteLine("Kundkorgen har tömts!");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }

        //det som ska fixas
        //* efter köp ska det automatisk kopiera kund id och produkt id till
        //Orderhistorik samt ha en getDate som hämtar datum och sedan töms kundkorgen med EmptyCart()

        //* efter att man köpt ska man få en fejk ordernummer(vi kan använda rng för det)

        //* efter att man har skrivt in sina personlgia information och har valt leverans och betalning alternativ 
        // ska man få en bekräftelse på skärmen som visar allt man har matat in så att man kan bekräfta sedan 
        // frågar den vill du fortsätta? om man v'ljer ja så kopieras ordern till order historik,
        // kundkorgen töms och man får en fejk order nummer


    }
}

