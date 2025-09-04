using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Servicios;
using DAL.Servicios;
using BE;

namespace BLL.Servicios
{
    public class BitacoraBL
    {
        private static BitacoraDAL mBitacoraDAL = new BitacoraDAL();

        public List<BitacoraBE> Listar(DateTime FechaDesde, DateTime FechaHasta, string Evento)
        {   //funcion que valida si el usuario tenia permisos para listar la bitacora
           // if (!ControladorPermisos.TienePermiso(TipoDePermiso.ListarBitacora))
             //   throw new UsuarioNoTienePermisoException();

            return mBitacoraDAL.Listar(FechaDesde,FechaHasta,Evento);
        }

        // hago 2 metodos y no le paso un usuario al metodo registrar bitacora por que en la mayoria de los casos siempre hay un usuario y me ahorro andar pasandole siempre al usuario
        // los casos donde no hay usuario no son muchos


        /// <summary>
        /// Registra el evento en la bitacora, fecha y hora son las actuales en el momento del registro y el usuario asociado es el logeado
        /// </summary>
        /// <param name="pEvento"></param>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public static int RegistrarBitacora(TipoEvento pEvento, Usuario pUsuario, string Descripcion = "")
        {
            BitacoraBE mBitacora = new BitacoraBE();
            mBitacora.Usuario = pUsuario;
            mBitacora.FechaHora = DateTime.Now;
            mBitacora.TipodeEvento = pEvento;
            mBitacora.Descripcion = Descripcion;
           return mBitacoraDAL.Agregar(mBitacora);
        }
        /// <summary>
        /// Registra el evento en la bitacora, la fecha y hora son las actuales en el momento del registro, pero sin especificar al usuario
        /// </summary>
        /// <param name="pEvento"></param>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public static int RegistrarBitacoraSinUsuario(TipoEvento pEvento, string Descripcion="")
        {
            BitacoraBE mBitacora = new BitacoraBE();
            mBitacora.FechaHora = DateTime.Now;
            mBitacora.TipodeEvento = pEvento;
            mBitacora.Descripcion = Descripcion;
            return mBitacoraDAL.Agregar(mBitacora);
        }


    }
}
