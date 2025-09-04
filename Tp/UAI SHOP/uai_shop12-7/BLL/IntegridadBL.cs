using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Servicios;
using System.Data;
using DAL.Servicios;
using BE;
using SERVICES;
using System.Reflection;
namespace BLL.Servicios
{
  public  class IntegridadBL
    {
        private IntegridadDAL mIntegridadDAL = new IntegridadDAL();
        private UsuarioDAL mUserDAL = new UsuarioDAL();
        public static bool IntegridadCorrupta =false;
        private List<DataRow> ObtenerRegistros(string nombretabla)
        {
            if (nombretabla == "Usuario")
            {
                return new UsuarioBll().ObtenerRegistros();
            }
            else if (nombretabla == "Producto")
            {
                return new ProductoBll().ObtenerRegistros();
            }
            else
                return null;
        }

        /// <summary>
        /// Metodo que genera un DVH en base a un row
        /// </summary>
        /// <param name="pRow"></param>
        /// <returns></returns>
        private string GenerarDVH(DataRow pRow)
        {
           var array = pRow.ItemArray.ToList();// lo paso a un array de object
           array.Remove(pRow["DVH"]);
            string rowConcatenado = "";
            foreach (var prop in array)// por cada una de sus propiedadades las concateno
            {
                if (prop.ToString() != "DVH")// concateno todos menos la columna de DVH
                    rowConcatenado += prop.ToString(); // concatena el valor del objeto
            }   
            string resultado= EncriptadorService.Instance.GenerarHASH(rowConcatenado);
            return resultado;
        }
        /// <summary>
        /// Genero un DVV a partir un una lista DVHS
        /// </summary>
        /// <param name="listaDVHS"></param>
        /// <returns></returns>
        public string GenerarDVV(List<string> listaDVHS)
        {
            string valorAEncriptar = string.Concat(listaDVHS);
            string resultado= EncriptadorService.Instance.GenerarHASH(valorAEncriptar);
            return resultado;
        }


        /// <summary>
        /// Esta funcion Genera un DVH para el Row Recibido y lo compara con el DVH que tiene almacenado el mismo. Si coinciden devuelve true, caso contrario false
        /// </summary>
        /// <param name="pRow"></param>
        /// <returns></returns>
        private bool ValidarDVH(DataRow pRow)
        {
            if (GenerarDVH(pRow) == pRow["DVH"].ToString())
            {
                return true;
            }
            else
                return false;
      
        }

        private bool ValidarDVV(string nombreTabla)
        {

            if (mIntegridadDAL.ObtenerDVV(nombreTabla) == GenerarDVV(ObtenerDVHS(nombreTabla)))
            {
                return true;
            }
            else
                return false;

        }

