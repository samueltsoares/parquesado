using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;


namespace parquessadoCidade1
{
    public class TrataCliente
    {

        private Socket servidor;
    	
    	
        Socket funcionario;
        IPEndPoint ie2;
        static Socket[]  lSocket = new Socket[20];
        static int indice = 0;
        static int ligacoes=0;
        private byte[] data;
        private string hello;
        private int r_nickname;
        private string nickname;
        private string answer;
        private string zona;
        private int recv;
        private string mensagem;
        private Random rand = new Random();
        int rdm;
        public TrataCliente(Socket s, Socket t)
        {
            funcionario = s;
            lSocket[indice++] = s;
            servidor=t;
        }

        public void trataligacao()
        {
       	data=new byte[1024];
        hello=null;
        r_nickname=0;
        nickname=null;
        answer=null;
        recv=0;
        mensagem=null;
        	//as matriculas da cidade
        	Matricula [] matriculas= new Matricula[8];
        	matriculas[0]=new Matricula("zona 1", "12-56-mq");
        	matriculas[1]=new Matricula("zona 1", "30-31-sm");
        	matriculas[2]=new Matricula("zona 2", "39-33-aa");
        	matriculas[3]=new Matricula("zona 2", "90-19-zz");
        	matriculas[4]=new Matricula("zona 3", "01-05-mm");
        	matriculas[5]=new Matricula("zona 3", "17-15-rr");
        	matriculas[6]=new Matricula("zona 1", "08-05-rm");
        	matriculas[7]=new Matricula("zona 1", "11-50-gd");

            ie2 = (IPEndPoint)funcionario.RemoteEndPoint;
            Console.WriteLine("Cliente " + ie2.Address.ToString() + "connected");
            Console.WriteLine("Porto " + ie2.Port.ToString());
            ligacoes++;


           data = new byte[1024];
            hello = "Bem vindo funcionario!";
            data = Encoding.ASCII.GetBytes(hello);
            
            funcionario.Send(data);     
            
            //Receber nickname
             
            do{
           	data = new byte[1024];
            r_nickname = funcionario.Receive(data);
            nickname = Encoding.ASCII.GetString(data, 0, r_nickname);
            Console.WriteLine("O funcionario " + nickname + " esta a tentar ligar-se a cidade " + ligacoes + "." );       
             
            data = new byte[1024];


		    data = Encoding.ASCII.GetBytes(nickname);

            servidor.Send(data);    
            
            data = new byte[1024];
            r_nickname=0;

			r_nickname = servidor.Receive(data);
          	answer = Encoding.ASCII.GetString(data, 0, r_nickname);
          	
          	data=Encoding.ASCII.GetBytes(answer);
          	funcionario.Send(data);
          	
          	Console.WriteLine(answer);
          	
        }
            
          	while(answer.Equals("nao"));

            //Enviar nome da cidade a sede

            
            data = new byte[1024];
            zona=null;
            servidor.Send(data);
            r_nickname=0;
			data = new byte[1024];
			r_nickname = servidor.Receive(data);
          	zona = Encoding.ASCII.GetString(data, 0, r_nickname);
                     
          	mensagem=null;
           	data = new byte[1024];
            Boolean pertence=false;
            do
            {        
                recv=0;
            	recv = funcionario.Receive(data);
                hello = Encoding.ASCII.GetString(data, 0, recv);
                

                if(hello.Equals("ordens")){
                	Console.WriteLine(nickname + " esta a consultar as ordens");
                	rdm=rand.Next(3)+1;
                	mensagem="Cidade 1 > Vai para a zona "+rdm;
                
                data = Encoding.ASCII.GetBytes(mensagem);

                funcionario.Send(data);
                
                }
               
                else if(hello.Length==8 && hello[2]==45 && hello[5]==45){
                	
                	Console.WriteLine(nickname + " verifica se a matricula "+hello+" e valida");
            	
            	for(int j=0; j<matriculas.Length; j++){
            		
            		if(hello.Equals(matriculas[j].getMatricula())){pertence=true;}
            	
            	}
                	
                	if(pertence==true){
                	
                	mensagem=hello + " > matricula valida";
                
                data = Encoding.ASCII.GetBytes(mensagem);

                funcionario.Send(data);
                		
                	}
                	else{
                		
                		mensagem=hello + " > matricula invalida - Multa 50 euros";
                
                data = Encoding.ASCII.GetBytes(mensagem);

                funcionario.Send(data);
                	
                	}
                }
                
                else{
                	Console.WriteLine(nickname +" > "+hello);
                mensagem=nickname + "> " + hello;
                
                data = Encoding.ASCII.GetBytes(mensagem);

                enviaMsgFunc(data);

                }

            } while (hello.CompareTo("/exit") != 0);


            ligacoes--;
            Console.WriteLine("Cliente " + ie2.Address.ToString() + "Desconectado");
            Console.WriteLine("O utilizador " + nickname + " desligou a sessão e ficaram " + ligacoes + " utilizador(es).");
            funcionario.Close();



            Console.ReadLine();

        }
        //para o servidor da cidade enviar informacoes para todos os funcionarios
                public void writer()
        {


            byte[] data = new byte[1024];
            string hello2;
            byte[] data2 = new byte[1024];
            
            do
            {
                hello2 = Console.ReadLine();
                data = Encoding.ASCII.GetBytes("Cidade 1 > "+hello2);
                for(int i=0; i< ligacoes; i++){
                	if(lSocket[i].Connected){
                		lSocket[i].Send(data);
                	}
                	//cliente.Send(data);
                }
            } while (hello2.CompareTo("exit") != 0);

            Console.ReadKey();




        }
                
                public void enviaMsgFunc(byte[]ms)
                {
                                 // Broadcast
                for (int i = 0; i < indice; i++)

                {
                    if (lSocket[i].Connected){

                        lSocket[i].Send(ms);
                    }
                }
                }
        
        

}
}
