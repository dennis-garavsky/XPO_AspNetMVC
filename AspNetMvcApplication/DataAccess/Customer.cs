﻿using System;
using DevExpress.Xpo;

namespace AspNetMvcApplication.DataAccess {

    public class Customer : XPObject {
        public Customer(Session session) : base(session) { }
        public string FirstName {
            get { return GetPropertyValue<string>(nameof(FirstName)); }
            set { SetPropertyValue(nameof(FirstName), value); }
        }
        public string LastName {
            get { return GetPropertyValue<string>(nameof(LastName)); }
            set { SetPropertyValue(nameof(LastName), value); }
        }
        [PersistentAlias("Concat([FirstName], ' ', [LastName])")]
        public string ContactName {
            get { return Convert.ToString(EvaluateAlias(nameof(ContactName))); }
        }
        [Association("CustomerOrders")]
        public XPCollection<Order> Orders {
            get { return GetCollection<Order>(nameof(Orders)); }
        }
    }

}
