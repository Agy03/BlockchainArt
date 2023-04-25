using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;

namespace TESTINGproyectos
{
    class Program
    {
        static void Main()
        {
            while(true)
            {
                // Crea un objeto de la clase ManagementEventWatcher para recibir notificaciones de eventos del sistema
                ManagementEventWatcher watcher = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStartTrace");

                // Configura un evento para cuando se detecte el inicio de un proceso
                watcher.EventArrived += new EventArrivedEventHandler(ProcessStarted);

                // Inicia la escucha de eventos
                watcher.Start();

                // Espera a que se detecte un evento
                Console.ReadLine();

                // Detiene la escucha de eventos
                watcher.Stop();
            }
        }
        // Método que se ejecutará cuando se detecte el inicio de un proceso
        private static void ProcessStarted(object sender, EventArrivedEventArgs e)
        {
            // Nombres de las aplicaciones que deseas bloquear
            string[] applicationNames = { "Notepad", "nombre_de_tu_aplicacion2.exe", "nombre_de_tu_aplicacion3.exe" };

            // Obtiene todos los procesos en ejecución en el sistema
            Process[] processes = Process.GetProcesses();

            // Recorre todos los procesos en ejecución
            foreach (Process process in processes)
            {
                // Compara el nombre de cada proceso con los nombres de las aplicaciones que deseas bloquear
                foreach (string applicationName in applicationNames)
                {
                    if (process.ProcessName.ToLower() == applicationName.ToLower())
                    {
                        //DateTime startTime = process.StartTime;
                        // Calcula el tiempo que ha pasado desde que se inició el proceso
                        //TimeSpan elapsedTime = DateTime.Now - startTime;
                        //Console.WriteLine("El proceso ha estado activo durante " + elapsedTime.ToString());
                        //if (elapsedTime.TotalSeconds > 10)
                        //{
                            // Termina el proceso
                            process.Kill();
                        //}
                    }
                }
            }
        }
    }

}
