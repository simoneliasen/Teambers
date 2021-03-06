using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Teamber.Models;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace Teamber.Pages.Employees
{
    public class DashboardModel : EmployeeTeamsPageModel
    {
        private readonly Teamber.Data.TeamberContext _context;

        public DashboardModel(Teamber.Data.TeamberContext context)
        {
            _context = context;
        }

        public string Login { get; set; }
        public string Manager { get; set; }

        public Employee Employee { get; set; }
        public EmpQuestionnaire Questionnaire { get; set; }


        public List<int> Id = new List<int>();

        public async Task<IActionResult> OnGetAsync()
        {
            Login = HttpContext.Session.GetString("username");
            Manager = HttpContext.Session.GetString("Manager");

            var allUs = new List<Employee>(
            _context.Employees.Where(c => c.Username == Login));

            int? idUsername = allUs[0].ID;

            if (idUsername == null)
            {
                return NotFound();
            }

            var allquestionaire = new List<EmpQuestionnaire>(
           _context.EmpQuestionnaires.Where(c => c.EmployeeID == idUsername));

            for (int i = 0; i < allquestionaire.Count; i++)
            {
                Id.Add(i);
            }

            Employee = await _context.Employees
            .Include(s => s.EmpTeams)
            .ThenInclude(e => e.Team)
            .Include(x => x.EmpQuestionnaires)
            .ThenInclude(k => k.Questionnaire)
            .Include(i => i.EmployeeCompetences)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == idUsername);

            PopulateAssignedEmployeeCompetenceValuesData(_context, Employee);

            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
