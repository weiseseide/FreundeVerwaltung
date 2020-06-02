using CBAP.Dynamic.Model;
using CBAP.Dynamic.Tools;
using CBAP.Shared.Tools;
using Itb.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//wie dialog öffnen. 2 verschiedene usercontrols? Dann alle sql immer so komisch lang? oder als funktion

namespace FreundeVerwaltung.DatabaseManager
{
    public interface IDataBaseManager //ToDo DataTable statt DataSet
    {
        ObservableCollection<People> refreshData();
        void deleteEntry(People SelectedPerson);
        void editEntry(People SelectedPerson);
        void newEntry(People newPerson);
        People getPerson(int id);
    }

    [Export(typeof(IDataBaseManager))]
    public class DataBaseManager : IDataBaseManager
    {
        KundenEntities dataEntities = new KundenEntities();
        DataTable workTable = new DataTable();


        public ObservableCollection<People> refreshData()
        {

            return new ObservableCollection<People>(dataEntities.People.ToList());

        }


        public void newEntry(People newPerson)
        {
            string connectionString = @"data source=CORELLIA\SQLEXPRESS;initial catalog=Kunden;integrated security=True;MultipleActiveResultSets=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter people = new SqlDataAdapter("Select * From People", connection);// where PersonID = " + SelectedPerson.PersonID + ";

                    DataTable dsKunden = new DataTable();
                    people.Fill(dsKunden);

                    DataRow drCurrent;
                    drCurrent = dsKunden.NewRow();

                    drCurrent["CustomerID"] = newPerson.CustomerID;
                    drCurrent["FirstName"] = newPerson.FirstName;
                    drCurrent["LastName"] = newPerson.LastName;


                    //Commit
                    dsKunden.Rows.Add(drCurrent);
                    SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(people);
                    people.Update(dsKunden);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        public void deleteEntry(People SelectedPerson)
        {
            string connectionString = @"data source=CORELLIA\SQLEXPRESS;initial catalog=Kunden;integrated security=True;MultipleActiveResultSets=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter people = new SqlDataAdapter("Select PersonID From People where PersonID=" +SelectedPerson.PersonID, connection);

                    DataTable dsKunden = new DataTable();
                    people.Fill(dsKunden);


                    DataRow drCurrent;
                    drCurrent = dsKunden.Rows[0];//Hier zb ohne where statement und dann mit find // drCurrent = tblPeople.Rows.Find(SelectedPerson.PersonID);

                    drCurrent.Delete();

                    //Commit
                    SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(people);
                    people.Update(dsKunden);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void editEntry(People SelectedPerson)
        {

            string connectionString = @"data source=CORELLIA\SQLEXPRESS;initial catalog=Kunden;integrated security=True;MultipleActiveResultSets=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter people = new SqlDataAdapter("Select * From People where PersonID = " + SelectedPerson.PersonID + ";", connection);// where PersonID = " + SelectedPerson.PersonID + ";

                    DataTable dsKunden = new DataTable();
                    people.Fill(dsKunden);


                    DataRow drCurrent;

                    drCurrent = dsKunden.Rows[0];
                    drCurrent.BeginEdit();
                    drCurrent["CustomerID"] = SelectedPerson.CustomerID;
                    drCurrent["FirstName"] = SelectedPerson.FirstName;
                    drCurrent["LastName"] = SelectedPerson.LastName;
                    drCurrent.EndEdit();

                    SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(people);
                    people.Update(dsKunden);

                    dataEntities.People.AddOrUpdate(SelectedPerson);//ToDo der hurensohn
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public People getPerson(int id)
        {
            People p = new People();
            string connectionString = @"data source=CORELLIA\SQLEXPRESS;initial catalog=Kunden;integrated security=True;MultipleActiveResultSets=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter people = new SqlDataAdapter("Select * From People where PersonID=" + id, connection);

                    DataTable dsKunden = new DataTable();
                    people.Fill(dsKunden);

                    DataRow drCurrent;
                    //drCurrent = tblPeople.Rows.Find(id);
                    drCurrent = dsKunden.Rows[0];

                    p.PersonID = (int)drCurrent["PersonID"];
                    p.CustomerID = (int)drCurrent["CustomerID"];
                    p.LastName = drCurrent["LastName"].ToString();
                    p.FirstName = drCurrent["FirstName"].ToString();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return p;

        }
    }
}


