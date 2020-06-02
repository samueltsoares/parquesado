using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;



namespace parquessadoSede
{
    public class TrataCliente
    {
        Socket newSocket;
        IPEndPoint ie2;
        static Socket[]  lSocket = new Socket[20];
        static int indice = 0;
        static int ligacoes=0;
        private string mensagem;
		private string users;
		private string city;

        public TrataCliente(Socket s, string t)
        {
            newSocket = s;
            lSocket[indice++] = s;
            this.city=t;
        }

        public void trataligacao()
        {

            ie2 = (IPEndPoint)newSocket.RemoteEndPoint;
            Console.WriteLine("Cliente " + ie2.Address.ToString() + "connected");
            Console.WriteLine("Porto " + ie2.Port.ToString());
            ligacoes++;


while(true){

            //Enviar hello

            
			byte[] data1 = new byte[1024];
            byte[] msg = new byte[1024];
			int recv=0, iUsr=0;
			//os utilizadores da aplicação
			 Funcionario[]nomes= new Funcionario[7];
            nomes[0]=new Funcionario("zona 1", "samu", "cidade 1");
            nomes[1]=new Funcionario("zona 1", "sha", "cidade 1");
            nomes[2]=new Funcionario("zona 2", "timoteo", "cidade 1");
            nomes[3]=new Funcionario("zona 2", "brunob", "cidade 1");
            nomes[4]=new Funcionario("zona 3", "eva", "cidade 1");
            nomes[5]=new Funcionario("zona 1", "trindade", "cidade 2");
            nomes[6]=new Funcionario("zona 1", "paulo", "cidade 2");
			do{
            data1 = new byte[1024];
            msg = new byte[1024];
            recv=0;
            recv=newSocket.Receive(data1);
            users=Encoding.ASCII.GetString(data1, 0, recv);
            mensagem="nao";
            for(int i=0; i < nomes.Length; i++){
            	if(users.Equals(nomes[i].getNome())&&this.city.Equals(nomes[i].getCidade()))
            {
            	mensagem="sim";
            	iUsr=i;
            	break;
            }

            }
            msg = new byte[1024];
                msg = Encoding.ASCII.GetBytes(mensagem);

                      newSocket.Send(msg);
            
			}while(mensagem.Equals("nao"));
			
            newSocket.Receive(msg);
			msg = new byte[1024];
			
			msg = Encoding.ASCII.GetBytes(nomes[iUsr].getZona());

                      newSocket.Send(msg);
			
			
                      Console.WriteLine("Funcionario "+users+" conectado");
}

        }
    }
}
