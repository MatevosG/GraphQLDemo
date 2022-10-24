﻿using GraphQLDemo.API.Models;
using HotChocolate;
using System;
using System.Collections.Generic;

namespace GraphQLDemo.API.Schema.Queries
{
    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        [GraphQLNonNullType]
        public InstruktorType Instruktor { get; set; }
        public IEnumerable<StudentType> Students { get; set; }
    }
}
