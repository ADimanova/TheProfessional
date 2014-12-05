﻿using Professional.Models;
using Professional.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Professional.Web.Models.InputViewModels
{
    public class EndorsementInputModel : IMapFrom<EndorsementOfUser>
    {
        [Required]
        [Range(1, 10)]
        public int Value { get; set; }
        public string Comment { get; set; }
        public string EndorsedID { get; set; }
        public bool IsOfUser { get; set; }
    }
}