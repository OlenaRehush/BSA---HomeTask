using System;
using System.Collections.Generic;

namespace HomeTask1.DAL.Models
{
    public partial class Query
    {
        public int Id { get; set; }
        public int cityId { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public System.DateTime time { get; set; }
    }
}
