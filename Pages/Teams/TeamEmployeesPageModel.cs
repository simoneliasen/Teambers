﻿using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Pages.Teams
{
    public class TeamEmployeesPageModel : PageModel
    {

        public List<AssignedEmployeeData> AssignedEmployeeDataList;
        public List<AssignedQuestionnaireData> AssignedQuestionnaireDataList;
        public List<AssignedTeamCriteriaData> AssignedTeamCriteriaDataList;
        public List<AllCompetences> AllCompetencesDataList;

        public void PopulateAssignedEmployeeData(SchoolContext context,
                                               Team team)
        {
            var allEmployees = context.Employees;
            var teamEmployees = new HashSet<int>(
                team.EmpTeams.Select(c => c.EmployeeID));
            AssignedEmployeeDataList = new List<AssignedEmployeeData>();
            foreach (var employee in allEmployees)
            {
                AssignedEmployeeDataList.Add(new AssignedEmployeeData
                {
                    EmployeeID = employee.ID,
                    FullName = employee.FullName,
                    JobTitle = employee.JobTitle,
                    PersonalityType = employee.PersonalityType,
                    Assigned = teamEmployees.Contains(employee.ID)
                });
            }
        }

        public void PopulateAssignedQuestionnaireData(SchoolContext context,
                                              Team team)
        {
            var allQuestionnaires = context.Questionnaires;
            var teamQuestionnaires = new HashSet<int>(
                team.TeamQuestionnaires.Select(c => c.QuestionnaireID));
            AssignedQuestionnaireDataList = new List<AssignedQuestionnaireData>();
            foreach (var questionnaire in allQuestionnaires)
            {
                AssignedQuestionnaireDataList.Add(new AssignedQuestionnaireData
                {
                    QuestionnaireID = questionnaire.QuestionnaireID,
                    Title = questionnaire.Title,
                    Assigned = teamQuestionnaires.Contains(questionnaire.QuestionnaireID)
                });
            }
        }

        public void PopulateAllCompetencesData(SchoolContext context,
                                               Team team)
        {
            var allCompetences = context.QuestionnaireCompetences; //rettet herfra idet, den skal indeholde alle competencer. ikke alle criterier. ret nedenfor også!
            var questionnaireCompetences = new HashSet<int>(
                team.TeamQuestionnaires.Select(c => c.QuestionnaireID));


           AllCompetencesDataList = new List<AllCompetences>();
            foreach (var competence in allCompetences)
            {
                AllCompetencesDataList.Add(new AllCompetences
                    {
                        QuestionnaireID = competence.QuestionnaireID,
                        QuestionnaireCompetenceID = competence.QuestionnaireCompetenceID,
                        Criteria = competence.Competence,
                        Assigned = questionnaireCompetences.Contains(competence.QuestionnaireID)
                    });
                


            }
        }

        public void PopulateAssignedTeamCriteriaData(SchoolContext context,
                                               Team team)
        {
            var questionnaireCompetences = new HashSet<int>(
                team.TeamQuestionnaires.Select(c => c.QuestionnaireID));

            

            var allCriterias = context.TeamCriterias;


            AssignedTeamCriteriaDataList = new List<AssignedTeamCriteriaData>();
            foreach (var criteria in allCriterias)
            {
                var questionnaireID = context.QuestionnaireCompetences.Where(i => i.QuestionnaireCompetenceID == criteria.QuestionnaireCompetenceID).Select(k => k.QuestionnaireID).FirstOrDefault();
                var criteriaName = context.QuestionnaireCompetences.Where(i => i.QuestionnaireCompetenceID == criteria.QuestionnaireCompetenceID).Select(k => k.Competence).FirstOrDefault();

                if (criteria.TeamID == team.TeamID) //easy?? 
                {
                    AssignedTeamCriteriaDataList.Add(new AssignedTeamCriteriaData
                    {
                        QuestionnaireID = questionnaireID,
                        Criteria = criteriaName,
                        Assigned = true,
                        Priority = allCriterias.Where(i => i.TeamID == team.TeamID).Where(j => j.QuestionnaireCompetenceID == criteria.QuestionnaireCompetenceID).FirstOrDefault().PriorityValue, //cirkemde med questionnairecompetence id i stedet for team id.
                        QuestionnaireCompetenceID = criteria.QuestionnaireCompetenceID
                    });
                }
            }
        }


        public void UpdateTeamQuestionnaires(SchoolContext context,
            string[] selectedQuestionnaires, Team teamToUpdate)
        {
            if (selectedQuestionnaires == null)
            {
                teamToUpdate.TeamQuestionnaires = new List<TeamQuestionnaire>();
                return;
            }

            var selectedQuestionnairesHS = new HashSet<string>(selectedQuestionnaires);
            var teamQuestionnaires = new HashSet<int>
                (teamToUpdate.TeamQuestionnaires.Select(c => c.Questionnaire.QuestionnaireID));
            foreach (var questionnaire in context.Questionnaires)
            {
                if (selectedQuestionnairesHS.Contains(questionnaire.QuestionnaireID.ToString()))
                {
                    if (!teamQuestionnaires.Contains(questionnaire.QuestionnaireID))
                    {
                        teamToUpdate.TeamQuestionnaires.Add(
                            new TeamQuestionnaire
                            {
                                TeamID = teamToUpdate.TeamID,
                                QuestionnaireID = questionnaire.QuestionnaireID
                            });
                    }
                }
                else
                {
                    if (teamQuestionnaires.Contains(questionnaire.QuestionnaireID))
                    {
                        TeamQuestionnaire questionnaireToRemove
                            = teamToUpdate
                                .TeamQuestionnaires
                                .SingleOrDefault(i => i.QuestionnaireID == questionnaire.QuestionnaireID);
                        context.Remove(questionnaireToRemove);
                    }
                }
            }
        }

        public void UpdateTeamEmployees(SchoolContext context,
            string[] selectedEmployees, Team teamToUpdate)
        {
            if (selectedEmployees == null)
            {
                teamToUpdate.EmpTeams = new List<EmpTeam>();
                return;
            }

            var selectedEmployeesHS = new HashSet<string>(selectedEmployees);
            var teamEmployees = new HashSet<int>
                (teamToUpdate.EmpTeams.Select(c => c.Employee.ID));
            foreach (var employee in context.Employees)
            {
                if (selectedEmployeesHS.Contains(employee.ID.ToString()))
                {
                    if (!teamEmployees.Contains(employee.ID))
                    {
                        teamToUpdate.EmpTeams.Add(
                            new EmpTeam
                            {
                                TeamID = teamToUpdate.TeamID,
                                EmployeeID = employee.ID
                            });
                    }
                }
                else
                {
                    if (teamEmployees.Contains(employee.ID))
                    {
                        EmpTeam employeeToRemove
                            = teamToUpdate
                                .EmpTeams
                                .SingleOrDefault(i => i.EmployeeID == employee.ID);
                        context.Remove(employeeToRemove);
                    }
                }
            }
        }


        //Det her er helt forkert. Fordi det vil tjekke om der er tilføjet flere osv. Men det er der jo ikke
        //Med denne metode kan der højst skiftes value på PriorityValue.
        public void UpdateTeamCriterias(int[] selectedCompetenceValues, Team teamToUpdate)
        {

            int i = 0;
            foreach (var criteria in teamToUpdate.TeamCriterias)
            {
                criteria.PriorityValue = selectedCompetenceValues[i];
                i++;
            }
        }


    }
}
