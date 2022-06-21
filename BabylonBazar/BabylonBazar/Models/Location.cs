﻿namespace BabylonBazar.Models {
	public class Location:BaseModel {
		public int UserId { get; set; }
		public string AddresLine1 { get; set; }
		public string? AddresLine2 { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Country { get; set; }
		public string County { get; set; }
		public string City { get; set; }
		public string ZipCode { get; set; }
	}
}
