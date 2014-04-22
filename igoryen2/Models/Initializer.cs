using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace igoryen2.Models {
    public class Initializer : DropCreateDatabaseAlways<DataContext> {

        private void InitializeIdentityForEF(DataContext dc) { 
        }
    }
}