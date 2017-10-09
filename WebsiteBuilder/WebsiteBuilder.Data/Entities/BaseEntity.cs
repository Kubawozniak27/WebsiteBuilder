﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Data.Entities
{
    public abstract partial class BaseEntity : BaseEntity<int> { }
    public abstract partial class BaseEntity<T>
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public T Id { get; set; }
    }
}