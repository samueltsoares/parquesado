using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace parquessadoCidade1
{
    class Program
    {
    	
    	
        static void Main(string[] args)
        {
      	
        byte[] data1;
        int recv;
        string hello;
        
		        Socket servidor;
            Socket funcionarios = new Socket(AddressFamily.InterNetwork,
SocketType.Stream, ProtocolType.Tcp);
        		IPEndPoint ieser = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6100);
        
                	servidor = new Socket(AddressFamily.InterNetwork,
SocketType.Stream, ProtocolType.Tcp);

            try
            {
                servidor.Connect(ieser);
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
                return;
            }

			data1 = new byte[1024];
       		recv = servidor.Receive(data1);
        	hello = Encoding.ASCII.GetString(data1, 0, recv);
        	Console.WriteLine(hello);
        
        
       
        	
        	
        	
        	IPAddress ip = IPAddress.Any;
        
            IPEndPoint ie = new IPEndPoint(ip, 6000);
            

            funcionarios.Bind(ie);

            funcionarios.Listen(10);
            Console.WriteLine("..:: ParquesSado ::..");
            Console.WriteLine("A espera...");

        
            
            byte[] data2;
             
            			data2 = new byte[1024];


		            	data2 = Encoding.ASCII.GetBytes("cidade 1");

                        servidor.Send(data2);
            
            

            while (true){

            Socket aceptFuncionario = funcionarios.Accept();

            TrataCliente trata = new TrataCliente(aceptFuncionario, servidor);

            Thread newthread = new Thread(new ThreadStart (trata.trataligacao));
            
            Thread writer= new Thread(new ThreadStart(trata.writer));
            
      
                newthread.Start();
                writer.Start();
                
             
            }
        }
    }
}
