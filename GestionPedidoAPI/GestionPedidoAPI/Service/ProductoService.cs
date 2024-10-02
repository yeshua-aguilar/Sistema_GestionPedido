using System;

public interface IProductoService
{
    void CreateProducto(Producto producto);
    void UpdateProducto(Producto producto);
    IEnumerable<Producto> GetProductos();
    Producto GetProductoById(int id);
    void DeleteProducto(int id);
}

public class ProductoService : IProductoService
{
    private readonly DbContext _dbContext;

    public ProductoService(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateProducto(Producto producto)
    {
        _dbContext.Productos.Add(producto);
        _dbContext.SaveChanges();
    }

    public void UpdateProducto(Producto producto)
    {
        _dbContext.Productos.Update(producto);
        _dbContext.SaveChanges();
    }

    public IEnumerable<Producto> GetProductos()
    {
        return _dbContext.Productos.ToList();
    }

    public Producto GetProductoById(int id)
    {
        return _dbContext.Productos.Find(id);
    }

    public void DeleteProducto(int id)
    {
        var producto = _dbContext.Productos.Find(id);
        if (producto != null)
        {
            _dbContext.Productos.Remove(producto);
            _dbContext.SaveChanges();
        }
    }
}
