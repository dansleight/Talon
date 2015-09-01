using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Test.Mvc.Models
{
    public class PersonModel
    {
        #region Properties

        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }

        #endregion

        #region Static Methods

        public static List<PersonModel> GetPeople()
        {
            string serialized = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/TestData.json"));
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<List<PersonModel>>(serialized);
        }

        #endregion
    }
}