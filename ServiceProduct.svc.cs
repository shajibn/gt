using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfRestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceProduct" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceProduct.svc or ServiceProduct.svc.cs at the Solution Explorer and start debugging.
    public class ServiceProduct : IServiceProduct
    {
        ProductDB1Entities db = new ProductDB1Entities();
        public bool Create(Product product)
        {
            try
            {
                db.Product.Add(product);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Product product)
        {
            try
            {
                Product pro = db.Product.Where(m => m.ID == product.ID).SingleOrDefault();
                db.Product.Remove(pro);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(Product product)
        {
            try
            {
                Product pro = db.Product.Where(m => m.ID == product.ID).SingleOrDefault();
                pro.Name = product.Name;
                pro.Category = product.Category;
                pro.Price = product.Price;
                pro.Description = product.Description;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Product find(string id)
        {
            int nID = Convert.ToInt32(id);
            return db.Product.Where(q => q.ID == nID).SingleOrDefault();
        }

        public List<Product> findAll()
        {
            return db.Product.ToList<Product>();
        }
    }
}
