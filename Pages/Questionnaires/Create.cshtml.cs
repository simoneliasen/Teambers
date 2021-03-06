﻿using Teamber.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teamber {
    public class CreateModel : PageModel {
        private readonly Teamber.Data.TeamberContext _context;

        public CreateModel(Teamber.Data.TeamberContext context)
        {
            _context = context;
        }

        public string Login { get; set; }
        public string Manager { get; set; }

        public IActionResult OnGet()
        {
            Login = HttpContext.Session.GetString("username");
            Manager = HttpContext.Session.GetString("Manager");
            return Page();
        }

        [BindProperty]
        public Questionnaire Questionnaire { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            string[] selectedQuestionnaireCompetences;

            //get competences from form, and add as string
            string tagOutput = Request.Form["myField"];
            Questionnaire.CompetencesString = tagOutput;

            if (Questionnaire.CompetencesString != null && Questionnaire.CompetencesString != "Questionnaire.CompetencesString")
            {
                selectedQuestionnaireCompetences = Questionnaire.CompetencesString.Split(", ");
            }
            else
            {
                selectedQuestionnaireCompetences = null;
            }

            var square = _context.QuestionnaireCompetences.Select(i => i.Competence);
            //square all competences saved in the database.

            var newQuestionnaire = new Questionnaire();
            if (selectedQuestionnaireCompetences != null)
            {
                newQuestionnaire.QuestionnaireCompetences = new List<QuestionnaireCompetence>();
                foreach (var competence in selectedQuestionnaireCompetences)
                {
                    if (!square.Contains(competence))
                    {
                        var competenceToAdd = new QuestionnaireCompetence
                        {
                            Competence = competence
                        };
                        newQuestionnaire.QuestionnaireCompetences.Add(competenceToAdd);
                    }
                }
            }

            await TryUpdateModelAsync<Questionnaire>(
                newQuestionnaire,
                "Questionnaire",
                i => i.Title);
            {

                _context.Questionnaires.Add(newQuestionnaire);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

        }
    }
}
