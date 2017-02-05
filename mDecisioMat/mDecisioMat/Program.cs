// Server
// Der Server wurde als Konsolenapplication implementiert und weist folgende Funktion auf:
// 
// 
// Martin Kitzwögerer
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace mDecisioMatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Erstelle den Server
            /// Die App.config enthält alle notwendigen Server Infos
            /// 

            ServiceHost cCcyServiceHost = new ServiceHost(typeof(mDecisioMat.RuleSyncProvider));

            // Open server
            cCcyServiceHost.Open();

            // Write to console
            Console.WriteLine();
            Console.WriteLine("Server is waiting for request from client!");
            Console.WriteLine();

            // Keep server open for requests until the server is closed by the user.
            Console.WriteLine("Press Enter to close the server!");
            Console.ReadLine();

            // Close server
            cCcyServiceHost.Close();
        }
    }
}
