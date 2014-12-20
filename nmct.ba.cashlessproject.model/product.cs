﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class Product : IDataErrorInfo
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _productName;

        [Required(ErrorMessage = "De productnaam is verplicht")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "De productnaam moet tussen de 3 en 50 karakters bevatten ")]
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        private double _price;

        [Required(ErrorMessage = "De prijs is verplicht")]
        [Range(typeof(Decimal), "0", "10000", ErrorMessage = "De prijs moet tussen 0 en 10000 liggen")]
        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string Error
        {
            get { return null; }
        }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    
                    if (value != null)
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = columnName });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }
    }
}
