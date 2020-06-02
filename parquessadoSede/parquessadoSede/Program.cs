using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace parquessadoSede
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork,
SocketType.Stream, ProtocolType.Tcp);

            IPAddress ip = IPAddress.Any;
        
            IPEndPoint ie = new IPEndPoint(ip, 6100);

            socket.Bind(ie);

            socket.Listen(10);
			Console.WriteLine("..:: ParquesSado ::..");
            Console.WriteLine("Em espera...");
            

            while (true){

            Socket newSocket = socket.Accept();
            
            byte[] data = new byte[1024];
            string hello = "Bem vindo cidade!";
            data = Encoding.ASCII.GetBytes(hello);
            
            newSocket.Send(data);

            //Receber nickname

            byte[] data2 = new byte[1024];
            string nickname;
            int r_nickname;

            r_nickname = newSocket.Receive(data2);
            nickname = Encoding.ASCII.GetString(data2, 0, r_nickname);
            Console.WriteLine("A cidade " + nickname + " ligou-se a sede" );

            TrataCliente trata = new TrataCliente(newSocket, nickname);

            Thread newthread = new Thread(new ThreadStart (trata.trataligacao));
            
            newthread.Start();
             
            }
        }
    }
}
