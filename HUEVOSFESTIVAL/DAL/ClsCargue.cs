using System.ComponentModel.DataAnnotations;
using HUEVOSFESTIVAL.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System;

namespace HUEVOSFESTIVAL.DAL
{
    public class ClsCargue

    {
       
        public int idcarga2 { get; set; }

        [Display(Name = "Id")]
        [Required]
        public int idcarga { get; set; }

        [Display(Name = "EXTR")]
        [Required]
        [Range(0, 10000, ErrorMessage = "Rango fuera de indices")]
        public int extr { get; set; }
        [Display(Name = "AAR")]
        [Required]
        [Range(0, 10000, ErrorMessage = "Rango fuera de indices")]
        public int aar { get; set; }
        [Display(Name = "AR")]
        [Required]
        [Range(0, 10000, ErrorMessage = "Rango fuera de indices")]
        public int ar { get; set; }
        [Display(Name = "BR")]
        [Required]
        [Range(0, 10000, ErrorMessage = "Rango fuera de indices")]
        public int br { get; set; }
        [Display(Name = "EXTB")]
        [Required]
        [Range(0, 10000, ErrorMessage = "Rango fuera de indices")]
        public int extb { get; set; }
        [Display(Name = "AAB")]
        [Required]
        [Range(0, 10000, ErrorMessage = "Rango fuera de indices")]
        public int aab { get; set; }
        [Display(Name = "AB")]
        [Required]
        [Range(0, 1000, ErrorMessage = "Rango fuera de indices")]
        public int ab { get; set; }
        [Display(Name = "BB")]
        [Required]
        [Range(0, 1000, ErrorMessage = "Rango fuera de indices")]
        public int bb { get; set; }
        [Display(Name = "Usuario")]
        [Required]
        public int idusuario { get; set; }
        [Display(Name = "Placa Vehiculo")]
        [Required]
        public int idcarro { get; set; }

        [Display(Name = "Fecha")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fecha { get; set; }

        public int bhabilitado { get; set; }

        //Propiedades Adicionales
        public string Fechacadena { get; set; }


        public List<CARGUE> Selectalldata()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<CARGUE> ListaCarga = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["FESTIVALEntities"].ToString());
                SqlCommand cmd = new SqlCommand("Sp_MostrarCargue", con);
                cmd.CommandType = CommandType.StoredProcedure;              
                cmd.Parameters.AddWithValue("@CustomerID", null);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Mobileno", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailID", null);
                cmd.Parameters.AddWithValue("@Query", 4);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                ListaCarga = new List<CARGUE>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CARGUE cobj = new CARGUE();
                    cobj.IDCARGA = Convert.ToInt32(ds.Tables[0].Rows[i]["IDCARGA"].ToString());
                    cobj.EXTR = Convert.ToInt32(ds.Tables[0].Rows[i]["EXTR"].ToString());
                    cobj.AAR = Convert.ToInt32(ds.Tables[0].Rows[i]["AAR"].ToString());
                    cobj.AR = Convert.ToInt32(ds.Tables[0].Rows[i]["AR"].ToString());
                    cobj.BR = Convert.ToInt32(ds.Tables[0].Rows[i]["BR"].ToString());
                    cobj.EXTB = Convert.ToInt32(ds.Tables[0].Rows[i]["EXTB"].ToString());
                    cobj.AAB = Convert.ToInt32(ds.Tables[0].Rows[i]["AAB"].ToString());
                    cobj.AB = Convert.ToInt32(ds.Tables[0].Rows[i]["AB"].ToString());
                    cobj.BB = Convert.ToInt32(ds.Tables[0].Rows[i]["BB"].ToString());
                    cobj.FECHA = Convert.ToDateTime(ds.Tables[0].Rows[i]["FECHA"].ToString());

                    ListaCarga.Add(cobj);
                }
                return ListaCarga;
            }
            catch
            {
                return ListaCarga;
            }
            finally
            {
                con.Close();
            }
        }
















    }
}