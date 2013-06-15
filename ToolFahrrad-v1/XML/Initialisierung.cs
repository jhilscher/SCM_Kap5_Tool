namespace ToolFahrrad_v1.XML
{
    class Initialisierung
    {
        // Array of flags to identificate KTeil
        private static bool[] _tkTeil =
            {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,
             false,false,false,true,true,true,true,true,false,true,true,false,false,false,true,true,true,true,true,
             true,true,true,true,true,true,true,true,true,true,true,true,false,false,false,true,true,false,false,false,
             true,true,true};
        // Array with ordering costs of each KTeil
        private static double[] _bkkTeil =
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,50,50,100,50,0,75,50,0,0,0,50,75,50,75,100,50,50,75,50,50,50,
             75,50,50,50,50,75,0,0,0,50,50,0,0,0,50,50,50};
        // Array with price of each KTeil
        private static double[] _pkTeil =
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5.00,6.50,6.50,0.06,0.06,0,0.1,1.2,0,0,0,0.75,22,0.1,1,8,1.5,1.5,
             1.5,2.5,0.06,0.1,5,0.5,0.06,0.1,3.5,1.5,0,0,0,22,0.1,0,0,0,22,0.1,0.15};
        // Array with usage indicator of each Teil (K=Kinderfahrrad, D=Damenfahrrad, H=Herrenfahrrad, KDH=all)
        private static string[] _vwTeil =
            {"K","D","H","K","D","H","K","D","H","K","D","H","K","D","H","KDH","KDH","K","D","H","K","D","H","KDH",
             "KDH","KDH","KDH","KDH","H","H","H","KDH","H","H","KDH","KDH","KDH","KDH","KDH","KDH","KDH","KDH","KDH",
             "KDH","KDH","KDH","KDH","KDH","K","K","K","K","K","D","D","D","D","D","KDH"};
        // Array with title of each Teil
        private static string[] _bez =
            {"Kinderfahrrad","Damenfahrrad","Herrenfahrrad","HinterradgruppeK","HinterradgruppeD","HinterradgruppeH",
             "VorderradgruppeK","VorderradgruppeD","VorderradgruppeH","SchutzblechK h","SchutzblechD h",
             "SchutzblechH h","SchutzblechK v","SchutzblechD v","SchutzblechH v","Lenker","Sattel","RahmenK","RahmenD",
             "RahmenH","KetteK","KetteD","KetteH","Mutter","Scheibe","Pedal","Schraube","Rohr","VorderradH",
             "Rahmen u. RäderH","Fahrrad o. PedalH","Farbe","FelgeH","SpeicheH","Nabe","Freilauf","Gabel","Welle",
             "Blech","Lenker","Mutter","Griff","Sattel","Stange","Mutter","Schraube","Zahnkranz","Pedal","VorderradK",
             "Rahmen u. RäderK","Fahrrad o. PedalK","FelgeK","SpeicheK","VorderradD","Rahmen u. RäderD",
             "Fahrrad o. PedalD","FelgeD","SpeicheD","Schweißdraht"};
        // Array with discount of each KTeil
        private static int[] _dmkTeil =
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,300,300,300,6100,3600,0,1800,4500,0,0,0,2700,900,22000,3600,900,
             900,300,1800,900,900,1800,2700,900,900,900,900,1800,0,0,0,600,22000,0,0,0,600,22000,1800};
        // Array with order duration of each KTeil
        private static double[] _bdkTeil =
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1.8,1.7,1.2,3.2,0.9,0,0.9,1.7,0,0,0,2.1,1.9,1.6,2.2,1.2,1.5,1.7,
             1.5,1.7,0.9,1.2,2,1,1.7,0.9,1.4,1,0,0,0,1.6,1.6,0,0,0,1.7,1.6,0.7};
        // Array with variance of the order duration of each KTeil
        private static double[] _abdkTeil =
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.4,0.4,0.2,0.3,0.2,0,0.2,0.4,0,0,0,0.5,0.5,0.3,0.4,0.1,0.3,0.4,
             0.3,0.2,0.2,0.3,0.5,0.2,0.3,0.3,0.1,0.2,0,0,0,0.4,0.2,0,0,0,0.3,0.5,0.2};
        // Get instance of Class DataContainer
        DataContainer _instance = DataContainer.Instance;
        // Public method to initialize each Teil
        public void Initialisieren()
        {
            // Create new object of class Teil
            for (int indexTeil = 0; indexTeil < 59; indexTeil++)
            {
                if (_tkTeil[indexTeil])
                {
                    _instance.NewTeil(indexTeil + 1, _bez[indexTeil], _pkTeil[indexTeil], _bkkTeil[indexTeil],
                                     _bdkTeil[indexTeil], _abdkTeil[indexTeil], _dmkTeil[indexTeil], 0, _vwTeil[indexTeil]);
                }
                else
                {
                    _instance.NewTeil(indexTeil + 1, _bez[indexTeil], 0, _vwTeil[indexTeil]);
                }
            }
            // Create new object of class Arbeitsplatz
            for (int indexAp = 1; indexAp < 16; indexAp++)
            {
                if (indexAp != 5)
                {
                    _instance.NeuArbeitsplatz(new Arbeitsplatz(indexAp));
                }
            }
            // Initialization of ETeil objects
            InitEteil();
            // Initialization of Arbeitsplatz objects
            InitializeArbPl();
        }
        // Initialization of each ETeil object
        private void InitEteil()
        {
            (_instance.GetTeil(1) as ETeil).Wert = 156.13;
            (_instance.GetTeil(2) as ETeil).Wert = 163.33;
            (_instance.GetTeil(3) as ETeil).Wert = 165.08;
            (_instance.GetTeil(4) as ETeil).Wert = 40.85;
            (_instance.GetTeil(5) as ETeil).Wert = 39.85;
            (_instance.GetTeil(6) as ETeil).Wert = 40.85;
            (_instance.GetTeil(7) as ETeil).Wert = 35.85;
            (_instance.GetTeil(8) as ETeil).Wert = 35.85;
            (_instance.GetTeil(9) as ETeil).Wert = 35.85;
            (_instance.GetTeil(10) as ETeil).Wert = 12.40;
            (_instance.GetTeil(11) as ETeil).Wert = 14.65;
            (_instance.GetTeil(12) as ETeil).Wert = 14.65;
            (_instance.GetTeil(13) as ETeil).Wert = 12.40;
            (_instance.GetTeil(14) as ETeil).Wert = 14.65;
            (_instance.GetTeil(15) as ETeil).Wert = 14.65;
            (_instance.GetTeil(16) as ETeil).Wert = 7.02;
            (_instance.GetTeil(17) as ETeil).Wert = 7.16;
            (_instance.GetTeil(18) as ETeil).Wert = 13.15;
            (_instance.GetTeil(19) as ETeil).Wert = 14.35;
            (_instance.GetTeil(20) as ETeil).Wert = 15.55;
            (_instance.GetTeil(26) as ETeil).Wert = 10.50;
            (_instance.GetTeil(29) as ETeil).Wert = 69.29;
            (_instance.GetTeil(30) as ETeil).Wert = 127.53;
            (_instance.GetTeil(31) as ETeil).Wert = 144.42;
            (_instance.GetTeil(49) as ETeil).Wert = 64.64;
            (_instance.GetTeil(50) as ETeil).Wert = 120.63;
            (_instance.GetTeil(51) as ETeil).Wert = 137.47;
            (_instance.GetTeil(54) as ETeil).Wert = 68.09;
            (_instance.GetTeil(55) as ETeil).Wert = 125.33;
            (_instance.GetTeil(56) as ETeil).Wert = 142.67;
            // -------------------------------------------------
            (_instance.GetTeil(1) as ETeil).IstEndProdukt = true;
            (_instance.GetTeil(2) as ETeil).IstEndProdukt = true;
            (_instance.GetTeil(3) as ETeil).IstEndProdukt = true;
            (_instance.GetTeil(16) as ETeil).AddBestandteil(24, 1);
            (_instance.GetTeil(16) as ETeil).AddBestandteil(28, 2);
            (_instance.GetTeil(16) as ETeil).AddBestandteil(40, 1);
            (_instance.GetTeil(16) as ETeil).AddBestandteil(41, 1);
            (_instance.GetTeil(16) as ETeil).AddBestandteil(42, 2);
            (_instance.GetTeil(17) as ETeil).AddBestandteil(43, 1);
            (_instance.GetTeil(17) as ETeil).AddBestandteil(44, 1);
            (_instance.GetTeil(17) as ETeil).AddBestandteil(45, 1);
            (_instance.GetTeil(17) as ETeil).AddBestandteil(46, 1);
            (_instance.GetTeil(26) as ETeil).AddBestandteil(47, 1);
            (_instance.GetTeil(26) as ETeil).AddBestandteil(44, 2);
            (_instance.GetTeil(26) as ETeil).AddBestandteil(48, 2);
            (_instance.GetTeil(1) as ETeil).AddBestandteil(21, 1);
            (_instance.GetTeil(1) as ETeil).AddBestandteil(24, 1);
            (_instance.GetTeil(1) as ETeil).AddBestandteil(27, 1);
            (_instance.GetTeil(1) as ETeil).AddBestandteil(26, 1);
            (_instance.GetTeil(1) as ETeil).AddBestandteil(51, 1);
            (_instance.GetTeil(56) as ETeil).AddBestandteil(24, 1);
            (_instance.GetTeil(56) as ETeil).AddBestandteil(27, 1);
            (_instance.GetTeil(56) as ETeil).AddBestandteil(55, 1);
            (_instance.GetTeil(56) as ETeil).AddBestandteil(16, 1);
            (_instance.GetTeil(56) as ETeil).AddBestandteil(17, 1);
            (_instance.GetTeil(55) as ETeil).AddBestandteil(24, 2);
            (_instance.GetTeil(55) as ETeil).AddBestandteil(25, 2);
            (_instance.GetTeil(55) as ETeil).AddBestandteil(5, 1);
            (_instance.GetTeil(55) as ETeil).AddBestandteil(11, 1);
            (_instance.GetTeil(55) as ETeil).AddBestandteil(54, 1);
            (_instance.GetTeil(11) as ETeil).AddBestandteil(32, 1);
            (_instance.GetTeil(11) as ETeil).AddBestandteil(39, 1);
            (_instance.GetTeil(5) as ETeil).AddBestandteil(35, 2);
            (_instance.GetTeil(5) as ETeil).AddBestandteil(36, 1);
            (_instance.GetTeil(5) as ETeil).AddBestandteil(57, 1);
            (_instance.GetTeil(5) as ETeil).AddBestandteil(58, 36);
            (_instance.GetTeil(54) as ETeil).AddBestandteil(24, 2);
            (_instance.GetTeil(54) as ETeil).AddBestandteil(25, 2);
            (_instance.GetTeil(54) as ETeil).AddBestandteil(8, 1);
            (_instance.GetTeil(54) as ETeil).AddBestandteil(14, 1);
            (_instance.GetTeil(54) as ETeil).AddBestandteil(19, 1);
            (_instance.GetTeil(8) as ETeil).AddBestandteil(35, 2);
            (_instance.GetTeil(8) as ETeil).AddBestandteil(37, 1);
            (_instance.GetTeil(8) as ETeil).AddBestandteil(38, 1);
            (_instance.GetTeil(8) as ETeil).AddBestandteil(57, 1);
            (_instance.GetTeil(8) as ETeil).AddBestandteil(58, 36);
            (_instance.GetTeil(19) as ETeil).AddBestandteil(28, 4);
            (_instance.GetTeil(19) as ETeil).AddBestandteil(32, 1);
            (_instance.GetTeil(19) as ETeil).AddBestandteil(59, 2);
            (_instance.GetTeil(14) as ETeil).AddBestandteil(32, 1);
            (_instance.GetTeil(14) as ETeil).AddBestandteil(39, 1);
            (_instance.GetTeil(2) as ETeil).AddBestandteil(22, 1);
            (_instance.GetTeil(2) as ETeil).AddBestandteil(24, 1);
            (_instance.GetTeil(2) as ETeil).AddBestandteil(27, 1);
            (_instance.GetTeil(2) as ETeil).AddBestandteil(26, 1);
            (_instance.GetTeil(2) as ETeil).AddBestandteil(56, 1);
            (_instance.GetTeil(51) as ETeil).AddBestandteil(16, 1);
            (_instance.GetTeil(51) as ETeil).AddBestandteil(17, 1);
            (_instance.GetTeil(51) as ETeil).AddBestandteil(50, 1);
            (_instance.GetTeil(51) as ETeil).AddBestandteil(24, 1);
            (_instance.GetTeil(51) as ETeil).AddBestandteil(27, 1);
            (_instance.GetTeil(50) as ETeil).AddBestandteil(24, 2);
            (_instance.GetTeil(50) as ETeil).AddBestandteil(25, 2);
            (_instance.GetTeil(50) as ETeil).AddBestandteil(4, 1);
            (_instance.GetTeil(50) as ETeil).AddBestandteil(10, 1);
            (_instance.GetTeil(50) as ETeil).AddBestandteil(49, 1);
            (_instance.GetTeil(10) as ETeil).AddBestandteil(32, 1);
            (_instance.GetTeil(10) as ETeil).AddBestandteil(39, 1);
            (_instance.GetTeil(4) as ETeil).AddBestandteil(35, 2);
            (_instance.GetTeil(4) as ETeil).AddBestandteil(36, 1);
            (_instance.GetTeil(4) as ETeil).AddBestandteil(52, 1);
            (_instance.GetTeil(4) as ETeil).AddBestandteil(53, 36);
            (_instance.GetTeil(49) as ETeil).AddBestandteil(24, 2);
            (_instance.GetTeil(49) as ETeil).AddBestandteil(25, 2);
            (_instance.GetTeil(49) as ETeil).AddBestandteil(7, 1);
            (_instance.GetTeil(49) as ETeil).AddBestandteil(13, 1);
            (_instance.GetTeil(49) as ETeil).AddBestandteil(18, 1);
            (_instance.GetTeil(7) as ETeil).AddBestandteil(35, 2);
            (_instance.GetTeil(7) as ETeil).AddBestandteil(37, 1);
            (_instance.GetTeil(7) as ETeil).AddBestandteil(38, 1);
            (_instance.GetTeil(7) as ETeil).AddBestandteil(52, 1);
            (_instance.GetTeil(7) as ETeil).AddBestandteil(53, 36);
            (_instance.GetTeil(18) as ETeil).AddBestandteil(28, 3);
            (_instance.GetTeil(18) as ETeil).AddBestandteil(32, 1);
            (_instance.GetTeil(18) as ETeil).AddBestandteil(59, 2);
            (_instance.GetTeil(13) as ETeil).AddBestandteil(32, 1);
            (_instance.GetTeil(13) as ETeil).AddBestandteil(39, 1);
            (_instance.GetTeil(3) as ETeil).AddBestandteil(23, 1);
            (_instance.GetTeil(3) as ETeil).AddBestandteil(24, 1);
            (_instance.GetTeil(3) as ETeil).AddBestandteil(27, 1);
            (_instance.GetTeil(3) as ETeil).AddBestandteil(26, 1);
            (_instance.GetTeil(3) as ETeil).AddBestandteil(31, 1);
            (_instance.GetTeil(31) as ETeil).AddBestandteil(24, 1);
            (_instance.GetTeil(31) as ETeil).AddBestandteil(27, 1);
            (_instance.GetTeil(31) as ETeil).AddBestandteil(16, 1);
            (_instance.GetTeil(31) as ETeil).AddBestandteil(17, 1);
            (_instance.GetTeil(31) as ETeil).AddBestandteil(30, 1);
            (_instance.GetTeil(30) as ETeil).AddBestandteil(24, 2);
            (_instance.GetTeil(30) as ETeil).AddBestandteil(25, 2);
            (_instance.GetTeil(30) as ETeil).AddBestandteil(6, 1);
            (_instance.GetTeil(30) as ETeil).AddBestandteil(12, 1);
            (_instance.GetTeil(30) as ETeil).AddBestandteil(29, 1);
            (_instance.GetTeil(12) as ETeil).AddBestandteil(32, 1);
            (_instance.GetTeil(12) as ETeil).AddBestandteil(39, 1);
            (_instance.GetTeil(6) as ETeil).AddBestandteil(35, 2);
            (_instance.GetTeil(6) as ETeil).AddBestandteil(36, 1);
            (_instance.GetTeil(6) as ETeil).AddBestandteil(33, 1);
            (_instance.GetTeil(6) as ETeil).AddBestandteil(34, 36);
            (_instance.GetTeil(29) as ETeil).AddBestandteil(24, 2);
            (_instance.GetTeil(29) as ETeil).AddBestandteil(25, 2);
            (_instance.GetTeil(29) as ETeil).AddBestandteil(9, 1);
            (_instance.GetTeil(29) as ETeil).AddBestandteil(15, 1);
            (_instance.GetTeil(29) as ETeil).AddBestandteil(20, 1);
            (_instance.GetTeil(9) as ETeil).AddBestandteil(35, 2);
            (_instance.GetTeil(9) as ETeil).AddBestandteil(37, 1);
            (_instance.GetTeil(9) as ETeil).AddBestandteil(38, 1);
            (_instance.GetTeil(9) as ETeil).AddBestandteil(33, 1);
            (_instance.GetTeil(9) as ETeil).AddBestandteil(34, 36);
            (_instance.GetTeil(20) as ETeil).AddBestandteil(28, 5);
            (_instance.GetTeil(20) as ETeil).AddBestandteil(32, 1);
            (_instance.GetTeil(20) as ETeil).AddBestandteil(59, 2);
            (_instance.GetTeil(15) as ETeil).AddBestandteil(32, 1);
            (_instance.GetTeil(15) as ETeil).AddBestandteil(39, 1);
            // -------------------------------------------------
            (_instance.GetTeil(26) as ETeil).AddArbeitsplatz(15);
            (_instance.GetTeil(26) as ETeil).AddArbeitsplatz(7);
            (_instance.GetTeil(16) as ETeil).AddArbeitsplatz(6);
            (_instance.GetTeil(16) as ETeil).AddArbeitsplatz(14);
            (_instance.GetTeil(17) as ETeil).AddArbeitsplatz(15);
            (_instance.GetTeil(1) as ETeil).AddArbeitsplatz(4);
            (_instance.GetTeil(56) as ETeil).AddArbeitsplatz(3);
            (_instance.GetTeil(55) as ETeil).AddArbeitsplatz(2);
            (_instance.GetTeil(11) as ETeil).AddArbeitsplatz(9);
            (_instance.GetTeil(11) as ETeil).AddArbeitsplatz(7);
            (_instance.GetTeil(11) as ETeil).AddArbeitsplatz(8);
            (_instance.GetTeil(11) as ETeil).AddArbeitsplatz(12);
            (_instance.GetTeil(11) as ETeil).AddArbeitsplatz(13);
            (_instance.GetTeil(5) as ETeil).AddArbeitsplatz(10);
            (_instance.GetTeil(5) as ETeil).AddArbeitsplatz(11);
            (_instance.GetTeil(54) as ETeil).AddArbeitsplatz(1);
            (_instance.GetTeil(8) as ETeil).AddArbeitsplatz(10);
            (_instance.GetTeil(8) as ETeil).AddArbeitsplatz(11);
            (_instance.GetTeil(19) as ETeil).AddArbeitsplatz(6);
            (_instance.GetTeil(19) as ETeil).AddArbeitsplatz(8);
            (_instance.GetTeil(19) as ETeil).AddArbeitsplatz(7);
            (_instance.GetTeil(19) as ETeil).AddArbeitsplatz(9);
            (_instance.GetTeil(14) as ETeil).AddArbeitsplatz(13);
            (_instance.GetTeil(14) as ETeil).AddArbeitsplatz(12);
            (_instance.GetTeil(14) as ETeil).AddArbeitsplatz(8);
            (_instance.GetTeil(14) as ETeil).AddArbeitsplatz(7);
            (_instance.GetTeil(14) as ETeil).AddArbeitsplatz(9);
            (_instance.GetTeil(2) as ETeil).AddArbeitsplatz(4);
            (_instance.GetTeil(51) as ETeil).AddArbeitsplatz(3);
            (_instance.GetTeil(50) as ETeil).AddArbeitsplatz(2);
            (_instance.GetTeil(10) as ETeil).AddArbeitsplatz(9);
            (_instance.GetTeil(10) as ETeil).AddArbeitsplatz(7);
            (_instance.GetTeil(10) as ETeil).AddArbeitsplatz(8);
            (_instance.GetTeil(10) as ETeil).AddArbeitsplatz(12);
            (_instance.GetTeil(10) as ETeil).AddArbeitsplatz(13);
            (_instance.GetTeil(4) as ETeil).AddArbeitsplatz(10);
            (_instance.GetTeil(4) as ETeil).AddArbeitsplatz(11);
            (_instance.GetTeil(49) as ETeil).AddArbeitsplatz(1);
            (_instance.GetTeil(7) as ETeil).AddArbeitsplatz(10);
            (_instance.GetTeil(7) as ETeil).AddArbeitsplatz(11);
            (_instance.GetTeil(18) as ETeil).AddArbeitsplatz(6);
            (_instance.GetTeil(18) as ETeil).AddArbeitsplatz(8);
            (_instance.GetTeil(18) as ETeil).AddArbeitsplatz(7);
            (_instance.GetTeil(18) as ETeil).AddArbeitsplatz(9);
            (_instance.GetTeil(13) as ETeil).AddArbeitsplatz(13);
            (_instance.GetTeil(13) as ETeil).AddArbeitsplatz(12);
            (_instance.GetTeil(13) as ETeil).AddArbeitsplatz(8);
            (_instance.GetTeil(13) as ETeil).AddArbeitsplatz(7);
            (_instance.GetTeil(13) as ETeil).AddArbeitsplatz(9);
            (_instance.GetTeil(3) as ETeil).AddArbeitsplatz(4);
            (_instance.GetTeil(31) as ETeil).AddArbeitsplatz(3);
            (_instance.GetTeil(30) as ETeil).AddArbeitsplatz(2);
            (_instance.GetTeil(12) as ETeil).AddArbeitsplatz(9);
            (_instance.GetTeil(12) as ETeil).AddArbeitsplatz(7);
            (_instance.GetTeil(12) as ETeil).AddArbeitsplatz(8);
            (_instance.GetTeil(12) as ETeil).AddArbeitsplatz(12);
            (_instance.GetTeil(12) as ETeil).AddArbeitsplatz(13);
            (_instance.GetTeil(6) as ETeil).AddArbeitsplatz(10);
            (_instance.GetTeil(6) as ETeil).AddArbeitsplatz(11);
            (_instance.GetTeil(29) as ETeil).AddArbeitsplatz(1);
            (_instance.GetTeil(9) as ETeil).AddArbeitsplatz(10);
            (_instance.GetTeil(9) as ETeil).AddArbeitsplatz(11);
            (_instance.GetTeil(20) as ETeil).AddArbeitsplatz(6);
            (_instance.GetTeil(20) as ETeil).AddArbeitsplatz(8);
            (_instance.GetTeil(20) as ETeil).AddArbeitsplatz(7);
            (_instance.GetTeil(20) as ETeil).AddArbeitsplatz(9);
            (_instance.GetTeil(15) as ETeil).AddArbeitsplatz(13);
            (_instance.GetTeil(15) as ETeil).AddArbeitsplatz(12);
            (_instance.GetTeil(15) as ETeil).AddArbeitsplatz(8);
            (_instance.GetTeil(15) as ETeil).AddArbeitsplatz(7);
            (_instance.GetTeil(15) as ETeil).AddArbeitsplatz(9);
        }
        // Initialization of each Arbeitsplatz object
        private void InitializeArbPl()
        {
            DataContainer dc = DataContainer.Instance;
            dc.GetArbeitsplatz(1).AddWerkzeit(49, 6);
            dc.GetArbeitsplatz(1).AddWerkzeit(54, 6);
            dc.GetArbeitsplatz(1).AddWerkzeit(29, 6);
            dc.GetArbeitsplatz(1).AddRuestzeit(49, 20);
            dc.GetArbeitsplatz(1).AddRuestzeit(54, 20);
            dc.GetArbeitsplatz(1).AddRuestzeit(29, 20);
            dc.GetArbeitsplatz(2).AddWerkzeit(50, 5);
            dc.GetArbeitsplatz(2).AddWerkzeit(55, 5);
            dc.GetArbeitsplatz(2).AddWerkzeit(30, 5);
            dc.GetArbeitsplatz(2).AddRuestzeit(50, 30);
            dc.GetArbeitsplatz(2).AddRuestzeit(55, 30);
            dc.GetArbeitsplatz(2).AddRuestzeit(30, 20);
            dc.GetArbeitsplatz(3).AddWerkzeit(51, 5);
            dc.GetArbeitsplatz(3).AddWerkzeit(56, 6);
            dc.GetArbeitsplatz(3).AddWerkzeit(31, 6);
            dc.GetArbeitsplatz(3).AddRuestzeit(51, 20);
            dc.GetArbeitsplatz(3).AddRuestzeit(56, 20);
            dc.GetArbeitsplatz(3).AddRuestzeit(31, 20);
            dc.GetArbeitsplatz(4).AddWerkzeit(1, 6);
            dc.GetArbeitsplatz(4).AddWerkzeit(2, 7);
            dc.GetArbeitsplatz(4).AddWerkzeit(3, 7);
            dc.GetArbeitsplatz(4).AddRuestzeit(1, 30);
            dc.GetArbeitsplatz(4).AddRuestzeit(2, 30);
            dc.GetArbeitsplatz(4).AddRuestzeit(3, 30);
            dc.GetArbeitsplatz(6).AddWerkzeit(16, 3);
            dc.GetArbeitsplatz(6).AddWerkzeit(18, 2);
            dc.GetArbeitsplatz(6).AddWerkzeit(19, 3);
            dc.GetArbeitsplatz(6).AddWerkzeit(20, 3);
            dc.GetArbeitsplatz(6).AddRuestzeit(16, 15);
            dc.GetArbeitsplatz(6).AddRuestzeit(18, 15);
            dc.GetArbeitsplatz(6).AddRuestzeit(19, 15);
            dc.GetArbeitsplatz(6).AddRuestzeit(20, 15);
            dc.GetArbeitsplatz(6).NaechsterArbeitsplatz[16] = 14;
            dc.GetArbeitsplatz(6).NaechsterArbeitsplatz[18] = 8;
            dc.GetArbeitsplatz(6).NaechsterArbeitsplatz[19] = 8;
            dc.GetArbeitsplatz(6).NaechsterArbeitsplatz[20] = 8;
            dc.GetArbeitsplatz(7).AddWerkzeit(13, 2);
            dc.GetArbeitsplatz(7).AddWerkzeit(18, 2);
            dc.GetArbeitsplatz(7).AddWerkzeit(26, 2);
            dc.GetArbeitsplatz(7).AddWerkzeit(10, 2);
            dc.GetArbeitsplatz(7).AddWerkzeit(14, 2);
            dc.GetArbeitsplatz(7).AddWerkzeit(19, 2);
            dc.GetArbeitsplatz(7).AddWerkzeit(11, 2);
            dc.GetArbeitsplatz(7).AddWerkzeit(15, 2);
            dc.GetArbeitsplatz(7).AddWerkzeit(20, 2);
            dc.GetArbeitsplatz(7).AddWerkzeit(12, 2);
            dc.GetArbeitsplatz(7).AddRuestzeit(13, 20);
            dc.GetArbeitsplatz(7).AddRuestzeit(18, 20);
            dc.GetArbeitsplatz(7).AddRuestzeit(26, 20);
            dc.GetArbeitsplatz(7).AddRuestzeit(10, 20);
            dc.GetArbeitsplatz(7).AddRuestzeit(14, 20);
            dc.GetArbeitsplatz(7).AddRuestzeit(19, 20);
            dc.GetArbeitsplatz(7).AddRuestzeit(11, 20);
            dc.GetArbeitsplatz(7).AddRuestzeit(15, 20);
            dc.GetArbeitsplatz(7).AddRuestzeit(20, 20);
            dc.GetArbeitsplatz(7).AddRuestzeit(12, 20);
            dc.GetArbeitsplatz(7).NaechsterArbeitsplatz[26] = 15;
            dc.GetArbeitsplatz(7).NaechsterArbeitsplatz[13] = 9;
            dc.GetArbeitsplatz(7).NaechsterArbeitsplatz[18] = 9;
            dc.GetArbeitsplatz(7).NaechsterArbeitsplatz[19] = 9;
            dc.GetArbeitsplatz(7).NaechsterArbeitsplatz[20] = 9;
            dc.GetArbeitsplatz(7).NaechsterArbeitsplatz[10] = 9;
            dc.GetArbeitsplatz(7).NaechsterArbeitsplatz[14] = 9;
            dc.GetArbeitsplatz(7).NaechsterArbeitsplatz[11] = 9;
            dc.GetArbeitsplatz(7).NaechsterArbeitsplatz[15] = 9;
            dc.GetArbeitsplatz(7).NaechsterArbeitsplatz[12] = 9;
            dc.GetArbeitsplatz(8).AddWerkzeit(13, 1);
            dc.GetArbeitsplatz(8).AddWerkzeit(18, 3);
            dc.GetArbeitsplatz(8).AddWerkzeit(10, 1);
            dc.GetArbeitsplatz(8).AddWerkzeit(14, 2);
            dc.GetArbeitsplatz(8).AddWerkzeit(19, 3);
            dc.GetArbeitsplatz(8).AddWerkzeit(11, 2);
            dc.GetArbeitsplatz(8).AddWerkzeit(15, 2);
            dc.GetArbeitsplatz(8).AddWerkzeit(20, 3);
            dc.GetArbeitsplatz(8).AddWerkzeit(12, 2);
            dc.GetArbeitsplatz(8).AddRuestzeit(13, 15);
            dc.GetArbeitsplatz(8).AddRuestzeit(18, 20);
            dc.GetArbeitsplatz(8).AddRuestzeit(10, 15);
            dc.GetArbeitsplatz(8).AddRuestzeit(14, 15);
            dc.GetArbeitsplatz(8).AddRuestzeit(19, 25);
            dc.GetArbeitsplatz(8).AddRuestzeit(11, 15);
            dc.GetArbeitsplatz(8).AddRuestzeit(15, 15);
            dc.GetArbeitsplatz(8).AddRuestzeit(20, 20);
            dc.GetArbeitsplatz(8).AddRuestzeit(12, 15);
            dc.GetArbeitsplatz(8).NaechsterArbeitsplatz[13] = 7;
            dc.GetArbeitsplatz(8).NaechsterArbeitsplatz[18] = 7;
            dc.GetArbeitsplatz(8).NaechsterArbeitsplatz[19] = 7;
            dc.GetArbeitsplatz(8).NaechsterArbeitsplatz[20] = 7;
            dc.GetArbeitsplatz(8).NaechsterArbeitsplatz[10] = 7;
            dc.GetArbeitsplatz(8).NaechsterArbeitsplatz[14] = 7;
            dc.GetArbeitsplatz(8).NaechsterArbeitsplatz[11] = 7;
            dc.GetArbeitsplatz(8).NaechsterArbeitsplatz[15] = 7;
            dc.GetArbeitsplatz(8).NaechsterArbeitsplatz[12] = 7;
            dc.GetArbeitsplatz(9).AddWerkzeit(13, 3);
            dc.GetArbeitsplatz(9).AddWerkzeit(18, 2);
            dc.GetArbeitsplatz(9).AddWerkzeit(10, 3);
            dc.GetArbeitsplatz(9).AddWerkzeit(14, 3);
            dc.GetArbeitsplatz(9).AddWerkzeit(19, 2);
            dc.GetArbeitsplatz(9).AddWerkzeit(11, 3);
            dc.GetArbeitsplatz(9).AddWerkzeit(15, 3);
            dc.GetArbeitsplatz(9).AddWerkzeit(20, 2);
            dc.GetArbeitsplatz(9).AddWerkzeit(12, 3);
            dc.GetArbeitsplatz(9).AddRuestzeit(13, 15);
            dc.GetArbeitsplatz(9).AddRuestzeit(18, 15);
            dc.GetArbeitsplatz(9).AddRuestzeit(10, 15);
            dc.GetArbeitsplatz(9).AddRuestzeit(14, 15);
            dc.GetArbeitsplatz(9).AddRuestzeit(19, 20);
            dc.GetArbeitsplatz(9).AddRuestzeit(11, 15);
            dc.GetArbeitsplatz(9).AddRuestzeit(15, 15);
            dc.GetArbeitsplatz(9).AddRuestzeit(20, 25);
            dc.GetArbeitsplatz(9).AddRuestzeit(12, 15);
            dc.GetArbeitsplatz(10).AddWerkzeit(7, 4);
            dc.GetArbeitsplatz(10).AddWerkzeit(4, 4);
            dc.GetArbeitsplatz(10).AddWerkzeit(8, 4);
            dc.GetArbeitsplatz(10).AddWerkzeit(5, 4);
            dc.GetArbeitsplatz(10).AddWerkzeit(9, 4);
            dc.GetArbeitsplatz(10).AddWerkzeit(6, 4);
            dc.GetArbeitsplatz(10).AddRuestzeit(7, 20);
            dc.GetArbeitsplatz(10).AddRuestzeit(4, 20);
            dc.GetArbeitsplatz(10).AddRuestzeit(8, 20);
            dc.GetArbeitsplatz(10).AddRuestzeit(5, 20);
            dc.GetArbeitsplatz(10).AddRuestzeit(9, 20);
            dc.GetArbeitsplatz(10).AddRuestzeit(6, 20);
            dc.GetArbeitsplatz(10).NaechsterArbeitsplatz[7] = 11;
            dc.GetArbeitsplatz(10).NaechsterArbeitsplatz[4] = 11;
            dc.GetArbeitsplatz(10).NaechsterArbeitsplatz[8] = 11;
            dc.GetArbeitsplatz(10).NaechsterArbeitsplatz[5] = 11;
            dc.GetArbeitsplatz(10).NaechsterArbeitsplatz[9] = 11;
            dc.GetArbeitsplatz(10).NaechsterArbeitsplatz[6] = 11;
            dc.GetArbeitsplatz(11).AddWerkzeit(7, 3);
            dc.GetArbeitsplatz(11).AddWerkzeit(4, 3);
            dc.GetArbeitsplatz(11).AddWerkzeit(8, 3);
            dc.GetArbeitsplatz(11).AddWerkzeit(5, 3);
            dc.GetArbeitsplatz(11).AddWerkzeit(9, 3);
            dc.GetArbeitsplatz(11).AddWerkzeit(6, 3);
            dc.GetArbeitsplatz(11).AddRuestzeit(7, 20);
            dc.GetArbeitsplatz(11).AddRuestzeit(4, 10);
            dc.GetArbeitsplatz(11).AddRuestzeit(8, 20);
            dc.GetArbeitsplatz(11).AddRuestzeit(5, 10);
            dc.GetArbeitsplatz(11).AddRuestzeit(9, 20);
            dc.GetArbeitsplatz(11).AddRuestzeit(6, 20);
            dc.GetArbeitsplatz(12).AddWerkzeit(13, 3);
            dc.GetArbeitsplatz(12).AddWerkzeit(10, 3);
            dc.GetArbeitsplatz(12).AddWerkzeit(14, 3);
            dc.GetArbeitsplatz(12).AddWerkzeit(11, 3);
            dc.GetArbeitsplatz(12).AddWerkzeit(15, 3);
            dc.GetArbeitsplatz(12).AddWerkzeit(12, 3);
            dc.GetArbeitsplatz(12).AddRuestzeit(13, 0);
            dc.GetArbeitsplatz(12).AddRuestzeit(10, 0);
            dc.GetArbeitsplatz(12).AddRuestzeit(14, 0);
            dc.GetArbeitsplatz(12).AddRuestzeit(11, 0);
            dc.GetArbeitsplatz(12).AddRuestzeit(15, 0);
            dc.GetArbeitsplatz(12).AddRuestzeit(12, 0);
            dc.GetArbeitsplatz(12).NaechsterArbeitsplatz[13] = 8;
            dc.GetArbeitsplatz(12).NaechsterArbeitsplatz[10] = 8;
            dc.GetArbeitsplatz(12).NaechsterArbeitsplatz[14] = 8;
            dc.GetArbeitsplatz(12).NaechsterArbeitsplatz[11] = 8;
            dc.GetArbeitsplatz(12).NaechsterArbeitsplatz[15] = 8;
            dc.GetArbeitsplatz(12).NaechsterArbeitsplatz[12] = 8;
            dc.GetArbeitsplatz(13).AddWerkzeit(13, 2);
            dc.GetArbeitsplatz(13).AddWerkzeit(10, 2);
            dc.GetArbeitsplatz(13).AddWerkzeit(14, 2);
            dc.GetArbeitsplatz(13).AddWerkzeit(11, 2);
            dc.GetArbeitsplatz(13).AddWerkzeit(15, 2);
            dc.GetArbeitsplatz(13).AddWerkzeit(12, 2);
            dc.GetArbeitsplatz(13).AddRuestzeit(13, 0);
            dc.GetArbeitsplatz(13).AddRuestzeit(10, 0);
            dc.GetArbeitsplatz(13).AddRuestzeit(14, 0);
            dc.GetArbeitsplatz(13).AddRuestzeit(11, 0);
            dc.GetArbeitsplatz(13).AddRuestzeit(15, 0);
            dc.GetArbeitsplatz(13).AddRuestzeit(12, 0);
            dc.GetArbeitsplatz(13).NaechsterArbeitsplatz[13] = 12;
            dc.GetArbeitsplatz(13).NaechsterArbeitsplatz[10] = 12;
            dc.GetArbeitsplatz(13).NaechsterArbeitsplatz[14] = 12;
            dc.GetArbeitsplatz(13).NaechsterArbeitsplatz[11] = 12;
            dc.GetArbeitsplatz(13).NaechsterArbeitsplatz[15] = 12;
            dc.GetArbeitsplatz(13).NaechsterArbeitsplatz[12] = 12;
            dc.GetArbeitsplatz(14).AddWerkzeit(16, 3);
            dc.GetArbeitsplatz(14).AddRuestzeit(16, 0);
            dc.GetArbeitsplatz(15).AddWerkzeit(17, 3);
            dc.GetArbeitsplatz(15).AddWerkzeit(26, 3);
            dc.GetArbeitsplatz(15).AddRuestzeit(17, 15);
            dc.GetArbeitsplatz(15).AddRuestzeit(26, 15);
        }
    }
}
