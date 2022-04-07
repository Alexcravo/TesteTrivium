namespace TesteTrivium.Entities
{
    public class CompraItem
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public virtual Compra Compra { get; set; }
        public virtual Produto Produto { get; set; }
    }
}