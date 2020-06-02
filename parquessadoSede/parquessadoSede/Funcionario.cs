/*
 * Created by SharpDevelop.
 * User: samu
 * Date: 16-06-2011
 * Time: 09:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace parquessadoSede
{
	/// <summary>
	/// Description of Funcionario.
	/// </summary>
	public class Funcionario
	{
		private string nome;
		private string cidade;
		private string zona;
		public Funcionario(string zona, string nome, string cidade)
		{
			this.zona=zona;
			this.nome=nome;
			this.cidade=cidade;
		}
		
		public string getNome(){
		return this.nome;
		}
		
		public string getCidade(){
		return this.cidade;
		}
		
		public string getZona(){
		return this.zona;
		}
		
		public void setZona(string s){
		this.zona=s;
		}
		
	}
}
