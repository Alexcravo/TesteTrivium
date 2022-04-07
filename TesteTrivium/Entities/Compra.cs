namespace TesteTrivium.Entities
{
    public class Compra
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
