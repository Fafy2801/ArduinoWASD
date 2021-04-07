using System;
using System.IO.Ports;
using WindowsInput;
using System.Threading;
using WindowsInput.Native;
using System.Collections.Generic;

namespace ArduinoWASD
{
    class Program
    {
        static SerialPort arduino;
        static InputSimulator sim;
        static readonly VirtualKeyCode[] keys = {
            VirtualKeyCode.VK_W,
            VirtualKeyCode.VK_A,
            VirtualKeyCode.VK_S,
            VirtualKeyCode.VK_D,
        };

        static List<bool> last = new List<bool>();
        static void Main(string[] args)
        {

            for (int i = 0; i < keys.Length; i++)
                last.Add(false);
            // Get COM
            PortInit:
            Console.WriteLine("Input COM port:");

            string port = Console.ReadLine();
            // Open port
            try
            {
                if(port.Length == 1)
                    arduino = new SerialPort("COM" + port);
                else
                    arduino = new SerialPort(port);
                arduino.Open();

                Console.WriteLine("Using port: " + arduino.PortName);
            }
            // Retry
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
                Console.WriteLine();

                goto PortInit;
            }

            sim = new InputSimulator();

            // Loop
            new Thread(() =>
            {
                while(true)
                {
                    Loop();
                    Thread.Sleep(1);
                }
            }).Start();
        }

        static void Loop()
        {
            int inputs = arduino.ReadByte();

            for(int i = 0; i < keys.Length; i++)
			{
                if ((inputs & (1 << i)) != 0 && !last[i])
				{
                    sim.Keyboard.KeyDown(keys[i]);
                    last[i] = true;
				}
                else if(last[i])
				{

                    sim.Keyboard.KeyUp(keys[i]);
                    last[i] = false;
				}
            }
        }
    }
}
