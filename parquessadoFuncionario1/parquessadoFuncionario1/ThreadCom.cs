using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace parquessadoFuncionario1{
	public class ThreadCom
	{
		
		private Thread tReader;
        private Thread tWriter;

      	private string nicknameT;
      	private string nickname;
		
        private byte[] data1;
        private int recv;
        private string hello;

        private Socket cliente;

		
		
		public ThreadCom()
		{
			
		}
			


        public void comunicar()
        {
        	IPEndPoint ie = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000);
        	
        	cliente = new Socket(AddressFamily.InterNetwork,
SocketType.Stream, ProtocolType.Tcp);

            try
            {
                cliente.Connect(ie);
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
                return;
            }
            
            

			data1 = new byte[1024];
       		recv = cliente.Receive(data1);
        	hello = Encoding.ASCII.GetString(data1, 0, recv);
			Console.WriteLine(hello);
			
			
			

            byte[] data = new byte[1024];
            string answer;
            byte[] data2 = new byte[1024];
            int int_answer;
            do{
            	
            Console.WriteLine("Insira o seu username");
            nicknameT = Console.ReadLine();
            Console.WriteLine("O seu username é " + nicknameT + ".");
            data2 = Encoding.ASCII.GetBytes(nicknameT);
            cliente.Send(data2);
            int_answer=0;
            data = new byte[1024];
            int_answer = cliente.Receive(data);
            answer = Encoding.ASCII.GetString(data, 0 , int_answer);
            Console.WriteLine(answer);
            if (answer.Equals("nao")){
            Console.WriteLine("Username errado, tente novo");
            }
            }while(answer.Equals("nao"));
			
        	tReader = new Thread(new ThreadStart(reader));
        	tWriter = new Thread(new ThreadStart(writer));

            
            
            
			tWriter.Start();
            tReader.Start();

        }

        public void reader()
       {

            byte[] data1 = new byte[1024];
            int recv;
            string hello;
			

            do
            {
                recv = cliente.Receive(data1);
                hello = Encoding.ASCII.GetString(data1, 0, recv);

                Console.WriteLine(hello);
            } while (true);

        }
		
        public void writer()
        {


            byte[] data = new byte[1024];
            string hello2;
            byte[] data2 = new byte[1024];
            
            do
            {
                hello2 = Console.ReadLine();
                data = Encoding.ASCII.GetBytes(hello2);
                cliente.Send(data);
            } while (hello2.CompareTo("exit") != 0);

            tReader.Abort();
            cliente.Shutdown(SocketShutdown.Both);
            cliente.Close();

            Console.ReadKey();




        }
			
			
		
	}
}
