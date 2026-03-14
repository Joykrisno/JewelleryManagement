namespace JewelleryManagement.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal TotalPurchase { get; set; }
        public string LastPurchaseDate { get; set; }
        public string Status { get; set; }
        public string ProfileImage { get; set; }
        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public string MemberSince { get; set; }
    }
}
