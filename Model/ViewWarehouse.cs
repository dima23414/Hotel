using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public class ViewWarehouse
    {

        public int Id { get; set; }

        private int Product;
        public int product
        {
            get { return Product; }
            set { Product = value; }
        }

        private string NameProduct;
        public string nameProduct
        {
            get { return NameProduct; }
            set { NameProduct = value; }
        }

        private float Quantity;
        public float quantity
        {
            get { return Quantity; }
            set { Quantity = value; }
        }

        private int ValueDir;
        public int valueDir
        {
            get { return ValueDir; }
            set { ValueDir = value; }
        }

        private string NameUnit;
        public string nameUnit
        {
            get { return NameUnit; }
            set { NameUnit = value; }
        }

        private int WarehouseId;
        public int warehouseId
        {
            get { return WarehouseId; }
            set { WarehouseId = value; }
        }

        public ViewWarehouse() { }

        public ViewWarehouse(int Product, string NameProduct, float Quantity, int ValueDir, string NameUnit, int WarehouseId)
        {
            this.Product = Product;
            this.NameProduct = NameProduct;
            this.Quantity = Quantity;
            this.ValueDir = ValueDir;
            this.NameUnit = NameUnit;
            this.WarehouseId = WarehouseId;
        }

    }
}
