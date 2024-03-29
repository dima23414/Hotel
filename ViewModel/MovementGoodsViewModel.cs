using Hotel.Model;
using Hotel.View;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hotel.ViewModel
{
    internal class MovementGoodsViewModel : INotifyPropertyChanged
    {
        public int id { get; set; }

        public int product { get; set; }

        public string nameProduct { get; set; }

        public int warehouseId { get; set; }

        public float quantity { get; set; }

        public string nameUnit { get; set; }

        public float arrivedGoods { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property, used to notify listeners.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public MovementGoodsViewModel(ViewWarehouse item)
        {
            id = item.Id;
            product = item.product;
            nameProduct = item.nameProduct;
            warehouseId = item.warehouseId;
            quantity = item.quantity;
            nameUnit = item.nameUnit;
            arrivedGoods = 0;
            MovementGoods_OnClick();
        }

        private ICollectionView _MovementItems;
        public ICollectionView MovementItems
        {
            get { return _MovementItems; }
            set
            {
                _MovementItems = value;
                OnPropertyChanged(nameof(MovementItems));
            }
        }

        public void MovementGoods_OnClick()
        {
            Hotel.AppContext db = new Hotel.AppContext();

            List<Movement> query = db.Movement.Where(obj => obj.Product == product && obj.WarehouseId == warehouseId).ToList();

            var userObservableCollection = new ObservableCollection<Movement>(query);

            MovementItems = CollectionViewSource.GetDefaultView(userObservableCollection);
        }
    }
}
