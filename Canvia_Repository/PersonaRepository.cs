using Canvia_DTO;
using Canvia_Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Canvia_Repository
{
    public class PersonaRepository : IPersonaServices
    {
        private readonly IOptions<ConnectionRepository> cnx;
        public PersonaRepository(IOptions<ConnectionRepository> conexion)
        {
            cnx = conexion;
        }

        public bool Actualizar(int intPersonaId, Persona_DTO BE)
        {
            bool result = false;
            using (SqlConnection cn = new SqlConnection(cnx.Value.conexionLocal))
            {
                using (SqlCommand cmd = new SqlCommand("usp_persona_actualizar", cn))
                {
                    cn.Open();
                    try
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PersonaId", SqlDbType.Int).Value = intPersonaId;
                        cmd.Parameters.Add("@Dni", SqlDbType.VarChar, 50).Value = BE.Dni;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = BE.Nombres;
                        cmd.Parameters.Add("@Apellidos", SqlDbType.VarChar, 50).Value = BE.Apellidos;
                        cmd.Parameters.Add("@Genero", SqlDbType.Char, 1).Value = BE.Genero;
                        cmd.Parameters.Add("@FlagEnviado", SqlDbType.Bit).Value = BE.FlagEnviado;
                        cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = BE.Edad;
                        cmd.Parameters.Add("@Celular", SqlDbType.NVarChar,20).Value = BE.Celular;
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        cn.Close();
                        cn.Dispose();
                        cmd.Dispose();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cn.Close();
                        cn.Dispose();
                        cmd.Dispose();
                    }

                }
            }
            return result;
        }

        public bool AnulacionLogica(int personaId)
        {
            bool result = false;
            using (SqlConnection cn = new SqlConnection(cnx.Value.conexionLocal))
            {
                using (SqlCommand cmd = new SqlCommand("usp_persona_anulacionLogica", cn))
                {
                    cn.Open();
                    try
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PersonaId", SqlDbType.Int).Value = personaId;

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        cn.Close();
                        cn.Dispose();
                        cmd.Dispose();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cn.Close();
                        cn.Dispose();
                        cmd.Dispose();
                    }

                }
            }
            return result;
        }

        public bool Eliminar(int personaId)
        {
            bool result = false;
            using (SqlConnection cn = new SqlConnection(cnx.Value.conexionLocal))
            {
                using (SqlCommand cmd = new SqlCommand("usp_persona_eliminar", cn))
                {
                    cn.Open();
                    try
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PersonaId", SqlDbType.Int).Value = personaId;
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        cn.Close();
                        cn.Dispose();
                        cmd.Dispose();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cn.Close();
                        cn.Dispose();
                        cmd.Dispose();
                    }

                }
            }
            return result;
        }

        public bool Insertar(Persona_DTO BE)
        {
            bool result = false;
            using (SqlConnection cn = new SqlConnection(cnx.Value.conexionLocal))
            {
                using (SqlCommand cmd = new SqlCommand("usp_persona_insert", cn))
                {
                    cn.Open();
                    try
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Dni", SqlDbType.VarChar, 50).Value = BE.Dni;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = BE.Nombres;
                        cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = BE.Edad;
                        cmd.Parameters.Add("@Apellidos", SqlDbType.VarChar, 50).Value = BE.Apellidos;
                        cmd.Parameters.Add("@Genero", SqlDbType.Char, 1).Value = BE.Genero;
                        cmd.Parameters.Add("@Celular", SqlDbType.NVarChar, 20).Value = BE.Celular;

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        cn.Close();
                        cn.Dispose();
                        cmd.Dispose();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cn.Close();
                        cn.Dispose();
                        cmd.Dispose();
                    }

                }
            }
            return result;
        }

        public List<Persona_DTO> Listar()
        {
            List<Persona_DTO> lista = new List<Persona_DTO>();
            using (SqlConnection cn = new SqlConnection(cnx.Value.conexionLocal))
            {
                using (SqlCommand cmd = new SqlCommand("usp_persona_listar", cn))
                {
                    try
                    {
                        cn.Open();
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(MapToEntity_New<Persona_DTO>(dr));

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        cn.Close();
                        cn.Dispose();
                        cmd.Dispose();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cn.Close();
                        cn.Dispose();
                        cmd.Dispose();
                    }
                }
            }
            return lista; throw new NotImplementedException();
        }

        public List<Persona_DTO> ListarById(int personaId)
        {
            List<Persona_DTO> lista = new List<Persona_DTO>();
            using (SqlConnection cn = new SqlConnection(cnx.Value.conexionLocal))
            {
                using (SqlCommand cmd = new SqlCommand("usp_persona_ListarById", cn))
                {
                    try
                    {
                        cn.Open();
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@personaId", SqlDbType.Int).Value = personaId;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(MapToEntity_New<Persona_DTO>(dr));

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        cn.Close();
                        cn.Dispose();
                        cmd.Dispose();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cn.Close();
                        cn.Dispose();
                        cmd.Dispose();
                    }
                }
            }
            return lista; throw new NotImplementedException();
        }

        public virtual Ta MapToEntity_New<Ta>(IDataReader reader)
        {

            var columnCount = reader.FieldCount;
            var item = Activator.CreateInstance<Ta>();
            try
            {                
                var rdrProperties = Enumerable.Range(0, columnCount).Select(i => reader.GetName(i)).ToArray();
                foreach (var property in typeof(Ta).GetProperties())
                {
                    if ((typeof(Ta).GetProperty(property.Name).GetGetMethod().IsVirtual) || (!rdrProperties.Contains(property.Name)))
                    {
                        continue;
                    }
                    else
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                        {
                            Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return item;
        }
    }
}
