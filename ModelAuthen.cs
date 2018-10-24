using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JWT.Authen
{
    public class ModelAuthen
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CompaniesId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string[] Role { get; set; }
        [Required]
        public int Staff_roleId { get; set; }

    }
    public class ModelAuthenCross
    {
        [Required]
        public int CId { get; set; }
        [Required]
        public string CCountry_code { get; set; }
        [Required]
        public string CPhone { get; set; }
        [Required]
        public string CFirstname { get; set; }
        [Required]
        public string CEmail { get; set; }
    }
}
