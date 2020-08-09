using System;
using System.Security.Cryptography.X509Certificates;

namespace ICT638July2020Group1Android.models
{
	public class House
	{
		public int id { get; set; }
		public string title { get; set; }
		public double weeklyRent { get; set; }
		public int numBedrooms { get; set; }
		public int numBathrooms { get; set; }
		public String Address { get; set; }
		public String longitude;
		public String latitude;
		public int AgentID { get; set; }
	}
}
