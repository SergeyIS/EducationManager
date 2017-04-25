﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationManager.Models.OperationRegistryData
{
    public class OperationRegistryUser
    {
        [Key]
        public int Id { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }
        public string Addres { get; set; }
        public int SchoolId { get; set; }
    }
}