        public  void ActualizarDVH()
        {

            foreach (string tabla in ConfiguracionesGenerales.tablasClaves)
            {
                List<DataRow> mlistaRow = ObtenerRegistros(tabla);
                foreach (DataRow mrow in mlistaRow)
                {
                    string newDVH = GenerarDVH(mrow);
                    mrow["DVH"]=newDVH;
                    mIntegridadDAL.ActualizarDVH(tabla,mrow);
                }
            }

        }
       private List<string> ObtenerDVHS(string nombreTabla)
        {
            List<string> mlistaDVH = new List<string>();
            foreach (DataRow mrow in ObtenerRegistros(nombreTabla))
            {
                mlistaDVH.Add(mrow["DVH"].ToString());
            }
            return mlistaDVH;
        }
        /// <summary>
        /// Esta funcion valida todas las tabla claves del sistema, para saber si la integridad en la base de datos es correcta o no. Devuelve lista de errores
        /// </summary>
        public List<string> VerificarIntegridad()
        {
            List<string> mlistaErrores = new List<string>();// aca voy almacenando los errores que detecte

            foreach(string tabla in ConfiguracionesGenerales.tablasClaves)
            {
                List<DataRow> mlistaRow = ObtenerRegistros(tabla);

                foreach (DataRow mrow in mlistaRow)
                {
                    //no valido los dos DV juntos en una sola funcion asi puedo agarrar los errores concretos
                    if (!this.ValidarDVH(mrow))//Si no coinciden los DVH quiere decir que el row fue modificado por fuera de la aplicacion
                    {
                        mlistaErrores.Add(string.Format("Error de integridad. TABLA: {0} , Error DVH Fila Nº {1}",tabla, mlistaRow.IndexOf(mrow) + 1));
                    }

                }
                if (!ValidarDVV(tabla))
                {
                    mlistaErrores.Add(string.Format("Error DVV en la tabla {0}", tabla));
                }


            }

            if (mlistaErrores.Any())
            {
                IntegridadCorrupta = true;
                BitacoraBL.RegistrarBitacoraSinUsuario(TipoEvento.Error, $"La integridad de la base de datos esta corrupta: {string.Join("\r\n", mlistaErrores)} ");               
                //throw new Excepciones.IntegridadBaseDeDatosException(ConstantesDeTexto.Error_integridad);
            }
            else
            {
                IntegridadCorrupta = false;
            }
            return mlistaErrores;
                

        }
        /// <summary>
        /// Actualizas los DVV de todas las tablas claves del sistema
        /// </summary>
        /// <returns></returns>
        public int ActualizarDVV()
        {
            int i = 1;
            foreach(String tabla in ConfiguracionesGenerales.tablasClaves)
            {
                string DvvNuevo = this.GenerarDVV(this.ObtenerDVHS(tabla));
                 i = mIntegridadDAL.ActualizarDVV(tabla, DvvNuevo);
                if (i != 0)
                    BitacoraBL.RegistrarBitacoraSinUsuario(TipoEvento.Message, "Se actualizaron los DVV de la tabla: " + tabla);
                if (i == 0)
                    return i;
            }
            return i;
      
        }
        /// <summary>
        /// Devuelve el DVH correspondiente al objeto pasado por parametro
        /// </summary>
        /// <param name="objeto"> Objeto a obtener DVH</param>
        /// <returns></returns>
        public string ObtenerDVH(Object objeto)
        {
            string objetoConcatenado = "";
            Type t = objeto.GetType();// obtengo el type del objeto
            PropertyInfo[] props = t.GetProperties();// obtengo todas sus propiedades
            foreach (var prop in props)// por cada una de sus propiedadades las concateno
            {
                if (prop.Name != "DVH")// concateno todos menos la columna de DVH
                {
                    Type mtype = prop.PropertyType;// obtengo la clase de la propiedad
                    if (mtype.Name != "String")// hago esto por que el type string tiene dos properties, en cambio int, bool etc no tienen ninguna entonces no pasan el if( any
                    {
                        PropertyInfo[] propiedades = mtype.GetProperties();// obtengo sus propiedades
                        if (propiedades.Any())// quiere decir que es un objeto no comun, osea que no es un string ni int etc
                        {
                            Object mObjeto = prop.GetValue(objeto);// agarro el objeto del objeto
                            IEntidadVerificable mEntidad = mObjeto as IEntidadVerificable;  // lo casteo                              
                            objetoConcatenado += mEntidad.GetDbKey();  // entonces una vez casteado esa entidad al implementar la interfaz IEntidadVerificable sabra dar la primary key                        
                        }
                        else
                            objetoConcatenado += prop.GetValue(objeto); // concatena el valor del objeto
                    }                  
                    else
                        objetoConcatenado += prop.GetValue(objeto); // concatena el valor del objeto
                }
                
            }
            string resultado = EncriptadorService.Instance.GenerarHASH(objetoConcatenado);
            return resultado;// devuelvo el DVH correspondiente a ese objeto utilizando un algoritmo hash
        }

    }
}
