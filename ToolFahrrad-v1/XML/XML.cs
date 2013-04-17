using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ToolFahrrad_v1
{
    class XML
    {
        private static DataContainer instance = DataContainer.Instance;

        /*     private static void Main()
             {
            
                 ReadFile();
                 Console.Read();
            
             }*/




        ///ReadFile Methode
        ///ließt Output Datei und füllt Datenkontainer
        public static void ReadFile()
        {
            bool sw1 = false;       //Schalter für Lagereinlesen
            bool sw2 = false;       //Schalter für zukünftigen Wareneingang
            bool sw3 = false;       //Schalter für Waitinglist Workstations und Stock
            bool sw4 = false;       //Schalter für OrdersinWork



            if (!File.Exists(instance.OpenFile))
            {
                throw new UnknownFileException(instance.OpenFile + " existiert nicht, wählen Sie eine gültige Datei!");
            }

            StreamReader file;
            file = File.OpenText(instance.OpenFile);

            string line = "";
            string[] att;
            int i = 0;

            Arbeitsplatz ap = instance.GetArbeitsplatz(1);

            while (file.Peek() != -1)
            {
                line = file.ReadLine();

                if (line == "<warehousestock>" || line == "/<warehousestock>")
                {
                    sw1 = !sw1;
                    continue;
                }

                if (sw1)
                {
                    att = line.Split(new char[] { '"' });

                    if (att[0] == "<article id=")
                    {
                        instance.GetTeil(i + 1).Lagerstand = Convert.ToInt32(att[3]);
                        i++;
                    }
                }


                if (line == "<futureinwardstockmovement>" || line == "</futureinwardstockmovement>")
                {
                    if (sw2 == true)
                    {
                        //XML.InitEteil();
                        //XML.InitializeArbPl();
                    }
                    sw2 = !sw2;
                    continue;
                }

                if (sw2)
                {
                    att = line.Split(new char[] { '"' });


                    (instance.GetTeil(Convert.ToInt32(att[7])) as KTeil).ErwarteteBestellung = Convert.ToInt32(att[9]);
                }


                if (line == "<waitinglistworkstations>" || line == "</waitinglistworkstations>" || line == "<waitingliststock>" || line == "</waitingliststock>")
                {
                    sw3 = !sw3;
                    continue;
                }

                if (sw3)
                {
                    att = line.Split(new char[] { '"' });

                    if (att[0] == "<workplace id=")
                    {
                        ap = instance.GetArbeitsplatz(Convert.ToInt32(att[1]));
                    }

                    if (att[0] == "<waitinglist period=")
                    {
                        ap.AddWarteschlange(Convert.ToInt32(att[9]), Convert.ToInt32(att[13]), true);
                    }
                }


                if (line == "<ordersinwork>" || line == "</ordersinwork>")
                {
                    sw4 = !sw4;
                    continue;
                }

                if (sw4)
                {
                    att = line.Split(new char[] { '"' });

                    ap = instance.GetArbeitsplatz(Convert.ToInt32(att[1]));
                    ap.AddWarteschlange(Convert.ToInt32(att[9]), Convert.ToInt32(att[13]), true);

                }

            }

            file.Close();

        }



        /// WriteInput Methode
        /// schreibst die input.xml zum Einlesen in SCSIM
        public static void WriteInput()
        {
            //Dateianfang
            WriteFile("<PeriodInput>");
            // WriteFile("<qualitycontrol type=\"no\" losequantity=\"0\" delay=\"0\" />");
            WriteFile("<SalesWishes>");
            WriteFile("<SalesWish>");
            WriteFile("<SalesItemInternalNumber>\"1\"</SalesItemInternalNumber>");
            WriteFile("<SalesQuantity>" + (instance.GetTeil(1) as ETeil).Produktionsmenge + "</SalesQuantity>");
            WriteFile("<DirectSaleQuantity>0</DirectSaleQuantity>");
            WriteFile("<DirectSalePrice>0.0</DirectSalePrice>");
            WriteFile("<DirectSalePenalty>0.0</DirectSalePenalty>");
            WriteFile("</SalesWish>");
            WriteFile("<SalesWish>");
            WriteFile("<SalesItemInternalNumber>2</SalesItemInternalNumber>");
            WriteFile("<SalesQuantity>" + (instance.GetTeil(2) as ETeil).Produktionsmenge + "</SalesQuantity>");
            WriteFile("<DirectSaleQuantity>0</DirectSaleQuantity>");
            WriteFile("<DirectSalePrice>0.0</DirectSalePrice>");
            WriteFile("<DirectSalePenalty>0.0</DirectSalePenalty>");
            WriteFile("</SalesWish>");
            WriteFile("<SalesWish>");
            WriteFile("<SalesItemInternalNumber>2</SalesItemInternalNumber>");
            WriteFile("<SalesQuantity>" + (instance.GetTeil(3) as ETeil).Produktionsmenge + "</SalesQuantity>");
            WriteFile("<DirectSaleQuantity>0</DirectSaleQuantity>");
            WriteFile("<DirectSalePrice>0.0</DirectSalePrice>");
            WriteFile("<DirectSalePenalty>0.0</DirectSalePenalty>");
            WriteFile("</SalesWish>");
            WriteFile("</SalesWishes>");


            //Bestellungen
            WriteFile("<ItemOrders>");

            foreach (Bestellposition bp in instance.Bestellung)
            {
                WriteFile("<ItemOrder>");
                WriteFile("<ItemInternalNumber>" + bp.Kaufteil.Nummer + "</ItemInternalNumber>");
                WriteFile("<Quantity>" + bp.Menge + "</Quantity>");
                WriteFile("<Supplier>" + bp.OutputEil + "</Supplier>");
                WriteFile("</ItemOrder>");
            }

            WriteFile("</ItemOrders>");


            //Produktionsaufträge
            WriteFile("<ProductionOrders>");

            foreach (int z in instance.Reihenfolge)
            {
                WriteFile("<ProductionOrder>");
                WriteFile("<ItemInternalNumber>" + z + "</ItemInternalNumber>");
                WriteFile("<Quantity>" + (instance.GetTeil(z) as ETeil).Produktionsmenge + "</Quantity>");
                WriteFile("</ProductionOrder>");
            }

            WriteFile("</ProductionOrders>");

            //Workingtimelist
            WriteFile("<WorkplaceShifts>");

            for (int i = 1; i <= 15; i++)
            {
                if (i == 5)
                {
                    i++;
                }

                WriteFile("<WorkplaceShift>");
                WriteFile("<WorkplaceName>" + i + "</WorkplaceName>");
                WriteFile("<Shifts>" + instance.GetArbeitsplatz(i).Schichten + "</Shifts>");
                WriteFile("<OvertimeInMinutes>" + instance.GetArbeitsplatz(i).UeberMin + "</OvertimeInMinutes>");
                WriteFile("</WorkplaceShift>");

            }

            WriteFile("</WorkplaceShifts>");

            WriteFile("</PeriodInput>");
        }


        /// <summary>
        /// WriteFile Methode
        /// Hilfsmethode zum schreiben von Zeilen in eine Datei
        /// </summary>
        /// <param name="Inhalt">Inhalt der Datei</param>
        /// <param name="Name">Dateiname</param>
        private static void WriteFile(string Inhalt)
        {
            StreamWriter datei;

            datei = File.AppendText(DataContainer.Instance.SaveFile);
            datei.WriteLine(Inhalt);
            datei.Close();
        }
    }
}
