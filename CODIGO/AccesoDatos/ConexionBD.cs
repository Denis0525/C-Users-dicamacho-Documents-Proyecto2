using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using Ejercicio2.Models;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using static System.Net.WebRequestMethods;
using System.Diagnostics.Metrics;
using System.Data.SqlTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ejercicio2.AccesoDatos
{
    public class ConexionBD
    {
        private NpgsqlConnection? con;
        private void conexion()
        {
            //string strConexion = "Provider=PostgreSQL OLE DB Provider;Data Source=127.0.0.1;location=Prueba_sye;User ID=postgres;password=;timeout=1000;";
            //string strConexion = "Provider=PostgreSQL OLE DB Provider; Server = localhost; Port = 5432; Database = Prueba_sye; Uid = postgres; Pwd = Rebeca";
            string strConexion = "server=localhost;port=5432;user id=postgres;password=Rebeca;database=Productos;";
            con = new NpgsqlConnection(strConexion);
        }
        public List<Productos> OntenerTodo()
        {

            List<Productos> listProdu = new List<Productos>();
            conexion();
            DataTable dt = new DataTable();
            NpgsqlCommand sqlcmd = new NpgsqlCommand("select * from schemasye.obtener_productos()", con);
            sqlcmd.CommandType = CommandType.Text;

            try
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqlcmd);
                con.Open();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Productos mod = new Productos();
                        mod.id_producto = Convert.ToInt16(dr[0]);
                        mod.nombre = dr[1].ToString();
                        mod.precio = Convert.ToInt16(dr[2]);
                        mod.cantidad = Convert.ToInt16(dr[3]);
                        mod.fecha_Registro = Convert.ToString(dr[4]);
                        mod.estado = dr[5].ToString();
                        listProdu.Add(mod);

                    }
                }
            }
            catch (Exception e)
            {
                string horror = e.Message.ToString();
                return listProdu;
            }
            finally
            {

                if (con != null) { con.Close(); }
            }

            return listProdu;
        }

        public bool Insertar(Productos mod)
        {
            conexion();
            DataTable dt = new DataTable();
            //NpgsqlCommand sqlcmd = new NpgsqlCommand("insert into public.inserta_productos(@nombre, @precio, @cantidad, @fecha_registro, @estado)", con);
            NpgsqlCommand sqlcmd = new NpgsqlCommand("call schemasye.spInsertarProducto(@nombre, @precio, @cantidad, @estado)", con);
             
            sqlcmd.CommandType = CommandType.Text;

            try
            {
                sqlcmd.Parameters.AddWithValue("@nombre", mod.nombre.ToString());
                sqlcmd.Parameters["@nombre"].NpgsqlDbType = NpgsqlDbType.Varchar;
                sqlcmd.Parameters.AddWithValue("@precio", mod.precio);
                sqlcmd.Parameters["@precio"].NpgsqlDbType = NpgsqlDbType.Integer;
                sqlcmd.Parameters.AddWithValue("@cantidad", mod.cantidad);
                sqlcmd.Parameters["@cantidad"].NpgsqlDbType = NpgsqlDbType.Integer;
                bool std = false;
                sqlcmd.Parameters.AddWithValue("@estado", bool.TryParse(mod.estado,out std));
                sqlcmd.Parameters["@estado"].NpgsqlDbType = NpgsqlDbType.Boolean;
                                
                con.Open();
                sqlcmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                string horror = e.Message.ToString();
                return false;
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
            return true;
        }

        public bool Actualizar(Productos mod)
        {
            conexion();
            DataTable dt = new DataTable();
            NpgsqlCommand sqlcmd = new NpgsqlCommand("call schemasye.spActualizarProducto(@idproducto,@pnombre, @pprecio, @pcantidad, @pestado)", con);

            sqlcmd.CommandType = CommandType.Text;

            try
            {
                sqlcmd.Parameters.AddWithValue("@idproducto", mod.id_producto);
                sqlcmd.Parameters["@idproducto"].NpgsqlDbType = NpgsqlDbType.Integer;
                
                sqlcmd.Parameters.AddWithValue("@pnombre", mod.nombre.ToString());
                sqlcmd.Parameters["@pnombre"].NpgsqlDbType = NpgsqlDbType.Varchar;

                sqlcmd.Parameters.AddWithValue("@pprecio", mod.precio);
                sqlcmd.Parameters["@pprecio"].NpgsqlDbType = NpgsqlDbType.Integer;

                sqlcmd.Parameters.AddWithValue("@pcantidad", mod.cantidad);
                sqlcmd.Parameters["@pcantidad"].NpgsqlDbType = NpgsqlDbType.Integer;
                bool std = false;
                sqlcmd.Parameters.AddWithValue("@pestado", bool.TryParse(mod.estado, out std));
                sqlcmd.Parameters["@pestado"].NpgsqlDbType = NpgsqlDbType.Boolean;

                con.Open();
                sqlcmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                string horror = e.Message.ToString();
                return false;
            }
            finally
            {

                if (con != null) { con.Close(); }
            }
            return true;
        }
        public List<Productos> ObtenerPorId(int Id)
        {
            List<Productos> lisProduc = new List<Productos>();
            conexion();
            DataTable dt = new DataTable();
            NpgsqlCommand sqlcmd = new NpgsqlCommand("select * from schemasye.tc_producto where id_producto =" + Id, con);
            sqlcmd.CommandType = CommandType.Text;

            try
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqlcmd);

                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Productos mod = new Productos();
                        mod.id_producto = Convert.ToInt16(dr[0]);
                        mod.nombre = dr[1].ToString();
                        mod.precio = Convert.ToInt16(dr[2]);
                        mod.cantidad = Convert.ToInt16(dr[3]);
                        mod.fecha_Registro = Convert.ToString(dr[4]);
                        mod.estado = dr[5].ToString();
                        lisProduc.Add(mod);

                    }
                }
            }
            catch (Exception e)
            {
                string horror = e.Message.ToString();
                return lisProduc;
            }
            finally
            {

                if (con != null) { con.Close(); }
            }

            return lisProduc;
        }

        public bool Eliminar(int Id)
        {
            conexion();
            DataTable dt = new DataTable();
            
            NpgsqlCommand sqlcmd = new NpgsqlCommand("call  schemasye.spEliminarProducto(@id)", con);
            sqlcmd.CommandType = CommandType.Text;


            try
            {
                sqlcmd.Parameters.AddWithValue("@id", Id);
                sqlcmd.Parameters["@id"].NpgsqlDbType = NpgsqlDbType.Integer;
                
                con.Open();
                sqlcmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                string horror = e.Message.ToString();
                return false;
            }
            finally
            {

                if (con != null) { con.Close(); }
            }
            return true;
        }
    }
}