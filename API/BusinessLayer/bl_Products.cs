using DataAccessLayer;

namespace BusinessLayer;

public class bl_Products
{
    public enum enMode
    {
        addNew = 0,
        Update = 1,
    }

    public enMode Mode = enMode.addNew;

    public ProductDTO PDTO
    {
        get
        {
            return (
                new ProductDTO(
                    this.ID,
                    this.name,
                    this.description,
                    this.categoryID,
                    this.price,
                    this.material,
                    this.gemstone,
                    this.carat,
                    this.clarity,
                    this.cut,
                    this.certification,
                    this.stock,
                    this.isFeatured,
                    this.createDate,
                    this.updateDate
                )
            );
        }
    }

    public int ID { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int categoryID { get; set; }
    public decimal price { get; set; }
    public string material { get; set; }
    public string gemstone { get; set; }
    public decimal carat { get; set; }
    public string clarity { get; set; }
    public string cut { get; set; }
    public string certification { get; set; }
    public int stock { get; set; }
    public bool isFeatured { get; set; }
    public DateTime createDate { get; set; }
    public DateTime updateDate { get; set; }

    // constructur
    public bl_Products(ProductDTO PDTO, enMode cMode = enMode.addNew)
    {
        this.ID = PDTO.ID;
        this.name = PDTO.name;
        this.description = PDTO.description;
        this.categoryID = PDTO.categoryID;
        this.price = PDTO.price;
        this.material = PDTO.material;
        this.gemstone = PDTO.gemstone;
        this.carat = PDTO.carat;
        this.clarity = PDTO.clarity;
        this.cut = PDTO.cut;
        this.certification = PDTO.certification;
        this.stock = PDTO.stock;
        this.isFeatured = PDTO.isFeatured;
        this.createDate = PDTO.createDate;
        this.updateDate = PDTO.updateDate;

        this.Mode = cMode;
    }

    public static bl_Products Find(int id)
    {
        ProductDTO PDTO = da_ProductsData.GetProductByID(id);

        if (PDTO != null)
        {
            return new bl_Products(PDTO, enMode.Update);
        }
        else
        {
            return null;
        }
    }

    public static List<ProductDTO> GetAllProducts()
    {
        return da_ProductsData.GetAllProducts();
    }

    public static ProductDTO GetProductByID(int id)
    {
        return da_ProductsData.GetProductByID(id);
    }

    private bool _AddNewProduct()
    {
        this.ID = da_ProductsData.AddNewProduct(PDTO);
        return (this.ID != -1);
    }

    private bool _UpdateProductByID()
    {
        return da_ProductsData.UpdateProductByID(PDTO);
    }

    public static bool DeleteProduct(int id)
    {
        return da_ProductsData.DeleteProductByID(id);
    }

    public bool Save()
    {
        switch (Mode)
        {
            case enMode.addNew:
                if (_AddNewProduct())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }
            case enMode.Update:
                return _UpdateProductByID();
        }

        return false;
    }
}
