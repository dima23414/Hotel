using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    internal class Movement
    {
        /*CREATE TABLE "Movement" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Product"	INTEGER,
	"WarehouseId"	INTEGER,
	"Was"	REAL,
	"Arrived"	REAL,
	"Left"	REAL,
	"Comment"	TEXT, EventDate DATETIME,
	PRIMARY KEY("Id" AUTOINCREMENT)
)*/


        public int Id { get; set; }

        private int? product;
        public int? Product
        {
            get { return product; }
            set { product = value; }
        }

        private int? warehouseId;
        public int? WarehouseId
        {
            get { return warehouseId; }
            set { warehouseId = value; }
        }

        private float? was;
        public float? Was
        {
            get { return was; }
            set { was = value; }
        }

        private float? arrived;
        public float? Arrived
        {
            get { return arrived; }
            set { arrived = value; }
        }

        private float? left;
        public float? Left
        {
            get { return left; }
            set { left = value; }
        }

        private string? comment;
        public string? Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private DateTime? eventDate;
        public DateTime? EventDate
        {
            get { return eventDate; }
            set { eventDate = value; }
        }
        public Movement() { }

        public Movement(int product, int warehouseId, float was, float arrived, float left, string comment, DateTime eventDate)
        {
            this.Product = product;
            this.WarehouseId = warehouseId;
            this.Was = was;
            this.Arrived = arrived;
            this.Left = left;
            this.Comment = comment;
            this.EventDate = eventDate;
        }
    }
}
