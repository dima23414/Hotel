using Hotel.Model;
using Hotel.View;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Hotel.ViewModel
{
    public class UsersPageWindowViewModel : INotifyPropertyChanged
    {
        object result;

        public UsersPageWindowViewModel()
        {
            Hotel.AppContext db = new Hotel.AppContext();

            Items = CollectionViewSource.GetDefaultView(db.Users.ToList());
            AddGoods = new RelayCommand(AddGoods_OnClick);

            MovementGoods = new RelayCommand(MovementGoods_OnClick);
        }

        private void ArrivedGoods(ReceiptGoodsViewModel DataContext, ViewWarehouse item, string Comment)
        {
            Hotel.AppContext db = new Hotel.AppContext();
            float Was = db.Warehouse.FirstOrDefault(obj => obj.product == DataContext.product && obj.warehouseId == DataContext.warehouseId).quantity;
            db.Warehouse.FirstOrDefault(obj => obj.product == DataContext.product && obj.warehouseId == DataContext.warehouseId).quantity += DataContext.arrivedGoods;
            item.quantity = db.Warehouse.FirstOrDefault(obj => obj.product == DataContext.product && obj.warehouseId == DataContext.warehouseId).quantity;

            db.Movement.Add(new Movement {
                Product = DataContext.product,
                WarehouseId = DataContext.warehouseId,
                Was = Was,
                Arrived = DataContext.arrivedGoods,
                Left = Was + DataContext.arrivedGoods,
                Comment = Comment,
                EventDate = DateTime.Now
            });

            db.SaveChanges();
        }

        public async void AddGoods_OnClick(object goods)
        {
            if (typeof(ViewWarehouse) != goods.GetType()) return;
            
            ViewWarehouse item = (ViewWarehouse)goods;

            var view = new ReceiptGoods
            {
                  DataContext = new ReceiptGoodsViewModel(item) {}
            };

            result = await DialogHost.Show(view, "RootDialog", null, ClosingEventHandler, ClosedEventHandler);
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
            if (!(bool)result) return;

            ArrivedGoods((ReceiptGoodsViewModel)view.DataContext,  item, "Одиночный приход товара" );

            ItemsWarehouse.Refresh();
        }

        public async void MovementGoods_OnClick(object goods)
        {
            Hotel.AppContext db = new Hotel.AppContext();

            if (typeof(ViewWarehouse) != goods.GetType()) return;

            ViewWarehouse item = (ViewWarehouse)goods;

            var view = new MovementGoods
            {
                DataContext = new MovementGoodsViewModel(item) { }
            };

            result = await DialogHost.Show(view, "RootDialog", null, null, null);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
             => Debug.WriteLine("You can intercept the closing event, and cancel here.");

        private void ClosedEventHandler(object sender, DialogClosedEventArgs eventArgs)
            => Debug.WriteLine("You can intercept the closed event here (1).");

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property, used to notify listeners.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        protected void OnWarehouseTextChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ICommand AddGoods {  get; set; }

        public ICommand MovementGoods { get; set; }

        public ICommand CloseDialogAddGoods { get; set; }

        public ICommand CloseDialogMovementGoods { get; set; }

        // private ICollectionView Items { get; set; }

        private ICollectionView _Items;
        public ICollectionView Items
        {
            get { return _Items; }
            set { _Items = value; 
            OnPropertyChanged(nameof(Items));
            }
        }

        private ICollectionView _ItemsWarehouse;
        public ICollectionView ItemsWarehouse
        {
            get { return _ItemsWarehouse; }
            set
            {
                _ItemsWarehouse = value;
                OnWarehouseTextChanged(nameof(ItemsWarehouse));
            }
        }

        /// <summary>
        /// Фильтр для таблицы пользователей
        /// </summary>
        private string _FilterText;
        public string FilterText
        {
            get { return _FilterText; }
            set 
            { 
                _FilterText = value; 
                OnPropertyChanged(nameof(FilterText));
                OnFilterChanged();
            }
        }

        private string _FilterWarehouse;
        public string FilterWarehouse
        {
            get { return _FilterWarehouse; }
            set
            {
                _FilterWarehouse = value;
                OnWarehouseTextChanged(nameof(FilterWarehouse));
                OnWarehouseFilterChanged();
            }
        }

        public int AddNewItemToDirectory(string NameItem, int Chapter, int valueDir = 0)
        {
            int result = 0;

            Hotel.AppContext db = new Hotel.AppContext();

            List<Model.Directory> directory = db.Directory.Where(obj => EF.Functions.Like(obj.Name, $"%{NameItem}%") && obj.Chapter == Chapter).ToList();

            if (directory.Count() == 0)
            {
                db.Directory.Add(new Model.Directory(NameItem.Trim(), Chapter, valueDir));
                db.SaveChanges();

                //   directory = db.Directory.Where(obj => obj.Name == NameItem && obj.Chapter == Chapter).ToList();
                directory = db.Directory.Where(obj => EF.Functions.Like(obj.Name, $"%{NameItem}%") && obj.Chapter == Chapter).ToList();
            }
            result = directory[0].Id;

            return result;
        }

        public string QueryTestLINQ(List<string> RowsNames, string QueryText)
        {
            List<string> chunks = QueryText.ToLower().Split(' ').ToList();

            string filter = "";

            int i1 = 0;

            foreach (string chunk in chunks)
            {
                if (chunk != "")
                {
                    if (i1 != 0)
                    {
                        filter = filter + " && ";
                    }

                    int i2 = 0;
                    foreach (string RowName in RowsNames)
                    {
                        if (i2 == 0)
                        {
                            filter = filter + "(";
                        }
                        else
                        {
                            filter = filter + " || ";
                        }

                        filter = filter + "(" + RowsNames[i2] + ".ToString().ToLower().Contains(\"" + chunk.Trim() + "\"))";

                        if (i2 == RowsNames.Count() - 1)
                        {
                            filter = filter + ")";
                        }

                        i2++;
                    }

                    i1++;
                }
            }

            if (filter == "")
            {
                filter = "id != null";
            }

            return filter;
        }

        private void OnFilterChanged()
        {
            Hotel.AppContext db = new Hotel.AppContext();
            Items = null;

            IQueryable<User> query = db.Users.Where(QueryTestLINQ(new List<string> { "Login" }, _FilterText));

            var userObservableCollection = new ObservableCollection<User>(query);

            Items = CollectionViewSource.GetDefaultView(userObservableCollection);
        }

        private void OnWarehouseFilterChanged()
        {
            Hotel.AppContext db = new Hotel.AppContext();
            ItemsWarehouse = null;

            IQueryable<ViewWarehouse> query = db.ViewWarehouse.Where(QueryTestLINQ(new List<string> { "nameProduct", "NameUnit" }, _FilterWarehouse)).OrderBy(p => p.nameProduct); 

            var userObservableCollection = new ObservableCollection<ViewWarehouse>(query);

            ItemsWarehouse = CollectionViewSource.GetDefaultView(userObservableCollection);
        }
    }
}
