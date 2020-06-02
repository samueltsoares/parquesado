/*
 * Created by SharpDevelop.
 * User: samu
 * Date: 20-06-2011
 * Time: 04:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace parquessadoFuncionario2
{
  class Program
    {
    	static void Main(string[] args)
        {
    		
    		ThreadCom tc= new ThreadCom();
    		tc.comunicar();
    		
    	}
    	}

    }
