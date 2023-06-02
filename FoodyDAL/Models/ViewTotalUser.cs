﻿using System;
using System.Collections.Generic;

#nullable disable

namespace FoodyDAL.Models
{
    public partial class ViewTotalUser
    {
        public string EMail { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public decimal? Phone { get; set; }
        public string UserType { get; set; }
        public int FkUserId { get; set; }
        public string OrgName { get; set; }
        public string OrgType { get; set; }
        public string OrgLocation { get; set; }
        public int IdUsers { get; set; }
    }
}
