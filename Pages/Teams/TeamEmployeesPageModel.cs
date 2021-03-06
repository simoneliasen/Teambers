﻿using Teamber.Data;
using Teamber.Models;
using Teamber.Models.TeamberViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Teamber.Pages.Teams {
    public class TeamEmployeesPageModel : PageModel {

        public List<AssignedEmployeeData> AssignedEmployeeDataList;
        public List<AssignedQuestionnaireData> AssignedQuestionnaireDataList;
        public List<AssignedTeamCriteriaData> AssignedTeamCriteriaDataList;
        public List<AllCompetences> AllCompetencesDataList;

        //collects a lot of data for the employees for the algorithm. This is inserted as javascript distionairies on the page.
        public string AllEmpCompetencesString;
        public string AllEmpQuestionnairesString;
        public string AllQuestionnairesString;
        public string AllQuestionnaireTitlesString;

        //for the edit page
        public string teamMembers;

        //for team members on the team/index page
        public string teamMemberCount;

        public void PopulateTeamMemberCount(TeamberContext context)
        {
            var allTeamIDs = context.Teams.Select(i => i.TeamID);
            var allTeamMembers = context.EmpTeams;

            int[] memberCount = new int[allTeamIDs.Count()];

            var counter = 0;

            foreach (var teamID in allTeamIDs) //loops through all teams
            {

                foreach (var member in allTeamMembers)//loops through all empteam rows
                {
                    if (teamID == member.TeamID)
                    {
                        memberCount[counter]++;
                    }
                }
                counter++;

            }

            //converts to javascript string
            var result = "var teamMemberCount = [";
            foreach (var item in memberCount)
            {
                result += $"{item}, ";
            }
            result += "]; ";
            teamMemberCount = result;
        }



        public void PopulateTeamMembers(Team team)
        {

            var teamEmployees = team.EmpTeams;

            string result = "var TeamMembers = { ";

            foreach (var employee in teamEmployees) //loops through all employees
            {
                if (employee.questionnaireRole != null)
                {
                    result += $"{employee.EmployeeID.ToString()}: {employee.questionnaireRole}, ";//questionnaireRole is an id
                }
                else
                {
                    result += $"{employee.EmployeeID.ToString()}: 0, ";
                }


            }

            result += "}; ";

            teamMembers = result;

        }


        public void PopulateAssignedEmployeeData(TeamberContext context,
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

        public void PopulateAssignedQuestionnaireData(TeamberContext context,
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

        public void PopulateAllCompetencesData(TeamberContext context,
                                               Team team)
        {
            var allCompetences = context.QuestionnaireCompetences;
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

        public void PopulateAllEmpCompetencesString(TeamberContext context)
        {
            var allEmployeeIDs = context.Employees.Select(i => i.ID);
            var allEmployeeCompetences = context.EmployeeCompetences;

            string result = "var EmpCompetences = { ";

            foreach (var employeeID in allEmployeeIDs) //loops through all employees
            {
                result += $"{employeeID.ToString()}: {{ ";
                foreach (var employeeCompetence in allEmployeeCompetences)//loops through all competences
                {
                    if (employeeID == employeeCompetence.EmployeeID)
                    {
                        result += $"{employeeCompetence.QuestionnaireCompetenceID.ToString()}: {employeeCompetence.Score.ToString()}, ";
                    }
                }
                result += $" }}, ";
            }

            result += "}; ";

            AllEmpCompetencesString = result;
        }


        public void PopulateAllEmpQuestionnairesString(TeamberContext context)
        {
            var allEmployeeIDs = context.Employees.Select(i => i.ID);
            var allEmployeeQuestionnaires = context.EmpQuestionnaires;

            string result = "var EmpQuestionnaires = { ";

            foreach (var employeeID in allEmployeeIDs) //loops through all employees
            {
                result += $"{employeeID.ToString()}: [ ";
                foreach (var employeeQuestionnaire in allEmployeeQuestionnaires)
                {
                    if (employeeID == employeeQuestionnaire.EmployeeID)
                    {
                        result += $"{employeeQuestionnaire.QuestionnaireID.ToString()}, ";
                    }
                }
                result += $" ], ";
            }

            result += "}; ";

            AllEmpQuestionnairesString = result;
        }


        public void PopulateAllQuestionnairesString(TeamberContext context)
        {
            var allQuestionnaireIDs = context.Questionnaires.Select(i => i.QuestionnaireID);
            var allQuestionnaireCompetences = context.QuestionnaireCompetences;

            string result = "var QuestionnaireCriterias = { ";

            foreach (var questionnaireID in allQuestionnaireIDs) //loops though all employees
            {
                result += $"{questionnaireID.ToString()}: {{ ";
                foreach (var competence in allQuestionnaireCompetences)//loops through all competences
                {
                    if (questionnaireID == competence.QuestionnaireID)
                    {
                        result += $"{competence.QuestionnaireCompetenceID.ToString()}: 0, ";
                    }
                }
                result += $" }}, ";
            }

            result += "}; ";

            AllQuestionnairesString = result;
        }



        public void PopulateAllQuestionnairesStringForEdit(TeamberContext context, Team team)
        {
            var allQuestionnaireIDs = context.Questionnaires.Select(i => i.QuestionnaireID);
            var allQuestionnaireCompetences = context.QuestionnaireCompetences;

            var currentCriterias = context.TeamCriterias.Where(i => i.TeamID == team.TeamID);

            string result = "var QuestionnaireCriterias = { ";

            foreach (var questionnaireID in allQuestionnaireIDs) 
            {
                result += $"{questionnaireID.ToString()}: {{ ";
                foreach (var competence in allQuestionnaireCompetences)
                {
                    if (questionnaireID == competence.QuestionnaireID)
                    {
                        int priority = 0;
                        try
                        {
                            priority = currentCriterias.Where(i => i.QuestionnaireCompetenceID == competence.QuestionnaireCompetenceID).Select(i => i.PriorityValue).FirstOrDefault();
                        }
                        catch (Exception)
                        {
                        }
                        result += $"{competence.QuestionnaireCompetenceID.ToString()}: {priority}, ";
                    }
                }
                result += $" }}, ";
            }

            result += "}; ";

            AllQuestionnairesString = result;
        }


        public void PopulateAllQuestionnaireTitlesString(TeamberContext context)
        {
            var allQuestionnaireIDs = context.Questionnaires.Select(i => i.QuestionnaireID);

            string result = "var QuestionnaireTitles = [ ";

            foreach (var questionnaireID in allQuestionnaireIDs) //looper through all questionnaires
            {
                result += $"{questionnaireID.ToString()}, ";
            }

            result += "]; ";

            AllQuestionnaireTitlesString = result;
        }


        public void PopulateAssignedTeamCriteriaData(TeamberContext context,
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

                if (criteria.TeamID == team.TeamID)
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


        public void UpdateTeamQuestionnaires(TeamberContext context,
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

        public void UpdateTeamEmployees(TeamberContext context,
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


        public void UpdateTeamCriterias(TeamberContext context, int[] selectedCompetences,
            int[] selectedCompetenceValues, Team teamToUpdate)
        {
            if (selectedCompetenceValues == null)
            {
                teamToUpdate.TeamCriterias = new List<TeamCriteria>();
                return;
            }
            
            
            //var selectedCompetenceHS = new HashSet<int>(selectedCompetences);
            //var selectedQuestionnairesHS = new HashSet<int>(context.QuestionnaireCompetences.Where(i => i.QuestionnaireCompetenceID));
            //var questionnairesHS = new HashSet<int>(context.QuestionnaireCompetences.Where(i => i.QuestionnaireID));

            var teamCompetences = new HashSet<int>
                (teamToUpdate.TeamCriterias.Select(c => c.QuestionnaireCompetenceID));
            foreach (var competence in context.QuestionnaireCompetences)
            {
                if (selectedCompetences.Contains(competence.QuestionnaireCompetenceID)) //if the current competence is selected
                {
                    if (!teamCompetences.Contains(competence.QuestionnaireCompetenceID))
                    {
                        int valueIndex = Array.IndexOf(selectedCompetences, competence.QuestionnaireCompetenceID);

                        teamToUpdate.TeamCriterias.Add(
                            new TeamCriteria
                            {
                                TeamID = teamToUpdate.TeamID,
                                QuestionnaireCompetenceID = competence.QuestionnaireCompetenceID,
                                PriorityValue = selectedCompetenceValues[valueIndex]
                            }); ;
                    }
                    else
                    {
                        var currentCompetence = teamToUpdate.TeamCriterias.Where(i => i.QuestionnaireCompetenceID == competence.QuestionnaireCompetenceID).FirstOrDefault();
                        int valueIndex = Array.IndexOf(selectedCompetences, competence.QuestionnaireCompetenceID);

                        currentCompetence.PriorityValue = selectedCompetenceValues[valueIndex];
                    }
                }
                
                
                
            }
            
        }


    }
}
