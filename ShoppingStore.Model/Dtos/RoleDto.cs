﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Model.Dtos
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
