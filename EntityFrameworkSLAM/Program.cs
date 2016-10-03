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
                Console.Write(employeId.NOMEMP + " - " + employeId.PRENOMEMP + " - " + employeId.SALAIRE);

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
                    Console.WriteLine("L'employé numéro " + idEmploye + " n'existe pas.");
                }

                

            }

           

            Console.Read();


        }
    }
}
