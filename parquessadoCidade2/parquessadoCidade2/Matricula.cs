
	
/*
 * Created by SharpDevelop.
 * User: samu
 * Date: 16-06-2011
 * Time: 10:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;


namespace parquessadoCidade2
{
	/// <summary>
	/// Description of Matricula.
	/// </summary>
	public class Matricula
	{
		private string zona;
		private string matricula;
		private string dataLim;
		public Matricula(string zona, string matricula, string dataLim)
		{
			this.zona=zona;
			this.matricula=matricula;
			this.dataLim=dataLim;
		}
		
		public string getZona(){
		return this.zona;
		}
		
		public string getMatricula(){
		return this.matricula;
		}
		
		public string getDataLim(){
		return this.dataLim;
		}
	}
}

