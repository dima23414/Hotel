using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    internal class Warehouse
    {
        public int Id { get; set; }

        private int Product;
        public int product
        {
            get { return Product; }
            set { Product = value; }
        }

        private int WarehouseId;
        public int warehouseId
        {
            get { return WarehouseId; }
            set { WarehouseId = value; }
        }

        private float Quantity;
        public float quantity
        {
            get { return Quantity; }
            set { Quantity = value; }
        }

        public Warehouse() { }

        public Warehouse(int Product, int WarehouseId, float Quantity)
        {
            this.Product = Product;
            this.WarehouseId = WarehouseId;
            this.Quantity = Quantity;
        }
    }
}
