using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using igoryen2.Models;

namespace igoryen2.ViewModels {
    public class RepositoryBase {

        protected DataContext dc;

        public RepositoryBase() {

            dc = new DataContext();

            dc.Configuration.ProxyCreationEnabled = false;
            dc.Configuration.LazyLoadingEnabled = false;
        }
    }
}