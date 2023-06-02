﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodyDAL.APIModels
{
    public class NewUserModel
    {
        public string EMail { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public decimal? Phone { get; set; }
        public int UserType { get; set; }
        public string Location { get; set; }
        public string OrgName { get; set; }
    }
}
