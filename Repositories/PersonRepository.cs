using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGualpa_TC_S5_PA.Models;
using SQLite;

namespace JGualpa_TC_S5_PA.Repositories
{
    public class PersonRepository
    {
        string dbPath;
        private SQLiteConnection conn;
        public string statusMessage { get; set; }

        public PersonRepository(string path)
        {
            dbPath = path;
        }

        private void Init()
        {
            if (conn is not null)
                return;
            conn = new(dbPath);
            conn.CreateTable<Persona>();
        }

        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("El nombre es requerido");

                Persona person = new() { Name = name };
                result = conn.Insert(person);
                statusMessage = string.Format($"Dato ingresado ({result} fila(s) afectadas)"); 
            }
            catch (Exception ex)
            {

                statusMessage = string.Format("ERROR al insertar: " + ex.Message); 
            }
        }

        public List<Persona> GetAllPerson()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("ERROR al listar: " + ex.Message); // Mostrar el mensaje de error
            }
            return new List<Persona>();
        }

        // *** Nuevo Método para Actualizar ***
        public void UpdatePerson(Persona person)
        {
            try
            {
                Init();
                if (person == null || person.Id <= 0)
                {
                    throw new Exception("Datos de persona inválidos para actualizar (ID requerido).");
                }
                if (string.IsNullOrEmpty(person.Name))
                {
                    throw new Exception("El nombre no puede estar vacío para actualizar.");
                }

                int result = conn.Update(person); 
                statusMessage = string.Format($"Dato actualizado ({result} fila(s) afectadas)");
            }
            catch (Exception ex)
            {
                statusMessage = string.Format($"ERROR al actualizar: {ex.Message}");
            }
        }

        // *** Nuevo Método para Eliminar ***
        public void DeletePerson(int id)
        {
            try
            {
                Init();
                if (id <= 0)
                {
                    throw new Exception("ID de persona inválido para eliminar.");
                }

                int result = conn.Delete<Persona>(id); 
                statusMessage = string.Format($"Dato eliminado ({result} fila(s) afectadas)");
            }
            catch (Exception ex)
            {
                statusMessage = string.Format($"ERROR al eliminar: {ex.Message}");
            }
        }
    }
}