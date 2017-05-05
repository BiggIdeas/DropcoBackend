﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace Droplist.api.Models
{
    public class User : IdentityUser
	{
		public virtual Employee Employee { get; set; }
	}
}