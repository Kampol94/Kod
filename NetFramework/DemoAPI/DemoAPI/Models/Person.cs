using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAPI.Models
{
    /// <summary>
    /// Represents one specific person
    /// </summary>
    public class Person
    {
        /// <summary>
        /// ID from SQL
        /// </summary>
        public int ID { get; set; } = 0;
        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; } = "";
        /// <summary>
        /// user last name
        /// </summary>
        public string LastName { get; set; } = "";
    }
}