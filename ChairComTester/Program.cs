using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChairComTester
{
    class Program
    {
        const int Max = 250;
        const int Min = 10;
        //const int Max = 240;
        //const int Min = 20;
        static void Main(string[] args)
        {
            JMDM_PortControl ChairPort = new JMDM_PortControl();
            Console.WriteLine("Hello to angelo's Chair Test program.\nPlease use the arrow keys.\nPress up to put chair up.\nPress down to put chair down.\nPress right to put the chair on a right tilt.\nPress left to put the chair on a left tilt.\nAdditionaly, Press R to reset and E to exit.");
            ChairPort.Open_Port("COM10");
            ChairPort.Reset_Zerp();
            Thread.Sleep(2000);
            ConsoleKey Key = Console.ReadKey().Key;
            while (Key != ConsoleKey.E)
            {
                switch (Key)
                {
                    case ConsoleKey.UpArrow:
                        ChairPort.Send_Data(1, Max);
                        ChairPort.Send_Data(2, Max);
                        ChairPort.Send_Data(3, Max);
                        break;
                    case ConsoleKey.DownArrow:
                        ChairPort.Send_Data(1, Min);
                        ChairPort.Send_Data(2, Min);
                        ChairPort.Send_Data(3, Min);
                        break;
                    case ConsoleKey.LeftArrow:
                        ChairPort.Send_Data(1, Min);
                        ChairPort.Send_Data(2, Max);
                        ChairPort.Send_Data(3, 175);
                        break;
                    case ConsoleKey.RightArrow:
                        ChairPort.Send_Data(2, Min);
                        ChairPort.Send_Data(1, Max);
                        ChairPort.Send_Data(3, 175);
                        break;
                    case ConsoleKey.W:
                        //ChairPort.Rotate_Control(250);
                        ChairPort.Send_Data(0, Max);
                        ChairPort.Send_Data(4, Max);
                        ChairPort.Send_Data(5, Max);
                        ChairPort.Send_Data(6, Max);
                        break;
                    case ConsoleKey.Q:
                        //ChairPort.Rotate_Control(10);
                        ChairPort.Send_Data(0, Min);
                        ChairPort.Send_Data(4, Min);
                        ChairPort.Send_Data(5, Min);
                        ChairPort.Send_Data(6, Min);
                        break;
                    case ConsoleKey.R:
                        ChairPort.Reset_Zerp();
                        break;
                    case ConsoleKey.C:
                        Loop(ChairPort);
                        break;
                    case ConsoleKey.Z:
                        WildLoop(ChairPort);
                        break;
                    case ConsoleKey.X:
                        SandBoxTilt2U(ChairPort, 1);
                        break;
                }
                Key = Console.ReadKey().Key;
            }
            ChairPort.Close_Port();
        }

        static void Loop(JMDM_PortControl ChairPort)
        {
            const int timing = 1000;
            bool Cont = true;
            while (Cont)
            {
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.E)
                    Cont = false;
                ChairPort.Send_Data(0, Max);
                ChairPort.Send_Data(1, Max);
                ChairPort.Send_Data(2, Max);
                ChairPort.Send_Data(3, Max);
                Thread.Sleep(timing);
                ChairPort.Send_Data(0, Max);
                ChairPort.Send_Data(1, Min);
                ChairPort.Send_Data(2, Min);
                ChairPort.Send_Data(3, Min);
                Thread.Sleep(timing);
            }

        }

        static void WildLoop(JMDM_PortControl ChairPort)
        {
            Random R = new Random();
            const int timing = 800;
            bool Cont = true;
            int RN = 0;
            while (Cont)
            {
                int RNN;
                do
                {
                    RNN = R.Next(0, 30);
                }
                while (!(RNN - RN > 10 || RNN - RN < -10));
                RN = RNN;
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.E)
                    Cont = false;
                if (RN >= 0 && RN <= 10)
                {
                    ChairPort.Send_Data(1, Max);
                    ChairPort.Send_Data(2, Min);
                    ChairPort.Send_Data(3, Min);
                    Thread.Sleep(timing);
                }
                else if (RN >= 10 && RN <= 20)
                {
                    ChairPort.Send_Data(1, Min);
                    ChairPort.Send_Data(2, Max);
                    ChairPort.Send_Data(3, Min);
                    Thread.Sleep(timing);
                }
                else if (RN >= 20 && RN <= 30)
                {
                    ChairPort.Send_Data(1, Min);
                    ChairPort.Send_Data(2, Min);
                    ChairPort.Send_Data(3, Max);
                    Thread.Sleep(timing);
                }
            }

        }

        static void SandBoxTilt2U(JMDM_PortControl ChairPort, int DropedPist)
        {
            int C1 = Max;
            int C2 = Max;
            int C3 = Max;

            switch (DropedPist)
            {
                case 1:
                    C1 = Min;
                    break;
                case 2:
                    C2 = Min;
                    break;
                case 3:
                    C3 = Min;
                    break;
            }

            ChairPort.Send_Data(1, C1);
            ChairPort.Send_Data(2, C2);
            ChairPort.Send_Data(3, C3);
        }
    }
}
