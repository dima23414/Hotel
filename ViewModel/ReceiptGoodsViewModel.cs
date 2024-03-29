using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ViewModel
{
    public class ReceiptGoodsViewModel
    {
        public int id { get; set; }

        public int product { get; set; }

        public string nameProduct { get; set; }

        public int warehouseId { get; set; }

        public float quantity { get; set; }

        public string nameUnit { get; set; }

        public float arrivedGoods { get; set; }

        public ReceiptGoodsViewModel(ViewWarehouse item)
        {
            id = item.Id;
            product = item.product;
            nameProduct = item.nameProduct;
            warehouseId = item.warehouseId;
            quantity = item.quantity;
            nameUnit = item.nameUnit;
            arrivedGoods = 0;
        }
    }
}
