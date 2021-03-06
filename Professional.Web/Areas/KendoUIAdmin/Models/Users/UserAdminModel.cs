﻿using AutoMapper;
using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Professional.Web.Areas.KendoUIAdmin.Models
{
    public class UserAdminModel : AdministrationViewModel, IMapFrom<User>
    {
        [HiddenInput]
        public object Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
    }
}