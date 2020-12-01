using System;
using System.IO.Ports;

namespace ArduinoWASD
{
    class Program
    {
        static SerialPort arduino;

        static void Main(string[] args)
        {
        // Get COM
        PortInit:
            Console.WriteLine("Input COM port:");

            string port = Console.ReadLine();
            // Open port
            try
            {
                if (port.Length == 1)
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
        }
    }
}
