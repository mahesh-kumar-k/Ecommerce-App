namespace WebApi.Models
{
    public partial class OrderCartModel
    {
        public Orders Order {get; set;}

        public int CartId {get; set;}

        public Carts Cart {get; set;}
    }
}
