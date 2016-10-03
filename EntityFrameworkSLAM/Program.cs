using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkSLAM
{
    class Program
    {
        static void Main(string[] args)
        {

            using (OracleEntities oracleContexte = new OracleEntities())
            {

                var requeteEmployes = from EMPLOYE in oracleContexte.EMPLOYEs
                                      select EMPLOYE;

                var lesEmployes = requeteEmployes.ToList();

                foreach(var unEmploye in lesEmployes)
                {
                    Console.WriteLine(unEmploye.NUMEMP + " - " + unEmploye.NOMEMP);
                }

                Console.WriteLine("-----------------------------------");

                var unCodeProjet = "PR1";
                var requeteEmployesProjet = from EMPLOYE in oracleContexte.EMPLOYEs
                                            where EMPLOYE.CODEPROJET.TrimEnd() == unCodeProjet
                                            select EMPLOYE;

                lesEmployes = requeteEmployesProjet.ToList();

                foreach(var unEmploye in lesEmployes)
                {
                    Console.WriteLine(unEmploye.NUMEMP + " - " + unEmploye.NOMEMP);
                }

                Console.WriteLine("-----------------------------------");

                var idEmploye = 3;
                var requeteEmployesById = from EMPLOYE in oracleContexte.EMPLOYEs
                                            where EMPLOYE.NUMEMP == idEmploye
                                            select EMPLOYE;

                var employeId = requeteEmployesById.First();
                Console.WriteLine(employeId.NOMEMP + " - " + employeId.PRENOMEMP + " - " + employeId.SALAIRE);

                Console.WriteLine("-----------------------------------");

                var idEmployeBis = 33;
                var requeteEmployesByIdBis = from EMPLOYE in oracleContexte.EMPLOYEs
                                             where EMPLOYE.NUMEMP == idEmployeBis
                                             select EMPLOYE;

                var employeIdBis = requeteEmployesByIdBis.FirstOrDefault();

                if(employeIdBis != null)
                {
                    Console.Write(employeIdBis.NOMEMP + " - " + employeIdBis.PRENOMEMP + " - " + employeIdBis.SALAIRE);
                }else
                {
                    Console.WriteLine("L'employé numéro " + idEmployeBis + " n'existe pas.");
                }

                Console.WriteLine("----------------> JOINTURE <------------------");

                var requete = from s in oracleContexte.SEMINAIREs
                              join COUR in oracleContexte.COURS on s.CODECOURS equals COUR.CODECOURS
                              group s by new { s.CODECOURS, s.COUR.LIBELLECOURS } into groupEmployes
                              select new
                              {
                                  Cours = groupEmployes.Key.CODECOURS,
                                  Libelle = groupEmployes.Key.LIBELLECOURS,
                                  Nombre = groupEmployes.Count()
                              };


                
                

                var lesSeminaires = requete.ToList();
                

                foreach (var unSeminaire in lesSeminaires)
                {
                    Console.WriteLine(unSeminaire.Libelle + " - " + unSeminaire.Cours + " - " + unSeminaire.Nombre);


                    var requeteDate = from s in oracleContexte.SEMINAIREs
                                      where s.CODECOURS == unSeminaire.Cours
                                      select s;

                    var lesDates = requeteDate.ToList();

                    foreach (var uneDate in lesDates)
                    {


                        Console.WriteLine("                            " + uneDate.DATEDEBUTSEM);
                    }
                }


                // Mise à jour des données

                var idEmp = 200;
                var emp = oracleContexte.EMPLOYEs.Find(idEmp);
                
               

                if (emp != null)
                {
                    emp.SALAIRE = emp.SALAIRE * (decimal)1.1;
                    oracleContexte.SaveChanges();
                    Console.WriteLine("Le nouveau salaire = " + emp.SALAIRE);
                }
                else
                {
                    Console.WriteLine("L'employé numéro " + idEmp + " n'existe pas.");
                }


                // Création de l'objet cours 

                COUR unCours = new COUR();
                unCours.CODECOURS = "BR099";
                unCours.LIBELLECOURS = "Entity framework 6 avec oracle";
                unCours.NBJOURS = 4;
                oracleContexte.COURS.Add(unCours);
                oracleContexte.SaveChanges();
                Console.WriteLine("Le cours a été crée");




            }

           

            Console.Read();


        }
    }
}
