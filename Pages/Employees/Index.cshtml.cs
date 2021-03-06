﻿using Teamber.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Teamber.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly Teamber.Data.TeamberContext _context;

        public IndexModel(Teamber.Data.TeamberContext context)
        {
            _context = context;
        }

        public string Login { get; set; }
        public string Manager { get; set; }


        public IList<Employee> Employee { get; set; }

        public async Task OnGetAsync()
        {
            Login = HttpContext.Session.GetString("username");
            Manager = HttpContext.Session.GetString("Manager");

            Employee = await _context.Employees.ToListAsync();
        }
    }
}
