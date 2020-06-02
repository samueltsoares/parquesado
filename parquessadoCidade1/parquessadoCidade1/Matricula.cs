
	
/*
 * Created by SharpDevelop.
 * User: samu
 * Date: 16-06-2011
 * Time: 10:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;


namespace parquessadoCidade1
{

	public class Matricula
	{
		private string zona;
		private string matricula;
		public Matricula(string zona, string matricula)
		{
			this.zona=zona;
			this.matricula=matricula;
		}
		
		public string getZona(){
		return this.zona;
		}
		
		public string getMatricula(){
		return this.matricula;
		}
		
	}
}

